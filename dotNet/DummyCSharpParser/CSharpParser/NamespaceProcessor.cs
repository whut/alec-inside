using System;

namespace CXParser {
	public sealed class NamespaceProcessor : ITokenProcessor {
		public string Token {
			get {
				return "namespace";
			}
		}

		public void Process( Context context ) {
			string name = context.ReadNextToken();
			context.OpenBlock( name );
		}		
	}
}
