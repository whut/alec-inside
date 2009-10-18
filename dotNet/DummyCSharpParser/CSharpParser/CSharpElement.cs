using System;
using CSharpParser.Base;

namespace CSharpParser {
	public class CSharpElement : ILangElement {
		/// <summary>
		/// Indicates whether that the specified character is line terminator.
		/// </summary>
		/// <param name="token">The character to verify is line terminator.</param>
		public bool IsLineTerminator( int token ) {
			switch ( token ) {
				case '\u000D':
				case '\u000A':
				case '\u0085':
				case '\u2028':
				case '\u2029':
					return true;
			}
			return false;
		}

		/// <summary>
		/// Indicates whether the specified Unicode character is categorized as a part of C# identifier
		/// or keyword. 
		/// </summary>
		/// <param name="symbol">A Unicode character.</param>
		/// <param name="nextSymbol">A next Unicode character.</param>
		public bool IsIdentifierOrKeyword( int symbol, int nextSymbol ) {
			return Char.IsLetterOrDigit( Convert.ToChar( symbol ) ) || symbol == '_' ||
				( symbol == '@' && nextSymbol != '"' );
		}

	}
}
