using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CXParser {
	public class Context {
		public Context( TextReader reader, ILangSymbol elementTester,
			Dictionary<int, ICollector> collectors ) {

			Reader = reader;
			SymbolTester = elementTester;
			Collectors = collectors;
			Defines = new Defines();
		}

		public TextReader Reader {
			get;
			private set;
		}

		public ILangSymbol SymbolTester {
			get;
			private set;
		}

		public Dictionary<int, ICollector> Collectors {
			get;
			private set;
		}

		public Defines Defines {
			get;
			private set;
		}

		private Stack<string> openedBlocks = new Stack<string>();
		private bool namedBlockOpened;
		public void OpenBlock() {
			if ( namedBlockOpened ) {
				namedBlockOpened = false;
				return;
			}
			openedBlocks.Push( null );
		}
		public void OpenBlock( string name ) {
			namedBlockOpened = true;
			openedBlocks.Push( name );
		}

		public void CloseBlock() {
			openedBlocks.Pop();
		}

		public string GetCurrentPath() {
			if ( openedBlocks.Count == 0 ) {
				return string.Empty;
			}
			var names = openedBlocks.Where( blockName => blockName != null )
				.Reverse()
				.ToArray();
			return string.Join( ".", names );
		}

		public string ReadNextToken() {
			var token = new StringBuilder();
			int currentSymbol;
			while ( ( currentSymbol = Reader.Read() ) >= 0 ) {
				if ( SymbolTester.IsIdentifierOrKeyword(
					currentSymbol, Reader.Peek() ) ) {

					token.Append( Convert.ToChar( currentSymbol ) );
					continue;
				}

				ICollector collector;
				if ( Collectors.TryGetValue( currentSymbol, out collector ) ) {
					collector.Collect( this );
				}

				if ( token.Length > 0 ) {
					return token.ToString();
				}
			}
			return null;
		}

	}
}
