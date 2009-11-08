using System;


namespace CXParser.Collectors {
	/// <summary>
	/// Represents a collector that can collect a verbatim string literal.
	/// </summary>
	public sealed class VerbatimStringLiteralCollector : ICollector {
		public int ListenFor {
			get {
				return '@';
			}
		}

		public void Collect( Context context ) {
			if ( context.Reader.Peek() == '"' ) {
				context.Reader.Read(); // skip start quote
				int currentSymbol;
				do {
					currentSymbol = context.Reader.Read();
					// skip escaped quote
					if ( currentSymbol == '"' && context.Reader.Peek() == '"' ) {
						context.Reader.Read();
						currentSymbol = 0;
					}
				}
				while ( currentSymbol >= 0 && currentSymbol != '"' );
			}			
		}
	}
}
