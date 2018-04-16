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

        int m2000;
        int m1000;
        int m500;
        int m200;
        int m100;
        int m50;
        int m25;
        int m20;
        int m10;
        int m5;
        int m1;

        double leftOver;
        double groosMoney;
        double discountMoney;

        double moneyInBox;

        bool isBoxClose;

        public CashBox(int id, int employeeId, double cashInDebitCard, double cashInCreditCard, double cashInCheck, int m2000, int m1000, int m500, int m200, int m100, int m50, int m25, int m20, int m10, int m5, int m1, double leftOver, double groosMoney, double discountMoney, double moneyInBox, bool isBoxClose)
        {
            this.id = id;
            this.employeeId = employeeId;
            this.cashInDebitCard = cashInDebitCard;
            this.cashInCreditCard = cashInCreditCard;
            this.cashInCheck = cashInCheck;
            this.M2000 = m2000;
            this.M1000 = m1000;
            this.M500 = m500;
            this.M200 = m200;
            this.M100 = m100;
            this.M50 = m50;
            this.M25 = m25;
            this.M20 = m20;
            this.M10 = m10;
            this.M5 = m5;
            this.M1 = m1;
            this.LeftOver = leftOver;
            this.GroosMoney = groosMoney;
            this.DiscountMoney = discountMoney;
            this.MoneyInBox = moneyInBox;
            this.IsBoxClose = isBoxClose;
        }

        public int M2000 { get => m2000; set => m2000 = value; }
        public int M1000 { get => m1000; set => m1000 = value; }
        public int M500 { get => m500; set => m500 = value; }
        public int M200 { get => m200; set => m200 = value; }
        public int M100 { get => m100; set => m100 = value; }
        public int M50 { get => m50; set => m50 = value; }
        public int M25 { get => m25; set => m25 = value; }
        public int M20 { get => m20; set => m20 = value; }
        public int M10 { get => m10; set => m10 = value; }
        public int M5 { get => m5; set => m5 = value; }
        public int M1 { get => m1; set => m1 = value; }
        public double LeftOver { get => leftOver; set => leftOver = value; }
        public double GroosMoney { get => groosMoney; set => groosMoney = value; }
        public double DiscountMoney { get => discountMoney; set => discountMoney = value; }
        public double MoneyInBox { get => moneyInBox; set => moneyInBox = value; }
        public bool IsBoxClose { get => isBoxClose; set => isBoxClose = value; }
    }
}
