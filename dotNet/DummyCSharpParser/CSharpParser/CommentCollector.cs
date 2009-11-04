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
			var isEndOfComment = GetEndOfCommentCondition( nextSymbol );

			if ( isEndOfComment == null ) {
				return;
			}
			 
			int currentSymbol;
			do {
				currentSymbol = context.Reader.Read();
			} while ( currentSymbol >= 0 && !isEndOfComment( context, currentSymbol ) );
		}

		protected virtual Func<Context, int, bool> GetEndOfCommentCondition( int secondSymbol ) {
			switch ( secondSymbol ) {
				case '/':
					return ( ( context, symbol ) =>
						context.SymbolTester.IsLineTerminator( symbol ) );
				case '*':
					return ( ( context, symbol ) =>
						symbol == '*' && context.Reader.Peek() == '/' );
			}
			return null;
		}

	}
}
