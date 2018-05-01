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
        DataChecker dataChecker = new DataChecker();
        TextBox[] allMoneyTexbox, allCustomerTextBox;
        bool isInsertion;
        List<Customer> customerArray;

        public PrincipalMenu()
        {
            InitializeComponent();
            isInsertion = true;
            principalTabControl.SelectedIndex = 2;
            listViewCashBox.View = View.Details;
            listViewCashBox.GridLines = true;
            listViewCashBox.Columns.Add("Fecha", 120, HorizontalAlignment.Left);
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

            listViewCustomer.View = View.Details;
            listViewCustomer.GridLines = true;
            listViewCustomer.Columns.Add("ID",-2, HorizontalAlignment.Left);
            listViewCustomer.Columns.Add("Nombre", 80, HorizontalAlignment.Left);
            listViewCustomer.Columns.Add("Cedula/RNC", 80, HorizontalAlignment.Left);
            listViewCustomer.Columns.Add("Celular", 80, HorizontalAlignment.Left);
            listViewCustomer.Columns.Add("Telefono", 80, HorizontalAlignment.Left);
            listViewCustomer.Columns.Add("email", 80, HorizontalAlignment.Left);
            listViewCustomer.Columns.Add("Recibe Whatsapp/Email", -2, HorizontalAlignment.Left);

            allMoneyTexbox = new TextBox[] {
            m2000_textBox,
            m1000_textBox, m500_textBox,
            m200_textBox, m100_textBox,
            m50_textBox, m25_textBox,
            m20_textBox, m10_textBox,
            m5_textBox, m1_textBox};
            allCustomerTextBox = new TextBox[] {
                customer_name_textBox,
                customer_id_textBox,
                customer_cel_textBox,
                customer_tel_textBox,
                customer_email_textBox,
                customer_search_for_textBox
            };

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
            //Cleaning CashBox View
            tarjDeb_textBox.Text = ""; tarjCre_textBox.Text = "";
            tarjCh_textBox.Text = "";
            foreach (TextBox a in allMoneyTexbox)
                a.Text = "";
            EnterCashButton.Text = "Ingresar Cuadre";
            isInsertion = true;

            //Cleaning Customer View
            foreach (TextBox a in allCustomerTextBox)
                a.Text = "";
            customer_notes_textBox.Text = "";
            customer_add_button.Text = "Agregar";
          
        }
        private void EnterCashButton_Click(object sender, EventArgs e)
        {
            CashBox cashBox = new CashBox(currentEmployee.Id);
            bool checker = cashBox.getMoney(tarjDeb_textBox.Text, tarjCre_textBox.Text,
                tarjCh_textBox.Text, m2000_textBox.Text, m1000_textBox.Text, m500_textBox.Text,
                m200_textBox.Text, m100_textBox.Text, m50_textBox.Text, m25_textBox.Text,
                m20_textBox.Text, m10_textBox.Text, m5_textBox.Text, m1_textBox.Text);
            if (checker)
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

        private void eraseCashButton_Click(object sender, EventArgs e) => cleanData();

        private void CashSearchButton_Click(object sender, EventArgs e)
        {
            isInsertion = false;
            List<CashBox> CashBoxArray = database.getCashBox(currentEmployee.Id,
                cashBoxdateTimePicker1.Value.Date, cashBoxdateTimePicker2.Value.Date);
            if (CashBoxArray == null) return;
            foreach (CashBox cashBox in CashBoxArray)
            {
                string balance = "Exacto", openCashBox = "abierta";
                if (cashBox.LeftOver > 0) balance = "Sobran " + cashBox.LeftOver.ToString();
                else if (cashBox.LeftOver < 0) balance = "Faltan " + cashBox.LeftOver.ToString();

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

        private void ListViewCashBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }

        private void Search_for_textBox_TextChanged(object sender, EventArgs e)
        {
            customerArray = new List<Customer>(); 
            if (customer_search_for_textBox.Text != "")
            {
                switch(customer_search_type_comboBox.SelectedIndex)
                {
                    case 0: // Name
                        customerArray = database.SearchCustomerByName(customer_search_for_textBox.Text);
                        break;
                    case 1: // NationalID
                        customerArray = database.SearchCustomerByNationalID(customer_search_for_textBox.Text);
                        break;
                    case 2: // Phone
                        customerArray = database.SearchCustomerByPhone(customer_search_for_textBox.Text);
                        break;
                    case 3: // Email
                        customerArray = database.SearchCustomerByEmail(customer_search_for_textBox.Text);
                        break;
                    default:
                        break;
                }
            }
            listViewCustomer.Items.Clear();
            foreach (Customer customer in customerArray)
            {
                string whatsapp = "SI";
                if (customer.SendWhatsapp == false) whatsapp = "NO";
                string[] row ={
                        customer.Id.ToString(),
                        customer.Name,
                        customer.NationalID,
                        customer.Celphone1,
                        customer.Telephone,
                        customer.Email,
                        whatsapp
                    };
                listViewCustomer.Items.Add(new ListViewItem(row));
            }

        }
        private void SendToService()
        {
            if (listViewCustomer.SelectedItems.Count > 0)
            {
                int index = listViewCustomer.SelectedItems[0].Index;
                service_name_label.Text = customerArray[index].Name;
                service_id_label.Tag = customerArray[index].Id;
                service_id_label.Text = customerArray[index].NationalID;
                service_cel_label.Text = customerArray[index].Celphone1;
                service_tel_label.Text = customerArray[index].Telephone;
                service_email_label.Text = customerArray[index].Email;
                service_notes_label.Text = customerArray[index].Notes;
                service_discount_label.Text = customerArray[index].Discount.ToString() + " %";
                principalTabControl.SelectedIndex = 1;
            }
        }

        private void ListViewCustomer_DoubleClick(object sender, EventArgs e) => SendToService();
        private void Customer_initService_button_Click(object sender, EventArgs e) => SendToService();

        private void Customer_modify_button_Click(object sender, EventArgs e)
        {
            if(listViewCustomer.SelectedItems.Count > 0)
            {
                int index = listViewCustomer.SelectedItems[0].Index;
                customer_add_button.Text = "Guardar cambios";
                customer_name_textBox.Text = customerArray[index].Name;
                customer_name_textBox.Tag = customerArray[index].Id;
                customer_id_textBox.Text = customerArray[index].NationalID;
                customer_cel_textBox.Text = customerArray[index].Celphone1;
                customer_tel_textBox.Text = customerArray[index].Telephone;
                customer_email_textBox.Text = customerArray[index].Email;
                customer_dateTimePicker.Value = customerArray[index].Birthday;
                customer_allowedWS_checkBox.Checked = customerArray[index].SendWhatsapp;
                customer_notes_textBox.Text = customerArray[index].Notes;
            }
        }

        private void ListViewCustomer_DoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void customer_notes_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label46_Click(object sender, EventArgs e)
        {

        }


        private void Customer_add_button_Click(object sender, EventArgs e)
        {
            if (customer_name_textBox.Text == "" ||
                  !dataChecker.CheckNumeric(customer_id_textBox.Text) ||
                  !dataChecker.CheckPhone(customer_cel_textBox.Text) ||
                  !dataChecker.CheckPhone(customer_tel_textBox.Text) ||
                  !dataChecker.CheckEmail(customer_email_textBox.Text))
                MessageBox.Show("Hay un error en los campos introducidos");
            else
            {
                bool wasSuccessful = false;
                if (customer_add_button.Text == "Agregar")
                {
                    wasSuccessful = database.InsertCustomer(currentEmployee.Id,
                            new Customer(customer_name_textBox.Text,
                            customer_id_textBox.Text, customer_dateTimePicker.Value.Date,
                            customer_cel_textBox.Text, customer_tel_textBox.Text,
                            customer_allowedWS_checkBox.Checked, customer_email_textBox.Text,
                            0, customer_notes_textBox.Text));                 
                }
                else
                {
                    wasSuccessful = database.ModifyCustomer(currentEmployee.Id,
                           new Customer(customer_name_textBox.Text,
                           customer_id_textBox.Text, customer_dateTimePicker.Value.Date,
                           customer_cel_textBox.Text, customer_tel_textBox.Text,
                           customer_allowedWS_checkBox.Checked, customer_email_textBox.Text,
                           0, customer_notes_textBox.Text)
                           {
                               Id = Int32.Parse(customer_name_textBox.Tag.ToString())
                           });
                }
                if (wasSuccessful)
                {
                    MessageBox.Show("Cliente agregado/modificado");
                    cleanData();
                }
            }
        }
    }
}
