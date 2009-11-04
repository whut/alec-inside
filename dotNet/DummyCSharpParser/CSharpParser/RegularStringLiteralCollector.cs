using System;

namespace CXParser.Collectors {
	/// <summary>
	/// Represents a collector that can collect a regular string literal.
	/// </summary>
	public sealed class RegularStringLiteralCollector : ICollector {
		public int ListenFor {
			get {
				return '"';
			}
		}
		public void Collect( Context context ) {
			int currentSymbol;
			do {
				currentSymbol = context.Reader.Read();
				// skip escaped quote
				if ( currentSymbol == '\\' && context.Reader.Peek() == '"' ) {
					context.Reader.Read();
				}
			} 
			while ( currentSymbol >= 0 && currentSymbol != '"' );
		}
	}
}
