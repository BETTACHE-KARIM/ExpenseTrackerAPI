# ExpenseTrackerAPI  

**ExpenseTrackerAPI** is a backend service built with **.NET 8** designed to manage budgets, expenses, and user notifications. It provides endpoints for users to create, track, and manage their budgets and expenses effectively.  

## Features  
- Manage user budgets with monthly tracking.  
- Record and monitor expenses linked to specific months and years.  
- Notify users when their total expenses exceed the allocated budget.  
- Full CRUD operations for budgets and expenses.  

## Requirements  
- **.NET 8 SDK**  
- **SQL Server** or compatible database  

## Setup Instructions  
1. Clone the repository:  
   ```bash  
   git clone https://github.com/BETTACHE-KARIM/ExpenseTrackerAPI.git
   cd ExpenseTrackerAPI  
   ```  

2. Update the connection string in `appsettings.json`:  
   ```json  
   "ConnectionStrings": {  
       "DefaultConnection": "YourDatabaseConnectionStringHere"  
   }  
   ```  

3. Apply database migrations:  
   ```bash  
   Add-Migration InitialCreate

   Update-Database
   ```  

4. Run the project:  
   ```bash  
   dotnet run  
   ```  

5. Access the API documentation at:  
   ```  
   http://localhost:<port>/swagger  
   ```  

## Additional Notes  
- Ensure your database server is running and accessible.  
- The API includes pre-configured Swagger documentation for easy testing.  
- Use a tool like **Postman** or **curl** to interact with the API endpoints.  
