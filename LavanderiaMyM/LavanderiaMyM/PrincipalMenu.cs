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
    public partial class PrincipalMenu : Form
    {
        Employee currentEmployee;
        LogIn log;

        public PrincipalMenu()
        {
            InitializeComponent();
        }
        public void receiveData(int id, string name, string username, LogIn logIn)
        {
            currentEmployee = new Employee(id, name, username, 0);
            log = logIn;
            labelCashBoxEmployeeName.Text = "Buenos días, " + name; 
        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
