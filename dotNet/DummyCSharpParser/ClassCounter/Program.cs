using System;
using System.IO;
using CXParser;

namespace ClassCounter {
	class Program {
		static void Main( string[] args ) {
#if DEBUG
			var sourceFile = new FileInfo( "ClassTest.cs" );
#else
			if ( args.Length == 0 ) {
				Console.WriteLine( "Usage: ClassCounter <path to source.cs>" );
				return;
			}  
			
			var sourceFile = new FileInfo( args[0].Trim(new char[] {'<','>'} ) );
#endif
			
			if ( sourceFile.Exists ) {
				var cs = new CSharpSourceFile( sourceFile.FullName );
				var classes = cs.GetClasses();
				Console.WriteLine( string.Format( 
					"File contains {0} classes and structs:",
					 classes.Length ) 
				);
				Array.ForEach( classes, Console.WriteLine );
			}
			else {
				Console.WriteLine( "Sorry, file not found." );
			}
			Console.ReadKey();
		}
	}
}
