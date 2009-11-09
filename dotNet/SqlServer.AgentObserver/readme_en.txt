AgentObserver checks for hangs Sql Server Jobs and restart SQL Agent service if needed.


Installation:
 *This program require Microsoft .Net Framework 2.0 or high.
1. Copy files to any folder of computer with Sql Server Agent
2. Run Install.bat
3. Enter login (e.g. ".\admistrator" or "domain\administrator" )and password in dialog
4. Start Windows service "Agent Observer for SQL Server". 
5. Edit file SqlServer.AgentObserver.exe.config

	* MissingTimeToNextJobRun (integer) - maximum number of minutes 
	elapsed after next planned job running time that detects job hangs.
	* InquiryPeriod (integer) - period in minutes of jobs status poll.
	* SQLServerInstanceName - instance name (without server name).