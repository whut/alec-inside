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
			do {
				context.Collect( context.Reader.Read() );
			} while ( !IsStopSymbol( context.Reader.Peek() ) );
		}

		private static bool IsStopSymbol( int symbol ) {
			return ( 
				symbol == -1 || 
				symbol == '{' || 
				symbol == ';' || 
				symbol == ']' );
		}

	}
}
