using System;
using System.IO;
using System.Collections.Generic;
using CSharpParser.Base;

namespace CSharpParser {
	/// <summary>
	/// Represents a collector that can collect "where T : class".
	/// </summary>
	public sealed class ColonCollector : Collector {
		
		public override int ListenFor {
			get {
				return ':';
			}
		}

		public override void Collect( Context context ) {
			int currentSymbol;
			do {
				currentSymbol = context.Reader.Read();
				if ( context.Collectors.ContainsKey( currentSymbol ) ) {
					context.Collectors[ currentSymbol ].Collect( context );
				}
			}
			while ( currentSymbol >= 0 && !IsStopSymbol( currentSymbol ) );
		}

		private bool IsStopSymbol( int symbol ) {
			return ( symbol == '{' || symbol == ';' || symbol == ']' );
		}

	}
}
