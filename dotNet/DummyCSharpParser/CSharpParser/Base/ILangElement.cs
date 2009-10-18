using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpParser.Base {
	public interface ILangElement {
		/// <summary>
		/// Indicates whether that the specified character is line terminator.
		/// </summary>
		/// <param name="token">The character to verify is line terminator.</param>
		bool IsLineTerminator( int token );

		/// <summary>
		/// Indicates whether the specified character is categorized as a part of identifier
		/// or keyword. 
		/// </summary>
		/// <param name="symbol">A character.</param>
		/// <param name="nextSymbol">A next character.</param>
		bool IsIdentifierOrKeyword( int symbol, int nextSymbol );
	}
}
