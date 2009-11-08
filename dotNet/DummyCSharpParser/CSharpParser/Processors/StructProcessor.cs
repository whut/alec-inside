using System;
using System.Collections.Generic;

namespace CXParser {
	public class StructProcessor : ClassProcessor {

		public StructProcessor( HashSet<string> structsRegistry )
			: base( structsRegistry ) {
		}

		public override string Token {
			get {
				return "struct";
			}
		}

	}
}
