using System;
using System.IO;
using System.Collections.Generic;

namespace CSharpParser {
	/// <summary>
	/// Represents a collector that can collect "where T : class".
	/// </summary>
	public sealed class ColonCollector : Collector {

		public ColonCollector( Dictionary<int, Collector> internalCollectors ) {
			this.collectors = internalCollectors;
		}

		public override int ListenFor {
			get {
				return ':';
			}
		}

		public override void Collect( TextReader reader ) {
			int currentSymbol;
			do {
				currentSymbol = reader.Read();
				if ( collectors.ContainsKey( currentSymbol ) ) {
					collectors[ currentSymbol ].Collect( reader );
				}
			}
			while ( currentSymbol >= 0 && !IsStopSymbol( currentSymbol ) );
		}

		private bool IsStopSymbol( int symbol ) {
			return ( symbol == '{' || symbol == ';' || symbol == ']' );
		}

		private Dictionary<int, Collector> collectors;
	}
}
