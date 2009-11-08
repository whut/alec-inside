using System;

namespace CXParser {
	public interface ITokenizer {
		/// <summary>
		/// Read next token from context. 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		string ReadNext( Context context );
	}
}
