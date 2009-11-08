using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CXParser {
	public class Context {
		private ITokenizer tokenizer;
		private Dictionary<int, ICollector> collectors;
		private Stack<string> openedBlocks = new Stack<string>();
		private bool namedBlockOpened;


		public Context( TextReader reader,
			Dictionary<int, ICollector> collectors,
			ITokenizer tokenizer ) {

			Reader = reader;
			this.collectors = collectors;
			this.tokenizer = tokenizer;
			//Defines = new Defines();
		}

		public TextReader Reader {
			get;
			private set;
		}

		/*
		public Defines Defines {
			get;
			private set;
		}  */

				
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

		public void Collect( int currentSymbol ) {
			ICollector collector;
			if ( collectors.TryGetValue( currentSymbol, out collector ) ) {
				collector.Collect( this );
			}
		}

		public string ReadNextToken() {
			return tokenizer.ReadNext( this );
		}

	}
}
