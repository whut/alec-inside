using System;
using System.IO;
using CSharpParser.Base;

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
		public override void Collect( Context context ) {
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
