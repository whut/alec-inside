using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

[assembly: AssemblyInformationalVersion( "" )]

namespace ClassCounter {
	//class @class defenition
	public class @class {
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
		private struct @struct {
			private const string /*class*/ structName = "I'm a struct and my name is \"@struct\"";
			private const int _struct = 25; // struct !
		}

	}
	

	public abstract class Generic<T> where T : /*TT // */
		class, ICloneable, new() {

		public abstract void Method<C>() where C : //CC 
			class;

		private class A {
			public void Method() {
				string a = ( true ? "aha" : @"""nea""" );
				int c = 10;
				switch ( c ) {
					case 10: //ten
						a = "nonono";
						break;
				}
			}

			private class BinA {
			}
		}

	}
}
