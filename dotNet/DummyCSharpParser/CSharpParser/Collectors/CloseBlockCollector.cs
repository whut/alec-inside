using System;

namespace CXParser.Collectors {
	/// <summary>
	/// Represents a collector that can collect '}'.
	/// </summary>
	public sealed class CloseBlockCollector : ICollector {
		public int ListenFor {
			get {
				return '}';
			}
		}
		public void Collect( Context context ) {
			context.CloseBlock();
		}
	}
}
