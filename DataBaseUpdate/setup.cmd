REM Create an instance of LocalDB
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\SqlLocalDB.exe" create MSSQLLocalDB11
REM Start the instance of LocalDB
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\SqlLocalDB.exe" start MSSQLLocalDB11
REM Gather information about the instance of LocalDB
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\SqlLocalDB.exe" info MSSQLLocalDB11