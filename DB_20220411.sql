
IF DB_ID(N'Abschlussprojekt_Fitnessstudio') IS NULL
    CREATE DATABASE Abschlussprojekt_Fitnessstudio;
GO
USE Abschlussprojekt_Fitnessstudio;
GO 


IF OBJECT_ID('Schedule') IS NOT NULL
    DROP TABLE Schedule;
GO
IF OBJECT_ID('Employee') IS NOT NULL
    DROP TABLE Employee;
GO
IF OBJECT_ID('TrainingMachinePlan') IS NOT NULL
    DROP TABLE TrainingMachinePlan;
GO
IF OBJECT_ID('TrainingMachine') IS NOT NULL
    DROP TABLE TrainingMachine;
GO
IF OBJECT_ID('CustomerAdress') IS NOT NULL
    DROP TABLE CustomerAdress;
GO

IF OBJECT_ID('Customer') IS NOT NULL
    DROP TABLE Customer;
GO
IF OBJECT_ID('TrainingPlan') IS NOT NULL
    DROP TABLE TrainingPlan;
GO
IF OBJECT_ID('Subscription') IS NOT NULL
    DROP TABLE Subscription;
GO
IF OBJECT_ID('Address') IS NOT NULL
    DROP TABLE Address;
GO





Create Table Address(
Id int not Null Primary Key Identity,
Streetname nvarchar(50),
City nvarchar(50),
Zipcode int,
StreetNumber nvarchar(50)
);
Go



--<TrainingsPlan Tabellen

Create Table TrainingMachine(
Id int not Null Primary Key Identity,
Name nvarchar(150)
);
Go

Create Table TrainingPlan(
TrainingPlanId int not Null Primary Key Identity,
);
Go

Create Table TrainingMachinePlan(
TrainingMachineId int,
TrainingPlanId int,
Iteration int,
Constraint fk_TrainingMachineId FOREIGN KEY (TrainingMachineId)
References TrainingMachine(Id),
Constraint fk_TrainingPlanId FOREIGN KEY (TrainingPlanId)
REFERENCES TrainingPlan(TrainingPlanId)
);
GO

--TrainingsPlan Tabellen>


Create Table Subscription(
Id int Not Null Primary Key identity,
Name nvarchar(50),
PricePerMonth float
);
GO

Create Table Employee(
ID int not Null Primary Key,
FirstName nvarchar(50),
LastName nvarchar(50),
Emailaddress nvarchar(256),
Createdate datetime2(7),
RemovedDate datetime2(7),
Password varchar(256) NOT NULL,
Address int
CONSTRAINT fk_EmployeeAddressId FOREIGN KEY (Address)
References Address(Id),
);
GO

Create Table Customer(
Id int not Null Primary Key,
FirstName nvarchar(50),
LastName nvarchar(50),
Birthday datetime2(7),
Emailaddress nvarchar(256),
Createdate datetime2(7),
RemovedDate datetime2(7),
TrainingPlanId int,
CONSTRAINT fk_CustomerTrainingPlanId FOREIGN KEY (TrainingPlanId)
References TrainingPlan(TrainingPlanId),
Subscription int
CONSTRAINT fk_CustomerSubscription FOREIGN KEY (Subscription)
References Subscription(Id)
);
Go

CREATE Table CustomerAddress(
AddressId int,
CustomerId int,
CONSTRAINT fk_AdressId FOREIGN KEY (AddressId)
References Address(Id),
CONSTRAINT fk_CustomerId FOREIGN KEY (CustomerId)
References Customer(Id)
);
GO


Create Table Schedule(
EmployeeId int,
CustomerId int,
StartTime DateTime2,
EndTime DateTime2
CONSTRAINT fk_ScheduleEmployeeId FOREIGN KEY (EmployeeId)
References Employee(Id),
CONSTRAINT fk_ScheduleCustomerId FOREIGN KEY (CustomerId)
References Customer(Id)
);
Go
