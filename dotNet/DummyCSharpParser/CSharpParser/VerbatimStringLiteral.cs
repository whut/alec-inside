using System;
using System.IO;

namespace CSharpParser {
	/// <summary>
	/// Represents a collector that can collect a verbatim string literal.
	/// </summary>
	public sealed class VerbatimStringLiteral : Collector {
		public override int ListenFor {
			get {
				return '@';
			}
		}

		public override void Collect( TextReader reader ) {
			if ( reader.Peek() == '"' ) {
				reader.Read(); // skip start quote
				int currentSymbol;
				do {
					currentSymbol = reader.Read();
					// skip escaped quote
					if ( currentSymbol == '"' && reader.Peek() == '"' ) {
						reader.Read();
						currentSymbol = 0;
					}
				} 
				while ( currentSymbol >= 0 && currentSymbol != '"' );
			}
		}
	}
}
