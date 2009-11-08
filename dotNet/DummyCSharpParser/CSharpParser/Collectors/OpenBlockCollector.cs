using System;

namespace CXParser.Collectors {
	/// <summary>
	/// Represents a collector that can collect '}'.
	/// </summary>
	public sealed class OpenBlockCollector : ICollector {
		public int ListenFor {
			get {
				return '{';
			}
		}
		public void Collect( Context context ) {
			context.OpenBlock();
		}
	}
}
