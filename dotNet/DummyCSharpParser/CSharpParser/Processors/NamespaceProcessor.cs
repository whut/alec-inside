using System;

namespace CXParser {
	/// <summary>
	/// Processor for 'namespace' token
	/// </summary>
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
