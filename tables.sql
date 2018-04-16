use mym
create table Employee(
	employeeID int not null identity(1,1),
	name varchar(30),
	nationalID varchar(11),
	birthday datetime,
	cellphone varchar(17),
	telephone varchar(17),
	email varchar(30),
	address varchar(40),

	wasCreated datetime,

	password varchar(15),

	maxDiscountAvaible int,

	isAdmin bit,
	couldModifyEmp bit,
	couldModifyOthersCashBox bit,
	couldModifyProcess bit,

	couldModifyServices bit,
	couldModifyPayments bit,
	couldModifyExpenses bit,

	isActive bit,

	constraint _pk_employeeID primary key clustered(employeeID)
)
create table Scheduler(
	schedulerID int not null identity(1,1),
	arriveTime datetime,
	leaveTime datetime,
	fk_employeeID int,
	constraint _pk_schedulerID primary key clustered(schedulerID),
	constraint _fk_employeeID foreign key(fk_employeeID) references Employee(employeeID)
)
create table Customer(
	customerID int not null identity(1,1),

	name varchar(30),
	nationalID varchar(11),
	birthday datetime,
	cellphone varchar(17),
	telephone varchar(17),
	email varchar(30),
	address varchar(40),
	sendWhatsapp bit,
	sendEmail bit,
	wasCreated datetime,
	fk_createdBy int,
	fk_modifiedBy int,

	constraint _pk_customerID primary key clustered(customerID),
	constraint _fk_createdBy foreign key(fk_createdBy) references Employee(employeeID),
	constraint _fk_modifiedBy foreign key(fk_modifiedBy) references Employee(employeeID)
)

create table Service(
	serviceID int not null identity(1,1),
	name varchar(15),
	wasCreated datetime,

	constraint _pk_serviceID primary key clustered(serviceID)
)

create table Cloth(
	clothID int not null identity(1,1),
	gender int,
	name varchar(15),
	amountPerCloth int,
	price money,
	wasCreated datetime,
	fk_serviceID int,
	constraint _pk_clothID primary key clustered(clothID),	
	constraint _fk_serviceID foreign key(fk_serviceID) references Service(serviceID)
)

create table Orders(
	orderID int not null identity(1,1),
 
	totalPrice money,
	discount int,
	customerPayment money,
	observations varchar(50),
	ticketID int,
	quantity int,

	clothQuantity int,

	wasCreated datetime,
	expectedFinishDate datetime,
	finishDate datetime,

	isDelivered bit,
	orderState int,
	fk_employeeID int,
	fk_customerID int,
	constraint _pk_orderID primary key clustered(orderID),
	constraint _fk_employeeID_orders foreign key(fk_employeeID) references Employee(employeeID),	
	constraint _fk_customerID_orders foreign key(fk_customerID) references Customer(customerID)

)
create table DeliveryPosition(
	deliveryID int not null identity(1,1),
	isFree bit,
	wasCreated datetime,
	fk_orderID int,
	constraint _pk_deliveryID primary key clustered(deliveryID),
	constraint _fk_orderID_dev foreign key(fk_orderID) references Orders(orderID)	
)
create table OrderDetail(
	orderDetailID int not null identity(1,1),

	quantity int,
	price money,
	wasCreated datetime,
	fk_orderID int,
	fk_clothID int,

	constraint _pk_orderDetailID primary key clustered(orderDetailID),
	constraint _fk_orderID_det foreign key(fk_orderID) references Orders(orderID),	
	constraint _fk_clothID_det foreign key(fk_clothID) references Cloth(clothID)
)
create table Payment(
	paymentID int not null identity(1,1),

	paymentType int,
	amount money,
	wasCreated datetime,
	fk_employeeID int,
	fk_orderID int,

	constraint _pk_paymentID primary key clustered(paymentID),
	constraint _fk_employeeID_p foreign key(fk_employeeID) references Employee(employeeID),	
	constraint _fk_orderID_p foreign key(fk_orderID) references Orders(orderID)	
)

create table Expense(
	expenseID int not null identity(1,1),

	description varchar(40),
	price money,
	wasCreated datetime,
	fk_employeeID int, 
	constraint _pk_expenseID primary key clustered(expenseID),
	constraint _fk_employeeID_exp foreign key(fk_employeeID) references Employee(employeeID)
)

create table CashBox(
	cashboxID int not null identity(1,1),
	
	cashInDebitCard money,
	cashInCreditCard money,
	cashInCheck money,

	m2000 smallmoney,
	m1000 smallmoney,
	m500 smallmoney,
	m200 smallmoney,
	m100 smallmoney,
	m50 smallmoney,
	m25 smallmoney,
	m20 smallmoney,
	m10 smallmoney,
	m5 smallmoney,
	m1 smallmoney,

	leftOver int,
	grossMoney money,
	discountMoney money,

	moneyInBox money,

	isBoxClose bit,
	wasCreated datetime,
	fk_employeeID int,


	constraint _pk_cashboxID primary key clustered(cashboxID),
	constraint _fk_employeeID_cabx foreign key(fk_employeeID) references Employee(employeeID)	
)

