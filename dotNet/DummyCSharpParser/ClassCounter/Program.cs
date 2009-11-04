using System;
using System.IO;

using CXParser;

namespace ClassCounter {
	class Program {
		static void Main( string[] args ) {
#if DEBUG
			FileInfo sourceFile = new FileInfo( "ClassTest.cs" );
#else
			if ( args.Length == 0 ) {
				Console.WriteLine( "Usage: ClassCounter <path to source.cs>" );
				return;
			}  
			
			FileInfo sourceFile = new FileInfo( args[0].Trim(new char[] {'<','>'} ) );
#endif
			
			if ( sourceFile.Exists ) {
				CSharpSourceFile cs = new CSharpSourceFile( sourceFile.FullName );				
				Console.WriteLine( string.Format( 
					"File contains {0} classes and structs.",
					cs.GetClassCount() ) 
				);
			}
			else {
				Console.WriteLine( "Sorry, file not found." );
			}

		}
	}
}
