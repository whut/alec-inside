using System;

namespace CXParser.Collectors {
	/// <summary>
	/// Represents a collector that can collect "where T : class".
	/// </summary>
	public sealed class ColonCollector : ICollector {
		
		public int ListenFor {
			get {
				return ':';
			}
		}

		public void Collect( Context context ) {
			int currentSymbol;
			do {
				currentSymbol = context.Reader.Read();
				if ( context.Collectors.ContainsKey( currentSymbol ) ) {
					context.Collectors[ currentSymbol ].Collect( context );
				}
			}
			while ( currentSymbol >= 0 && 
				!IsStopSymbol( context.Reader.Peek() ) );
		}

		private bool IsStopSymbol( int symbol ) {
			return ( symbol == '{' || symbol == ';' || symbol == ']' );
		}

	}
}
