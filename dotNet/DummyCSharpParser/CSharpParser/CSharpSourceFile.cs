using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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
			RegisterCollector( new Comment() );
			RegisterCollector( new RegularStringLiteral() );
			RegisterCollector( new VerbatimStringLiteral() );

			RegisterCollector(
				new ColonCollector( 
					new Dictionary<int, Collector>( collectors ) 
				) 
			);
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
			using ( StreamReader reader = new StreamReader( fileName ) ) {
				StringBuilder token = new StringBuilder();
				int currentSymbol;
				while ( ( currentSymbol = reader.Read() ) >= 0 ) {
					if ( IsIdentifierOrKeyword( (char)currentSymbol, reader.Peek() ) ) {
						token.Append( (char)currentSymbol );
						continue;
					}
					if ( token.Length > 0 ) {
						ProcessToken( token.ToString() );
						token.Length = 0;
					}

					if ( collectors.ContainsKey( currentSymbol ) ) {
						collectors[ currentSymbol ].Collect( reader );
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

		/// <summary>
		/// Indicates whether the specified Unicode character is categorized as a part of C# identifier
		/// or keyword. 
		/// </summary>
		/// <param name="symbol">A Unicode character.</param>
		/// <param name="nextSymbol">A next Unicode character.</param>
		private static bool IsIdentifierOrKeyword( char symbol, int nextSymbol ) {
			return Char.IsLetterOrDigit( symbol ) || symbol == '_' ||
				( symbol == '@' && nextSymbol != '"' );
		}

		private bool processed;
		private int classTokenCount;
		private Dictionary<int, Collector> collectors = new Dictionary<int, Collector>();
		private string fileName;
	}


}
