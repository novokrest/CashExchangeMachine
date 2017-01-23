# Cash Exchange Machine


## REST API
### GET  **/api/cashmachine/money** 
Retrieve information about available money in machine

### POST **/api/cashmachine/money** 
Set money in machine

### PUT  **/api/cashmachine/insert/coin**/*nominal* 
Insert coin with *specified* nominal

### PUT  **/api/cashmachine/insert/note**/*nominal* 
Insert note with *specified* nominal

### POST **/api/cashmachine/exchange**
Make exchange of inserted money


## Build and Run
1. Initialize SQL Server database with *InitCashDB.sql* script located in *sql* folder 
2. Open CashExchangeMachine.sln
3. Find app.config in 'CashExchangeMachine.WebApi.SelfHost' project and set:
 * SQL Server connection string (xpath: /configuration/connectionStrings/add/@name="sql")
 * base URL for REST server (xpath: /configuration/appSettings/add/@name="server")
4. Build solution (Ctrl+Shift+B)
5. Run CashExchangeMachine.WebApi.SelfHost.exe



