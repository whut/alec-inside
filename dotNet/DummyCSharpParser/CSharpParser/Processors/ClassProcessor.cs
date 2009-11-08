using System;
using System.Collections.Generic;

namespace CXParser {
	/// <summary>
	/// Processor for 'class' token
	/// </summary>
	public class ClassProcessor : ITokenProcessor {

		private HashSet<string> classesRegistry;
		public ClassProcessor( HashSet<string> classesRegistry ) {
			this.classesRegistry = classesRegistry;                                                                  
		}

		public virtual string Token {
			get {
				return "class";
			}
		}

		public void Process( Context context ) {
			string name = context.ReadNextToken();
			//Find generic args
			int genericArgsCount = FindGenericArgsCount( context );
			if ( genericArgsCount != 0 ) {
				name += "`" + genericArgsCount.ToString();
			}
					
			context.OpenBlock( name );
			classesRegistry.Add( context.GetCurrentPath() );
		}

		private int FindGenericArgsCount( Context context ) {
			int genericArgumentCount = 0;
			while ( context.Reader.Peek() != '{' &&
				context.Reader.Peek() != -1 ) {

				int currentSymbol = context.Reader.Read();
				switch ( currentSymbol ) {
					case '<':
						genericArgumentCount = 1;
						break;
					case '>':
						return genericArgumentCount;
					case ',':
						genericArgumentCount++;
						break;
					default:
						context.Collect( currentSymbol );
						break;
				}
			}
			return genericArgumentCount;
		}
	}
}
