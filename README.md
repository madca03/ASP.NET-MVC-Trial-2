# ASP.NET-MVC-Trial-2

MVC CRUD app with Entity Framework 6 Database First Scaffold

Followed the tutorial in this [link][tutorial-link]

## Notes for using MSSQL Server Express for the tutorial

1. In [this][setting-up-db] part of the tutorial, instead of creating an SQL Server Database Project, we'll use the Microsoft SQL Server Express for our database. First login to your SQL Server then create a database. Create the tables **student**, **course**, and **enrollment** using the T-SQL scripts below. After creating the tables, generate the seed data. The T-SQL script for the seed data is also shown below.

   - image showing the creation of table in MS SQL Server Express

     ![creating table in MSSQL][create-table-mssql]

   - T-SQL script for creating **student** table

     ```SQL
     CREATE TABLE [dbo].[Student] (
         [StudentID]      INT           IDENTITY (1, 1) NOT NULL,
         [LastName]       NVARCHAR (50) NULL,
         [FirstName]      NVARCHAR (50) NULL,
         [EnrollmentDate] DATETIME      NULL,
         PRIMARY KEY CLUSTERED ([StudentID] ASC)
     )
     ```

   - Create **course** table

     ```SQL
     CREATE TABLE [dbo].[Course] (
         [CourseID] INT           IDENTITY (1, 1) NOT NULL,
         [Title]    NVARCHAR (50) NULL,
         [Credits]  INT           NULL,
         PRIMARY KEY CLUSTERED ([CourseID] ASC)
     )
     ```

   - Create **enrollment** table

     ```SQL
     CREATE TABLE [dbo].[Enrollment] (
         [EnrollmentID] INT IDENTITY (1, 1) NOT NULL,
         [Grade]        DECIMAL(3, 2) NULL,
         [CourseID]     INT NOT NULL,
         [StudentID]    INT NOT NULL,
         PRIMARY KEY CLUSTERED ([EnrollmentID] ASC),
         CONSTRAINT [FK_dbo.Enrollment_dbo.Course_CourseID] FOREIGN KEY ([CourseID])
             REFERENCES [dbo].[Course] ([CourseID]) ON DELETE CASCADE,
         CONSTRAINT [FK_dbo.Enrollment_dbo.Student_StudentID] FOREIGN KEY ([StudentID])
             REFERENCES [dbo].[Student] ([StudentID]) ON DELETE CASCADE
     )
     ```

   - Generate seed data for the **student**, **course**, and **enrollment** tables

     ```SQL
     MERGE INTO Course AS Target
     USING (VALUES
             (1, 'Economics', 3),
             (2, 'Literature', 3),
             (3, 'Chemistry', 4)
     )
     AS Source (CourseID, Title, Credits)
     ON Target.CourseID = Source.CourseID
     WHEN NOT MATCHED BY TARGET THEN
     INSERT (Title, Credits)
     VALUES (Title, Credits);

     MERGE INTO Student AS Target
     USING (VALUES
             (1, 'Tibbetts', 'Donnie', '2013-09-01'),
             (2, 'Guzman', 'Liza', '2012-01-13'),
     (3, 'Catlett', 'Phil', '2011-09-03')
     )
     AS Source (StudentID, LastName, FirstName, EnrollmentDate)
     ON Target.StudentID = Source.StudentID
     WHEN NOT MATCHED BY TARGET THEN
     INSERT (LastName, FirstName, EnrollmentDate)
     VALUES (LastName, FirstName, EnrollmentDate);

     MERGE INTO Enrollment AS Target
     USING (VALUES
     (1, 2.00, 1, 1),
     (2, 3.50, 1, 2),
     (3, 4.00, 2, 3),
     (4, 1.80, 2, 1),
     (5, 3.20, 3, 1),
     (6, 4.00, 3, 2)
     )
     AS Source (EnrollmentID, Grade, CourseID, StudentID)
     ON Target.EnrollmentID = Source.EnrollmentID
     WHEN NOT MATCHED BY TARGET THEN
     INSERT (Grade, CourseID, StudentID)
     VALUES (Grade, CourseID, StudentID);
     ```

2. Add your MSSQL Server Express in Visual Studio. Follow the steps below.

   1. Open **SQL Server Object Explorer** in Visual Studio.
   2. Right-click on **SQL Server** and select **Add SQL Server**.
   3. In the **Connect** Dialog box, select the **Local** dropdown list then select for your local MSSQL Server Express. In the example below, it's **LAPTOP-VRPV043O\SQLEEXPRESS**

      ![adding MSSQL Express in Visual Studio][add-mssql-in-vs]

3. In [this][generate-the-models] part where we generate our models using Entity Framework, instead of using the localDB as our SQL server, we will use the local MS SQL Server Express. For instance, in the image shown below, the **Server name:** field is set to **LAPTOP-VRPV043O\SQLEEXPRESS** which is the local MS SQL Server Express in the machine.

   ![choosing MSSQL Express for EF6-DB-First][connect-to-mssql-from-vs]

4. If you're unable to find the **ADO.NET Entity Data Model** option as mentioned in this [link][generate-the-models], install the **Microsoft SQL Server Data Tools** as mentioned [here][setting-up-db]. For Visual Studio 2019, follow the instructions in this [link][install-ssdt] and install the **SQL Server Data Tools**. If you have already installed Visual Studio, you can install additional features by going to **Tools > Get Tools and Features**.

## Notes on passing a one-to-many resource from controller to view.

1. In [this](https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/database-first-development/customizing-a-view)
   part of the tutorial, it was not mentioned how to pass a single student resource and multple enrollments resource related to
   the student. For this, I created a view model named "StudentViewModel.cs" which has two data members - first is the student resource and
   the second is a list of enrollments resource. This view model is then used in the "Details" action method of the "Students" controller
   to pass both the student and its enrollments to the ViewResult.

[tutorial-link]: https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/database-first-development/
[setting-up-db]: https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/database-first-development/setting-up-database#set-up-the-database
[generate-the-models]: https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/database-first-development/creating-the-web-application#generate-the-models
[install-ssdt]: https://docs.microsoft.com/en-us/sql/ssdt/download-sql-server-data-tools-ssdt?redirectedfrom=MSDN&view=sql-server-ver15#install-ssdt-with-visual-studio-2019
[create-table-mssql]: ./img/creating-table-in-mssql.png
[add-mssql-in-vs]: ./img/add-mssql-in-vs.png
[connect-to-mssql-from-vs]: ./img/connect-to-mssql-from-vs.png
