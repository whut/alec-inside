using System;
using System.Collections.Generic;

namespace CXParser {
	public class Defines {
		private HashSet<string> defines = new HashSet<string>();

		public void Define( string name ) {
			defines.Add( name.Trim() );
		}

		public void Define( string names, char separator ) {
			var nameArray = names.Split( separator );
			foreach ( var name in nameArray ) {
				Define( name );				
			}
		}

		public void Undefine( string name ) {
			defines.Remove( name.Trim() );
		}

		public bool IsDefined( string name ) {
			return defines.Contains( name.Trim() );
		}

	}
}
