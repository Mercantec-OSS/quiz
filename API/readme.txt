How to make migrations and update the database

1. Open the Package Manager Console and navigate to /API folder.
	this console is located in tools -> NuGet Packet Manager -> Packet Manager Console

2. Run "dotnet ef migrations add {MigrationName}" and wait for it to finish.

3. Run "dotnet ef database update" and wait for it to finish.

NOTE: for this to work you need your appsetting to be up to date.