using System;

namespace CXParser {
	/// <summary>
	/// Represents a token processor.
	/// </summary>
	public interface ITokenProcessor {
		/// <summary>
		/// Returns the token.
		/// </summary>
		string Token {
			get;
		}
		
		void Process( Context context );
	}
}
