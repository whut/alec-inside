using System;
using Microsoft.SqlServer.Management.Smo.Wmi;
using System.Threading;

namespace SqlServer.AgentObserver {
	class ServiceManager {

		private string serviceName;
		public ServiceManager( string serviceName ) {
			this.serviceName = serviceName;
                                                     
		}
		public bool CheckServiceRunning() {
			Service agent = GetAgentService();
			if ( agent != null ) {
				if ( agent.ServiceState == ServiceState.Stopped ) {
					agent.Start();
					return false;
				}
				if ( agent.ServiceState == ServiceState.Running ) {
					return true;
				}
			}
			return false;
		}

		public void RestartService() {
			Service agent = GetAgentService();
			if ( agent != null && agent.ServiceState == ServiceState.Running ) {
				agent.Stop();
				while ( agent.ServiceState != ServiceState.Stopped ) {
					Thread.Sleep( 100 );
					agent.Refresh();
				}
				agent.Start();
			}
		}		

		private Service GetAgentService() {
			ManagedComputer computer = new ManagedComputer();			
			if ( computer.Services.Contains( serviceName ) ) {
				return computer.Services[ serviceName ];
			}
			return null;
		}
	}
}
