﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;


namespace SqlServer.AgentObserver {
	[RunInstaller( true )]
	public partial class ProjectInstaller : Installer {
		public ProjectInstaller() {
			InitializeComponent();
		}
	}
}
