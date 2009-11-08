using System;

namespace CXParser.Collectors {
	/// <summary>
	/// Represents a collector that can collect comment.
	/// </summary>
	public class CommentCollector : ICollector {

		public int ListenFor {
			get {
				return '/';
			}
		}

		public void Collect( Context context ) {
			int nextSymbol = context.Reader.Peek();
			switch ( nextSymbol ) {
				case '/':
					CollectSingleLine( context );
					break;
				case '*':
					CollectMultiLine( context );
					break;
			}
		}

		private void CollectSingleLine( Context context ) {
			while ( !IsSingleLineStopSymbol( context.Reader.Read() ) )
				;
		}

		private static bool IsSingleLineStopSymbol( int symbol ) {
			return symbol == -1 || IsLineTerminator( symbol );
		}

		private void CollectMultiLine( Context context ) {
			while ( !IsMultiLineStopSymbol( context.Reader.Read(), context ) )
				;

			context.Reader.Read();
		}

		private static bool IsMultiLineStopSymbol( int symbol, Context context ) {
			return symbol == -1 || 
				(symbol == '*' && context.Reader.Peek() == '/');
		}

		/// <summary>
		/// Indicates whether that the specified character is line terminator.
		/// </summary>
		/// <param name="token">The character to verify is line terminator.</param>
		private static bool IsLineTerminator( int token ) {
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
