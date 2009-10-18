using System;
using System.IO;
using CSharpParser.Base;

namespace CSharpParser {
	/// <summary>
	/// Represents a collector that can collect comment.
	/// </summary>
	public class CommentCollector : Collector {

		public override int ListenFor {
			get {
				return '/';
			}
		}

		public override void Collect( Context context ) {
			int next = context.Reader.Peek();
			var isEndOfComment = GetEndOfCommentCondition( next );

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
						context.ElementTester.IsLineTerminator( symbol ) );
				case '*':
					return ( ( context, symbol ) =>
						symbol == '*' && context.Reader.Peek() == '/' );
			}
			return null;
		}

	}
}
