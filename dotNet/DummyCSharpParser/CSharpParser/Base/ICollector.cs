using System;

namespace CXParser {
	/// <summary>
	/// Represents a collector that can collect some series of characters.
	/// </summary>
	public interface ICollector {
		/// <summary>
		/// Returns the character code that begin collecting.
		/// </summary>
		int ListenFor {
			get;
		}
		/// <summary>
		/// Collect a series of characters.
		/// </summary>
		/// <param name="reader"></param>
		void Collect( Context context );
	}
}
