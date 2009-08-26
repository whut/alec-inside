using System;

namespace CSharpParser {
	public static class LineTerminator {
		/// <summary>
		/// Indicates whether that the specified character is line terminator.
		/// </summary>
		/// <param name="token">The character to verify is line terminator.</param>
		public static bool IsLineTerminator( int token ) {
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
		
	}
}
