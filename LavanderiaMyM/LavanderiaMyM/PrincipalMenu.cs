using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace LavanderiaMyM
{
    public partial class PrincipalMenu : Form
    {
        Employee currentEmployee;
        LogIn log;
        Database database = new Database();
        TextBox[] allMoneyTexbox;
        bool isInsertion;
        public PrincipalMenu()
        {
            InitializeComponent();
            isInsertion = true;
            
            listViewCashBox.View = View.Details;
            listViewCashBox.GridLines = true;
            listViewCashBox.Columns.Add("Fecha",120, HorizontalAlignment.Left);
            listViewCashBox.Columns.Add("ID", 40, HorizontalAlignment.Left);
            listViewCashBox.Columns.Add("Dinero En Caja", -2, HorizontalAlignment.Left);
            listViewCashBox.Columns.Add("Balance", 80, HorizontalAlignment.Left);
            listViewCashBox.Columns.Add("Estado caja ", -2, HorizontalAlignment.Left);
            listViewCashBox.Columns.Add("Tarj. Cred.", -2, HorizontalAlignment.Left);
            listViewCashBox.Columns.Add("Tarj. Deb.", -2, HorizontalAlignment.Left);
            listViewCashBox.Columns.Add("Tarj. Che.", -2, HorizontalAlignment.Left);
            listViewCashBox.Columns.Add("2000", -2, HorizontalAlignment.Left);
            listViewCashBox.Columns.Add("1000", -2, HorizontalAlignment.Left);
            listViewCashBox.Columns.Add("500", -2, HorizontalAlignment.Left);
            listViewCashBox.Columns.Add("200", -2, HorizontalAlignment.Left);
            listViewCashBox.Columns.Add("100", -2, HorizontalAlignment.Left);
            listViewCashBox.Columns.Add("50", -2, HorizontalAlignment.Left);
            listViewCashBox.Columns.Add("25", -2, HorizontalAlignment.Left);
            listViewCashBox.Columns.Add("20", -2, HorizontalAlignment.Left);
            listViewCashBox.Columns.Add("10", -2, HorizontalAlignment.Left);
            listViewCashBox.Columns.Add("5", 30, HorizontalAlignment.Left);
            listViewCashBox.Columns.Add("1", 30, HorizontalAlignment.Left);
            allMoneyTexbox = new TextBox[] {
            m2000_textBox,
            m1000_textBox, m500_textBox,
            m200_textBox, m100_textBox,
            m50_textBox, m25_textBox,
            m20_textBox, m10_textBox,
            m5_textBox, m1_textBox};
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
        private void cleanData()
        {
            tarjDeb_textBox.Text = ""; tarjCre_textBox.Text ="";
            tarjCh_textBox.Text = "";
            for (int i = 0; i < allMoneyTexbox.Length; i++)
                allMoneyTexbox[i].Text = "";
            EnterCashButton.Text = "Ingresar Cuadre";
            isInsertion = true;
        }
        private void EnterCashButton_Click(object sender, EventArgs e)
        {
            CashBox cashBox = new CashBox(currentEmployee.Id);
            bool checker = cashBox.getMoney(tarjDeb_textBox.Text, tarjCre_textBox.Text,
                tarjCh_textBox.Text, m2000_textBox.Text, m1000_textBox.Text, m500_textBox.Text, 
                m200_textBox.Text, m100_textBox.Text, m50_textBox.Text, m25_textBox.Text,
                m20_textBox.Text, m10_textBox.Text, m5_textBox.Text, m1_textBox.Text);
            if(checker)
            {
                MessageBox.Show(cashBox.Money.ToString());
                cleanData();
                if (isInsertion)
                {
                    cashBox = database.insertCashBox(cashBox);
                    if (cashBox != null)
                    {
                        if (cashBox.IsBoxClose)
                            MessageBox.Show("La Caja ha sido cerrada, el desbalance en la misma es de " + cashBox.LeftOver.ToString());
                        else MessageBox.Show("La Caja ha sido abierta");
                    }
                }
            }
            else
                MessageBox.Show("Los numeros introducidos son incorrectos.\n Favor Revisar");
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void tarjC_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void eraseCashButton_Click(object sender, EventArgs e)
        {
            cleanData();
        }

        private void cashSearchButton_Click(object sender, EventArgs e)
        {
            isInsertion = false;
            List<CashBox> CashBoxArray = database.getCashBox(currentEmployee.Id, 
                cashBoxdateTimePicker1.Value.Date, cashBoxdateTimePicker2.Value.Date);
            if (CashBoxArray == null) return;
            foreach (CashBox cashBox in CashBoxArray)
            {
                string balance = "Exacto", openCashBox = "abierta";
                if (cashBox.LeftOver > 0) balance = "Sobran " + cashBox.LeftOver.ToString();
                else if(cashBox.LeftOver < 0) balance = "Faltan " + cashBox.LeftOver.ToString();

                if (cashBox.IsBoxClose)
                    openCashBox = "cerrada";

                string[] row ={
                        cashBox.WasCreated.ToString(),
                        cashBox.Id.ToString(),
                        cashBox.MoneyInBox.ToString(),
                        balance,
                        openCashBox,
                        cashBox.CashInCreditCard.ToString(),
                        cashBox.CashInDebitCard.ToString(),
                        cashBox.CashInCheck.ToString(),
                    };

                string[] moneyString = new string[cashBox.Money.Length + row.Length];

                for (int i = row.Length; i < moneyString.Length; i++)
                    moneyString[i] = cashBox.Money[moneyString.Length - i - 1].ToString();

                row.CopyTo(moneyString, 0);
                listViewCashBox.Items.Add(new ListViewItem(moneyString));
            }
        }

        private void listViewCashBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }
    }
}
