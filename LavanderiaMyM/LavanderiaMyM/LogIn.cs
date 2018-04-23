using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LavanderiaMyM
{
    public partial class LogIn : Form
    {
        DataChecker check;
        Database database;
        public LogIn()
        {
            InitializeComponent();
            check = new DataChecker();
            database = new Database();
        }
        public delegate void sendValue(int id, string name, string username, LogIn logIn);

        private void button1_Click(object sender, EventArgs e)
        {
            string username = LogIn_username.Text.ToString();
            string password = LogIn_password.Text.ToString();
            if(check.checkString(username,30)&& check.checkString(password, 25))
            {
                Employee employee = database.authUser(username, password);
                if (employee.Name != null)
                {
                    PrincipalMenu principalMenu = new PrincipalMenu();
                    sendValue sendValue = new sendValue(principalMenu.receiveData);
                    sendValue(employee.Id, employee.Name, username, this);
                    principalMenu.Show();
                    this.Visible = false;
                }
                else
                    labelWrongPassword.Visible = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
