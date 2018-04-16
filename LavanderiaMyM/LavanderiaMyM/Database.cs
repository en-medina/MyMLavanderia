using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LavanderiaMyM
{
    class Database
    {
        public string authUser(string username, string password)
        {
            if (username == "" || password == "") return null;

            SqlParameter output = new SqlParameter("@name", SqlDbType.VarChar, 30);
            output.Direction = ParameterDirection.Output;
            SqlCommand cmd = (SqlCommand)RunStoredProc("authUser", false,
                new SqlParameter("@username", username),
                new SqlParameter("@password", password),
                output
                );
            string ans = cmd.Parameters["@name"].Value.ToString();
            if (ans == "") return null;
            return ans;
        }

        private object RunStoredProc(string storedProcedureName, bool is_table, params SqlParameter[] data)
        {
            SqlConnection conn = null;
            SqlDataReader rdr = null;
            SqlCommand cmd = null;
            try
            {
                // create and open a connection object
                conn = new
                    SqlConnection("Data Source=MEDINA-LAPTOP\\SQLEXPRESS;Initial Catalog=mym;Integrated Security=True");
                conn.Open();

                // 1. create a command object identifying
                // the stored procedure
                cmd = new SqlCommand(
                    "dbo." + storedProcedureName, conn);

                // 2. set the command object so it knows
                // to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                //3. add the parameters to the stored procedure
                if (data != null)
                {
                    foreach (SqlParameter p in data)
                        cmd.Parameters.Add(p);
                }
                // execute the command
                if (is_table) rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                else cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null && !is_table)
                {
                    conn.Close();
                }
            }
            if (is_table) return rdr;
            else return cmd;
        }
    }
}
