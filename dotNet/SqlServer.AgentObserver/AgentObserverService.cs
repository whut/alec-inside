using System;
using System.ServiceProcess;
using System.Threading;
using System.Configuration;
using System.Diagnostics;

namespace SqlServer.AgentObserver {
	public partial class AgentObserverService : ServiceBase {
		private Timer timer;
		private readonly object syncLock = new object();

		public AgentObserverService() {
			InitializeComponent();
		}

		protected override void OnStart( string[] args ) {
			try {
				int periodMinutes = int.Parse( ConfigurationManager.AppSettings[ "InquiryPeriod" ] );
				int period = Convert.ToInt32( TimeSpan.FromMinutes( periodMinutes ).TotalMilliseconds );
				timer = new Timer( OnTimerTick, null, 0, period );
			}
			catch ( Exception ex ) {
				this.EventLog.WriteEntry( ex.Message, EventLogEntryType.Error );				
			}
		}

		protected override void OnStop() {
			if ( timer != null ) {
				timer.Dispose();
			}
		}

		private void OnTimerTick( object state ) {			
			lock ( syncLock ) {
				try {
					JobServerInspector jobServer = new JobServerInspector();
					ServiceManager manager = new ServiceManager( jobServer.GetServiceName() );
					if ( manager.CheckServiceRunning() && jobServer.HasJobHangs() ) {
						jobServer.Dispose();
						manager.RestartService();
					}
				}
				catch ( Exception ex ) {
					this.EventLog.WriteEntry( ex.Message, EventLogEntryType.Error );
				}
			}

		}

	}
}
