using System;
using System.IO;

namespace CSharpParser {
	/// <summary>
	/// Represents a collector that can collect a regular string literal.
	/// </summary>
	public sealed class RegularStringLiteral : Collector {
		public override int ListenFor {
			get {
				return '"';
			}
		}
		public override void Collect( TextReader reader ) {
			int currentSymbol;
			do {
				currentSymbol = reader.Read();
				// skip escaped quote
				if ( currentSymbol == '\\' && reader.Peek() == '"' ) {
					reader.Read();
				}
			} 
			while ( currentSymbol >= 0 && currentSymbol != '"' );
		}
	}
}
