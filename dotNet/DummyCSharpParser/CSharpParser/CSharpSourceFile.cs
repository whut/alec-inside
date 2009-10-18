using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CSharpParser.Base;

namespace CSharpParser {
	/// <summary>
	/// Represents a C# source code file.
	/// </summary>
	public sealed class CSharpSourceFile {
		/// <summary>
		/// Initializes a new instance of the <see cref="CSharpSourceFile"/> class for the specified file name.
		/// </summary>
		/// <param name="fileName">The complete file path.</param>
		public CSharpSourceFile( string fileName ) {
			this.fileName = fileName;
			RegisterCollector( new CommentCollector() );
			RegisterCollector( new RegularStringLiteral() );
			RegisterCollector( new VerbatimStringLiteral() );
			
		}
		/// <summary>
		/// Returns numbers of classes and structs definitions.
		/// </summary>
		/// <returns></returns>
		public int GetClassCount() {
			if ( !processed ) {
				Process();
			}
			return classTokenCount;
		}

		/// <summary>
		/// Register collector in collectors collection.
		/// </summary>
		/// <param name="collector"></param>
		private void RegisterCollector( Collector collector ) {
			if ( collectors.ContainsKey( collector.ListenFor ) ) {
				throw new ArgumentException(
					String.Format( "Duplicate collector. Collector with ListenFor character '{0}' already registered.",
						collector.ListenFor )
				);
			}
			collectors.Add( collector.ListenFor, collector );
		}

		/// <summary>
		/// Parse file with source code.
		/// </summary>
		private void Process() {
			using ( var reader = new StreamReader( fileName ) ) {
				var context = new Context( reader, new CSharpElement(), collectors );

				var token = new StringBuilder();
				int currentSymbol;
				while ( ( currentSymbol = reader.Read() ) >= 0 ) {
					if ( context.ElementTester.IsIdentifierOrKeyword( currentSymbol, reader.Peek() ) ) {
						token.Append( (char)currentSymbol );
						continue;
					}
					if ( token.Length > 0 ) {
						ProcessToken( token.ToString() );
						token.Length = 0;
					}

					if ( collectors.ContainsKey( currentSymbol ) ) {
						collectors[ currentSymbol ].Collect( context );
					}
				}
			}
			processed = true;
		}

		/// <summary>
		/// Analyze token.
		/// </summary>		
		private void ProcessToken( string token ) {
			if ( String.CompareOrdinal( token, "class" ) == 0 ||
				String.CompareOrdinal( token, "struct" ) == 0 ) {

				++classTokenCount;
			}
		}



		private bool processed;
		private int classTokenCount;
		private Dictionary<int, Collector> collectors = new Dictionary<int, Collector>();
		private string fileName;
	}


}
