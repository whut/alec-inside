using System;
using Microsoft.SqlServer.Management.Smo;
using System.Configuration;
using Microsoft.SqlServer.Management.Smo.Agent;

namespace SqlServer.AgentObserver {
	public class JobServerInspector : IDisposable {
		private Server server;
		public JobServerInspector() {
			string instanceName = ConfigurationManager.AppSettings[ "SQLServerInstanceName" ];
			server = String.IsNullOrEmpty( instanceName )
				? new Server()
				: new Server( instanceName );
		}

		public bool HasJobHangs() {
			foreach ( Job job in server.JobServer.Jobs ) {
				if ( job.CurrentRunStatus == JobExecutionStatus.Executing ) {
					int missingTime = int.Parse( ConfigurationManager.AppSettings[ "MissingTimeToNextJobRun" ] );
					double jobMissingTime = DateTime.Now.Subtract( job.NextRunDate ).TotalMinutes;
					if ( jobMissingTime > missingTime ) {
						return true;
					}
				}
			}
			return false;
		}

		public string GetServiceName() {
			if ( !String.IsNullOrEmpty( server.InstanceName ) ) {
				return "SQLAgent$" + server.InstanceName;
			}
			return "SQLSERVERAGENT";
		}

		#region IDisposable Members

		public void Dispose() {
			server = null;
		}

		#endregion
	}
}
