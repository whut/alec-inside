using System;
using System.IO;
using CSharpParser.Base;

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

		public override void Collect( Context context ) {
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
