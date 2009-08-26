using System;
using System.IO;

namespace CSharpParser {
	/// <summary>
	/// Represents a collector that can collect C# comment.
	/// </summary>
	public sealed class Comment : Collector {
		public override int ListenFor {
			get {
				return '/';
			}
		}

		public override void Collect( TextReader reader ) {
			int next = reader.Peek();
			if ( next == '/' ) {
				CollectSingleLine( reader );
			}
			else if ( next == '*' ) {
				CollectDelimeted(reader);
			}
		}

		/// <summary>
		/// Collect single line comment.
		/// </summary>
		private static void CollectSingleLine( TextReader reader ) {
			int currentSymbol;
			do {
				currentSymbol = reader.Read();
			} while ( currentSymbol >= 0 && !LineTerminator.IsLineTerminator( currentSymbol ) );
		}

		/// <summary>
		/// Collect delimeted comment.
		/// </summary>
		private static void CollectDelimeted( TextReader reader ) {
			int currentSymbol;
			do {
				currentSymbol = reader.Read();
			} while ( currentSymbol >= 0 && !( currentSymbol == '*' && reader.Peek() == '/' ) );
		}
	}
}
