# Cash Exchange Machine


## REST API
###### GET  **/api/cashmachine/money** 
Retrieve information about available money in machine

###### POST **/api/cashmachine/money** 
Set money in machine

###### PUT  **/api/cashmachine/insert/coin**/*nominal* 
Insert coin with *specified* nominal

###### PUT  **/api/cashmachine/insert/note**/*nominal* 
Insert note with *specified* nominal

###### POST **/api/cashmachine/exchange**
Make exchange of inserted money


## Build and Run
1. Open CashExchangeMachine.sln
2. Find app.config in 'CashExchangeMachine.WebApi.SelfHost' project and set:
 * SQL Server connection string (under path /configuration/connectionStrings/add/@name="sql")
 * base URL for REST server
2. Build solution (Ctrl+Shift+B)
3. Run CashExchangeMachine.WebApi.SelfHost.exe



