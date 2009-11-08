using System;
using System.Text;

namespace CXParser {
	public class CSharpTokenizer : ITokenizer {
		private StringBuilder token;
		private const char at = '@';

		public string ReadNext( Context context ) {
			token = new StringBuilder();

			for ( int nextSymbol = context.Reader.Peek();
				nextSymbol != -1;
				nextSymbol = context.Reader.Peek() ) {

				if ( nextSymbol == at ) {
					ProcessAt( context );
					continue;
				}

				if ( IsIdentifierOrKeywordSymbol( nextSymbol ) ) {
					token.Append( Convert.ToChar( context.Reader.Read() ) );
					continue;
				}

				if ( token.Length > 0 ) {
					return token.ToString();
				}

				context.Collect( context.Reader.Read() );
			}
			return null;
		}

		private void ProcessAt( Context context ) {
			context.Reader.Read();
			if ( IsIdentifierOrKeywordSymbol( context.Reader.Peek() ) ) {
				token.Append( at );
			}
			else {
				context.Collect( at );
			}
		}
		/// <summary>
		/// Indicates whether the specified Unicode character is categorized 
		/// as a part of C# identifier or keyword. 
		/// </summary>
		/// <param name="symbol">A Unicode character.</param>		
		private bool IsIdentifierOrKeywordSymbol( int symbol ) {
			return Char.IsLetterOrDigit( Convert.ToChar( symbol ) ) ||
				symbol == '_';
		}

	}
}
