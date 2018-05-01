using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LavanderiaMyM
{
    public enum State
    {
        Received = 0x0,
        Processed = 0x1,
        Finish = 0x2,
        Delivered = 0x3
    };

    class Order
    {
        int orderID;
        int employeeID;
        int customerID;
        double totalPrice;
        int discount;
        double customerPayment;
        string observations;
        int ticketID;
        int quantity;
        int clothQuantity;
        DateTime wasCreated;
        DateTime expectedFinish;
        bool isDelivered;
        State orderState;
        List<OrderDetail> orderDetail;

        public Order(int employeeID, int customerID, double totalPrice, int discount, string observations, int clothQuantity, DateTime expectedFinish, int quantity)
        {
            EmployeeID = employeeID;
            CustomerID = customerID;
            TotalPrice = totalPrice;
            Discount = discount;
            Observations = observations;
            ClothQuantity = clothQuantity;
            ExpectedFinish = expectedFinish;
            OrderState = State.Received;
            Quantity = quantity;
            OrderDetail = new List<OrderDetail>();
        }

        public int OrderID { get => orderID; set => orderID = value; }
        public int EmployeeID { get => employeeID; set => employeeID = value; }
        public int CustomerID { get => customerID; set => customerID = value; }
        public double TotalPrice { get => totalPrice; set => totalPrice = value; }
        public int Discount { get => discount; set => discount = value; }
        public double CustomerPayment { get => customerPayment; set => customerPayment = value; }
        public string Observations { get => observations; set => observations = value; }
        public int TicketID { get => ticketID; set => ticketID = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public int ClothQuantity { get => clothQuantity; set => clothQuantity = value; }
        public DateTime WasCreated { get => wasCreated; set => wasCreated = value; }
        public DateTime ExpectedFinish { get => expectedFinish; set => expectedFinish = value; }
        public bool IsDelivered { get => isDelivered; set => isDelivered = value; }
        public State OrderState { get => orderState; set => orderState = value; }
        internal List<OrderDetail> OrderDetail { get => orderDetail; set => orderDetail = value; }
    }

    class OrderDetail
    {
        int orderDetailID;
        int orderID;
        int clothID;
        int quantity;
        double price;
        DateTime wasCreated;

        public OrderDetail(int orderID, int clothID, int quantity, double price)
        {
            OrderID = orderID;
            ClothID = clothID;
            Quantity = quantity;
            Price = price;
        }

        public int OrderDetailID { get => orderDetailID; set => orderDetailID = value; }
        public int OrderID { get => orderID; set => orderID = value; }
        public int ClothID { get => clothID; set => clothID = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public double Price { get => price; set => price = value; }
        public DateTime WasCreated { get => wasCreated; set => wasCreated = value; }
    }
}
