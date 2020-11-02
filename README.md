# EmployeeDetail

## General info
This project is to create an application for registration and login of employee based on mobile number
	
## Technologies
Project is created with:
* ASP.Net
* SQL Server DB
	
  

## Setup
To run this project create DB as discribed in below in Database section of this file. Download the coide and run the solution. 
Both API and web forms are contained in same project. 

## Database  table and stored procedure user :

* DB name : Employee

***********************************************************
CREATE TABLE [dbo].[EmployeeDet](
	[EmployeeId] [int] IDENTITY(1,1)NOTNULL,
	[FirstName] [varchar](500)NULL,
	[LastName] [varchar](500)NULL,
	[MobileNo] [varchar](50)NOTNULL,
	[Email] [varchar](500)NULL,
	[EntryDate] [datetime] NULL,
CONSTRAINT [PK_EmployeeDet] PRIMARY KEY CLUSTERED
(
	[MobileNo] ASC
)WITH (PAD_INDEX=OFF,STATISTICS_NORECOMPUTE=OFF,IGNORE_DUP_KEY=OFF,ALLOW_ROW_LOCKS=ON,ALLOW_PAGE_LOCKS=ON)ON [PRIMARY]
)ON [PRIMARY]
GO

ALTER TABLE [dbo].[EmployeeDet] ADDCONSTRAINT [DF_EmployeeDet_EntryDate]  DEFAULT (getdate())FOR [EntryDate]
GO

***********************************************************
### Stored procedures created: 
***********************************************************
CREATE PROCEDURE [dbo].[AddEmployeeDet]
	@FirstName varchar(500),
	@LastName varchar(500),
	@Mobile varchar(50),
	@Email varchar(500),
	@returnVal varchar(500)OUTPUT
AS
BEGINTRY
BEGINTRANSACTION

BEGIN
SETNOCOUNTON
	insertinto EmployeeDet(FirstName, LastName, MobileNo, Email)values(@FirstName, @LastName, @Mobile, @Email)

END
set @returnVal='Successfully Uploaded'
COMMITTRAN-- Transaction Success!          
ENDTRY

BEGINCATCH
IF@@TRANCOUNT> 0
ROLLBACKTRAN--RollBack in case of Error 
set @returnVal=ERROR_MESSAGE()
ENDCATCH
Return
*****************************************************************
CREATE PROCEDURE [dbo].[GetUserInfo]
@EmpMobile varchar(50)
ASBEGIN
	select*from EmployeeDet where MobileNo=@EmpMobile
END
*****************************************************************
