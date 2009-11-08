using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

[assembly: AssemblyInformationalVersion( "1.1.1.0" )]

namespace ClassCounter {
	//1
	public partial class ABC {
		partial void DoWork();
	}
	namespace Inner//jjjjj
	{
		//2
		public partial class//zzz
			ABC/*nnn*/{
			partial void DoWork();
			//9
			public partial class InnerAbc {
				void Some() {
				}
			}
		}
		//3
		//class @class definition
		public class/*777*/ @class {
			/// <summary>
			/// class Name field
			/// </summary>		
			private const string class_Name = @"""my ""class"" name is ""@class"" in C.Sharp""";
			private const string quote = @"""";
			private const string classExternalName = "\"my name is \" class \" for other languages \"";
			/**/
			/*
			 * Nested struct definition
			 */
			//4
			private struct/*zzz*/@struct {
				private const string/* class */structName = "I'm a struct and my name is \"@struct\"";
				private const int _struct = 25; // struct !
			}

		}
	}

	namespace Inner {
		//2
		public partial class ABC {
			partial void DoWork() {
				//FrrrrrrrTrrrrrrr!
			}
			//9
			public partial class InnerAbc {
				//nothing
			}
			//10
			public struct MotherNight<Brasil> {
				/*Read It*/
				private int readers;//
			}
		}
	}
	//5
	namespace Inner2/*777*/{public partial class ABC { void DoWork() {
				//FrrrrrrrTrrrrrrr!
			}
		}
	}
	//1
	public partial class ABC {
		partial void DoWork() {
			//no code
		}
	}
	//6
	public abstract class Generic<T> where T : /*TT // */
		class, ICloneable, new() {

		public abstract void Method<C>()where/*777*/C/*777*/ ://CC 
			class;
		//7
		private class A {
			public void Method() {
				string a = ( true ? "a" : @"""b""" );
				int c = 10;
				switch ( c ) {
					case 10: //ten
						a = "nonono";
						break;
				}
			}
			//8
			private class BinA {
			}
		}

	}
	//11
	public abstract partial class Generic/*555*/<G,/*44*/t> {
		G x;
		public abstract int X();
	}
	//12
	public abstract class Generic //12121
		<G, t, Z> {

		public abstract int X();
	}
}
namespace ClassCounter {
	//11
	public abstract partial class Generic/*555*/<G,/*,,*/t> {
		
		
	}
}