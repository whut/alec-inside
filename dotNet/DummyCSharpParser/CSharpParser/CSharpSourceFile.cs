using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CXParser.Collectors;


namespace CXParser {
	/// <summary>
	/// Represents a C# source code file.
	/// </summary>
	public sealed class CSharpSourceFile {
		private bool processed;
		private HashSet<string> classesRegistry = new HashSet<string>();
		private Dictionary<int, ICollector> collectors =
			new Dictionary<int, ICollector>();
		private Dictionary<string, ITokenProcessor> tokenProcessors =
			new Dictionary<string, ITokenProcessor>();
		private string fileName;

		/// <summary>
		/// Initializes a new instance of the <see cref="CSharpSourceFile"/> 
		/// class for the specified file name.
		/// </summary>
		/// <param name="fileName">The complete file path.</param>
		public CSharpSourceFile( string fileName ) {
			this.fileName = fileName;
			Register( new CommentCollector() );
			Register( new RegularStringLiteralCollector() );
			Register( new VerbatimStringLiteralCollector() );
			Register( new ColonCollector() );
			Register( new OpenBlockCollector() );
			Register( new CloseBlockCollector() );

			Register( new NamespaceProcessor() );
			Register( new ClassProcessor( classesRegistry ) );
			Register( new StructProcessor( classesRegistry ) );
		}
		

		public void Register( ICollector collector ) {
			if ( collectors.ContainsKey( collector.ListenFor ) ) {
				throw new ArgumentException(
					String.Format( @"Duplicate collector. Collector with 
						ListenFor character '{0}' already registered.",
						collector.ListenFor )
				);
			}
			collectors.Add( collector.ListenFor, collector );
		}

		public void Register( ITokenProcessor processor ) {
			if ( tokenProcessors.ContainsKey( processor.Token ) ) {
				throw new ArgumentException(
					String.Format( @"Duplicate processor. Processor for 
						token '{0}' already registered.",
						processor.Token )
				);
			}
			tokenProcessors.Add( processor.Token, processor );
		}


		/// <summary>
		/// Parse file with source code.
		/// </summary>
		private void Parse() {
			using ( var reader = new StreamReader( fileName ) ) {
				var context = new Context( reader, collectors, new CSharpTokenizer() );

				for ( string token = context.ReadNextToken(); 
					token != null; 
					token = context.ReadNextToken() ) {

					ITokenProcessor processor;
					if ( tokenProcessors.TryGetValue( token, out processor ) ) {
						processor.Process( context );
					}
				}
			}
			processed = true;
		}

		

		/// <summary>
		/// Returns names of classes and structs definitions.
		/// </summary>
		/// <returns></returns>
		public string[] GetClasses() {
			if ( !processed ) {
				Parse();
			}
			return classesRegistry.ToArray();
		}

	
	}


}
