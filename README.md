# ASP.NET-MVC-Trial-2
MVC CRUD app with Entity Framework 6 Database First Scaffold

Followed the tutorial in this [link](https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/database-first-development/)

### Some Notes:
1. In [this](https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/database-first-development/customizing-a-view) 
part of the tutorial, it was not mentioned how to pass a single student resource and multple enrollments resource related to
the student. For this, I created a view model named "StudentViewModel.cs" which has two data members - first is the student resource and 
the second is a list of enrollments resource. This view model is then used in the "Details" action method of the "Students" controller
to pass both the student and its enrollments to the ViewResult.
