using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LavanderiaMyM
{
    class Database
    {
        DataChecker dataChecker = new DataChecker();
        public Employee authUser(string username, string password)
        {
            if (username == "" || password == "") return null;

            SqlParameter output = new SqlParameter("@name", SqlDbType.VarChar, 30);
            output.Direction = ParameterDirection.Output;
            SqlParameter output1 = new SqlParameter("@id", SqlDbType.Int);
            output1.Direction = ParameterDirection.Output;

            try
            {
                SqlCommand cmd = (SqlCommand)RunStoredProc("authUser", false,
                    new SqlParameter("@username", username),
                    new SqlParameter("@password", password),
                    output, output1
                    );
                string ans = cmd.Parameters["@name"].Value.ToString();
                if (ans == "") return null;
                int id = Int32.Parse(cmd.Parameters["@id"].Value.ToString());

                return new Employee(id, ans, username, 0);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }
        public bool ModifyCustomer(int employeeId, Customer customer)
        {
            try
            {
                SqlDataReader rdr = (SqlDataReader)RunStoredProc("modifyCustomer", true,
                    new SqlParameter("@id", customer.Id),
                    new SqlParameter("@name", customer.Name),
                    new SqlParameter("@nationalID", customer.NationalID),
                    new SqlParameter("@birthday", customer.Birthday),
                    new SqlParameter("@cellphone", customer.Celphone1),
                    new SqlParameter("@telephone", customer.Telephone),
                    new SqlParameter("@email", customer.Email),
                    new SqlParameter("@address", customer.Address),
                    new SqlParameter("@sendWhatsapp", customer.SendWhatsapp),
                    new SqlParameter("@sendEmail", customer.SendWhatsapp),
                    new SqlParameter("@employeeID", employeeId),
                    new SqlParameter("@notes", customer.Notes),
                    new SqlParameter("@discount", customer.Discount)
                );
                if (rdr != null) rdr.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }

            return true;
        }


        public List<Customer> SearchCustomerByName(string search) => SearchCustomer("searchCustomerByName", search);
        public List<Customer> SearchCustomerByNationalID(string search) => SearchCustomer("searchCustomerByNationalID", search);
        public List<Customer> SearchCustomerByPhone(string search) => SearchCustomer("searchCustomerByPhone", search);
        public List<Customer> SearchCustomerByEmail(string search) => SearchCustomer("searchCustomerByEmail", search);

        private List<Customer> SearchCustomer(string storeProcedureName, string search)
        {
            List<Customer> ans = new List<Customer>();
            try
            {
                SqlDataReader rdr = (SqlDataReader)RunStoredProc(storeProcedureName, true,
                    new SqlParameter("@param", search)
                );
                while (rdr.Read())
                {
                    Customer temp = new Customer(
                        rdr.GetString(rdr.GetOrdinal("name")).ToString(),
                        rdr.GetString(rdr.GetOrdinal("nationalID")).ToString(),
                        DateTime.Parse(rdr.GetSqlDateTime(rdr.GetOrdinal("birthday")).ToString()),
                        rdr.GetString(rdr.GetOrdinal("cellphone")).ToString(),
                        rdr.GetString(rdr.GetOrdinal("telephone")).ToString(),
                        Boolean.Parse(rdr.GetBoolean(rdr.GetOrdinal("sendWhatsapp")).ToString()),
                        rdr.GetString(rdr.GetOrdinal("email")).ToString(),
                        Int32.Parse(rdr.GetInt32(rdr.GetOrdinal("discount")).ToString()),
                        rdr.GetString(rdr.GetOrdinal("notes")).ToString()
                        )
                    {
                        Id = Int32.Parse(rdr.GetInt32(rdr.GetOrdinal("customerID")).ToString())
                    };
                    ans.Add(temp);
                }
                if (rdr != null) rdr.Close();
                return ans;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return ans;
            }
        }
        public bool InsertCustomer(int employeeId, Customer customer)
        {
            try
            {
                SqlDataReader rdr = (SqlDataReader)RunStoredProc("insertCustomer", true,
                    new SqlParameter("@name", customer.Name),
                    new SqlParameter("@nationalID", customer.NationalID),
                    new SqlParameter("@birthday", customer.Birthday),
                    new SqlParameter("@cellphone", customer.Celphone1),
                    new SqlParameter("@telephone", customer.Telephone),
                    new SqlParameter("@email", customer.Email),
                    new SqlParameter("@address", customer.Address),
                    new SqlParameter("@sendWhatsapp", customer.SendWhatsapp),
                    new SqlParameter("@sendEmail", customer.SendWhatsapp),
                    new SqlParameter("@employeeID", employeeId),
                    new SqlParameter("@notes", customer.Notes),
                    new SqlParameter("@discount", customer.Discount)
                );
                if (rdr != null) rdr.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }

            return true;
        }
        public CashBox insertCashBox(CashBox cashBox)
        {
            SqlParameter output1 = new SqlParameter("@result", SqlDbType.Money);
            SqlParameter output2 = new SqlParameter("@state", SqlDbType.Bit);
            output1.Direction = ParameterDirection.Output;
            output2.Direction = ParameterDirection.Output;
            try
            {
                SqlCommand cmd = (SqlCommand)RunStoredProc("insertCashBox", false,
                    new SqlParameter("@id", cashBox.EmployeeId),
                    new SqlParameter("@cashInDebitCard", cashBox.CashInDebitCard),
                    new SqlParameter("@cashInCreditCard", cashBox.CashInCreditCard),
                    new SqlParameter("@cashInCheck", cashBox.CashInCheck),
                    new SqlParameter("@m2000", cashBox.Money[10]),
                    new SqlParameter("@m1000", cashBox.Money[9]),
                    new SqlParameter("@m500", cashBox.Money[8]),
                    new SqlParameter("@m200", cashBox.Money[7]),
                    new SqlParameter("@m100", cashBox.Money[6]),
                    new SqlParameter("@m50", cashBox.Money[5]),
                    new SqlParameter("@m25", cashBox.Money[4]),
                    new SqlParameter("@m20", cashBox.Money[3]),
                    new SqlParameter("@m10", cashBox.Money[2]),
                    new SqlParameter("@m5", cashBox.Money[1]),
                    new SqlParameter("@m1", cashBox.Money[0]),
                    new SqlParameter("@moneyInBox", cashBox.MoneyInBox),
                    output1, output2
                );
                cashBox.IsBoxClose = (bool)cmd.Parameters["@state"].Value;
                cashBox.LeftOver = Double.Parse(cmd.Parameters["@result"].Value.ToString());

                return cashBox;
            }
            catch(SqlException esql)
            {
                MessageBox.Show(esql.ToString());
                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }
        public CashBox modifyCashBox(CashBox cashBox)
        {

            CashBox ans = null;
            try
            {
                SqlDataReader rdr = (SqlDataReader)RunStoredProc("modifyCashBox", true,
                    new SqlParameter("@idEmployee", cashBox.EmployeeId),
                    new SqlParameter("@idCashBox", cashBox.Id),
                    new SqlParameter("@cashInDebitCard", cashBox.CashInDebitCard),
                    new SqlParameter("@cashInCreditCard", cashBox.CashInCreditCard),
                    new SqlParameter("@cashInCheck", cashBox.CashInCheck),
                    new SqlParameter("@m2000", cashBox.Money[10]),
                    new SqlParameter("@m1000", cashBox.Money[9]),
                    new SqlParameter("@m500", cashBox.Money[8]),
                    new SqlParameter("@m200", cashBox.Money[7]),
                    new SqlParameter("@m100", cashBox.Money[6]),
                    new SqlParameter("@m50", cashBox.Money[5]),
                    new SqlParameter("@m25", cashBox.Money[4]),
                    new SqlParameter("@m20", cashBox.Money[3]),
                    new SqlParameter("@m10", cashBox.Money[2]),
                    new SqlParameter("@m5", cashBox.Money[1]),
                    new SqlParameter("@m1", cashBox.Money[0]),
                    new SqlParameter("@moneyInBox", cashBox.MoneyInBox),
                    new SqlParameter("@wasCreated", cashBox.WasCreated)
                );
                if (rdr.Read())
                {
                    ans = new CashBox(
                        Int32.Parse(rdr.GetInt32(rdr.GetOrdinal("cashboxID")).ToString()),
                        cashBox.Id,
                        Double.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("cashInDebitCard")).ToString()),
                        Double.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("cashInCreditCard")).ToString()),
                        Double.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("cashInCheck")).ToString()),

                        Int32.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("m2000")).ToString()), 
                        Int32.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("m1000")).ToString()),
                        Int32.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("m500")).ToString()), 
                        Int32.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("m200")).ToString()),
                        Int32.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("m100")).ToString()), 
                        Int32.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("m50")).ToString()),
                        Int32.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("m25")).ToString()), 
                        Int32.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("m20")).ToString()),
                        Int32.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("m10")).ToString()),
                        Int32.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("m5")).ToString()),
                        Int32.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("m1")).ToString()),

                        Double.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("unbalance")).ToString()),
                        Double.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("grossMoney")).ToString()),
                        Double.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("discountMoney")).ToString()),
                        Double.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("moneyInBox")).ToString()),
                        Boolean.Parse(rdr.GetInt32(rdr.GetOrdinal("isBoxClose")).ToString()),
                        DateTime.Parse(rdr.GetSqlDateTime(rdr.GetOrdinal("wasCreated")).ToString())
                    );
                }
                if (rdr != null) rdr.Close();
                return null;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                return ans;
            }
        }

        public List<CashBox> getCashBox(int idEmployee, DateTime fromdate, DateTime todate)
        {
            List<CashBox> ans = new List<CashBox>();
            try
            {
                SqlDataReader rdr = (SqlDataReader)RunStoredProc("getCashBox", true,
                    new SqlParameter("@idEmployee", idEmployee),
                    new SqlParameter("@fromdate", SqlDbType.DateTime) { Value = fromdate },
                    new SqlParameter("@todate", SqlDbType.DateTime) { Value = todate }
                );
                while (rdr.Read())
                {
                    ans.Add(new CashBox(
                        Int32.Parse(rdr.GetInt32(rdr.GetOrdinal("cashboxID")).ToString()),
                        idEmployee,
                        Double.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("cashInDebitCard")).ToString()),
                        Double.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("cashInCreditCard")).ToString()),
                        Double.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("cashInCheck")).ToString()),

                        Int32.Parse(rdr.GetInt16(rdr.GetOrdinal("m2000")).ToString()),
                        Int32.Parse(rdr.GetInt16(rdr.GetOrdinal("m1000")).ToString()),
                        Int32.Parse(rdr.GetInt16(rdr.GetOrdinal("m500")).ToString()),
                        Int32.Parse(rdr.GetInt16(rdr.GetOrdinal("m200")).ToString()),
                        Int32.Parse(rdr.GetInt16(rdr.GetOrdinal("m100")).ToString()),
                        Int32.Parse(rdr.GetInt16(rdr.GetOrdinal("m50")).ToString()),
                        Int32.Parse(rdr.GetInt16(rdr.GetOrdinal("m25")).ToString()),
                        Int32.Parse(rdr.GetInt16(rdr.GetOrdinal("m20")).ToString()),
                        Int32.Parse(rdr.GetInt16(rdr.GetOrdinal("m10")).ToString()),
                        Int32.Parse(rdr.GetInt16(rdr.GetOrdinal("m5")).ToString()),
                        Int32.Parse(rdr.GetInt16(rdr.GetOrdinal("m1")).ToString()),

                        Double.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("unbalance")).ToString()),
                        Double.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("grossMoney")).ToString()),
                        Double.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("discountMoney")).ToString()),
                        Double.Parse(rdr.GetSqlMoney(rdr.GetOrdinal("moneyInBox")).ToString()),
                        Boolean.Parse(rdr.GetBoolean(rdr.GetOrdinal("isBoxClose")).ToString()),
                        DateTime.Parse(rdr.GetSqlDateTime(rdr.GetOrdinal("wasCreated")).ToString())
                    ));
                }
                if (rdr != null) rdr.Close();
                return ans;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
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
            catch(Exception e)
            {
                MessageBox.Show("error: " + e.ToString());
                if(conn != null && !is_table)
                {
                    conn.Close();
                }
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
