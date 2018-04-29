using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LavanderiaMyM
{
    class CashBox
    {
        int id;
        int employeeId;

        double cashInDebitCard;
        double cashInCreditCard;
        double cashInCheck;
        int[] moneyValue = { 2000, 1000, 500, 200, 100, 50, 25, 20, 10, 5, 1};

        int[] money = new int [11];
        double leftOver;
        double groosMoney;
        double discountMoney;

        double moneyInBox;
        DateTime wasCreated;
        DataChecker dataChecker = new DataChecker();

        bool isBoxClose;

        public CashBox(int employeeId)
        {
            EmployeeId = employeeId;
        }
        public CashBox(int id, int employeeId, double cashInDebitCard, double cashInCreditCard, 
            double cashInCheck, int m2000, int m1000, int m500, int m200, int m100, int m50, int m25, 
            int m20, int m10, int m5, int m1, double leftOver, double groosMoney, double discountMoney, 
            double moneyInBox, bool isBoxClose, DateTime wasCreated)
        {
            Id = id;
            EmployeeId = employeeId;
            CashInDebitCard = cashInDebitCard;
            CashInCreditCard = cashInCreditCard;
            CashInCheck = cashInCheck;
            Money[10] = m2000;
            Money[9] = m1000;
            Money[8] = m500;
            Money[7] = m200;
            Money[6] = m100;
            Money[5] = m50;
            Money[4] = m25;
            Money[3] = m20;
            Money[2] = m10;
            Money[1] = m5;
            Money[0] = m1;
            LeftOver = leftOver;
            GroosMoney = groosMoney;
            DiscountMoney = discountMoney;
            MoneyInBox = moneyInBox;
            IsBoxClose = isBoxClose;
            WasCreated = wasCreated;
            calculateMoneyInBox();
        }
        public void calculateMoneyInBox()
        {
            MoneyInBox = 0;
            for (int i = 0; i < Money.Length; i++)
                MoneyInBox += Money[i] * moneyValue[i];
        }
        public bool getMoney(string cashInDebitCard, string cashInCreditCard,
            string cashInCheck, params string[] coins)
        {
            if (coins.Length != Money.Length)
                return false;
            try
            {
                CashInDebitCard = dataChecker.ConvertStringtoInt(cashInDebitCard);
                CashInCreditCard = dataChecker.ConvertStringtoInt(cashInCreditCard);
                CashInCheck = dataChecker.ConvertStringtoInt(cashInCheck);
                for (int i = 0; i < Money.Length; i++)
                    Money[i] = Int32.Parse(coins[i]);
                calculateMoneyInBox();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public double LeftOver { get => leftOver; set => leftOver = value; }
        public double GroosMoney { get => groosMoney; set => groosMoney = value; }
        public double DiscountMoney { get => discountMoney; set => discountMoney = value; }
        public double MoneyInBox { get => moneyInBox; set => moneyInBox = value; }
        public bool IsBoxClose { get => isBoxClose; set => isBoxClose = value; }
        public int Id { get => id; set => id = value; }
        public int EmployeeId { get => employeeId; set => employeeId = value; }
        public double CashInDebitCard { get => cashInDebitCard; set => cashInDebitCard = value; }
        public double CashInCreditCard { get => cashInCreditCard; set => cashInCreditCard = value; }
        public double CashInCheck { get => cashInCheck; set => cashInCheck = value; }
        public DateTime WasCreated { get => wasCreated; set => wasCreated = value; }
        public int[] Money { get => money; set => money = value; }
    }
}
