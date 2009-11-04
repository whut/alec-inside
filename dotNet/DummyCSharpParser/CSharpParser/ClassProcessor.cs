using System;
using System.Collections.Generic;

namespace CXParser {
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
			context.OpenBlock( name );
			classesRegistry.Add( context.GetCurrentPath() );
		}		
	}
}
