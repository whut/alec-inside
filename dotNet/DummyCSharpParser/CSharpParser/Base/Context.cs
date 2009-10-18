using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CSharpParser.Base {
	public class Context {
		public Context( TextReader reader, ILangElement elementTester,
			Dictionary<int, Collector> collectors) {

			Reader = reader;
			ElementTester = elementTester;
			Collectors = collectors;
		}

		public TextReader Reader {
			get;
			private set;
		}

		public ILangElement ElementTester {
			get;
			private set;
		}

		public Dictionary<int, Collector> Collectors {
			get;
			private set;
		}
	}
}
