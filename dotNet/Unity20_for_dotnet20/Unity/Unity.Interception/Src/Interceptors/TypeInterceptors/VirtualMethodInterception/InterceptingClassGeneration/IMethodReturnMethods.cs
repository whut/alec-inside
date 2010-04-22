//===============================================================================
// Microsoft patterns & practices
// Unity Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System.Reflection;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.InterceptionExtension
{
    internal static class IMethodReturnMethods
    {
        internal static MethodInfo GetException
        {
            get { return /*StaticReflection.GetPropertyGetMethodInfo((IMethodReturn imr) => imr.Exception)*/
                typeof(IMethodReturn).GetProperty("Exception").GetGetMethod(); 
            }
        }

        internal static MethodInfo GetReturnValue
        {
            get { return /*StaticReflection.GetPropertyGetMethodInfo((IMethodReturn imr) => imr.ReturnValue)*/
                typeof(IMethodReturn).GetProperty("ReturnValue").GetGetMethod(); 
            }
        }

        internal static MethodInfo GetOutputs
        {
            get { return /*StaticReflection.GetPropertyGetMethodInfo((IMethodReturn imr) => imr.Outputs)*/
                typeof(IMethodReturn).GetProperty("Outputs").GetGetMethod(); 
            }
        }
    }
}
