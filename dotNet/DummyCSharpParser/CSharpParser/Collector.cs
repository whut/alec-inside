using System;
using System.IO;

namespace CSharpParser {
	/// <summary>
	/// Represents a collector that can collect some series of characters.
	/// </summary>
	public abstract class Collector {
		/// <summary>
		/// Returns the character code that begin collecting.
		/// </summary>
		public abstract int ListenFor {
			get;
		}
		/// <summary>
		/// Collect a series of characters.
		/// </summary>
		/// <param name="reader"></param>
		public abstract void Collect( TextReader reader );
	}
}
