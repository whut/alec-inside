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

using System;
using System.Reflection;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.InterceptionExtension
{
    internal static class InterceptionBehaviorPipelineMethods
    {
        internal static ConstructorInfo Constructor
        {
            get
            {
                return /*StaticReflection.GetConstructorInfo(() => new InterceptionBehaviorPipeline())*/
                    typeof(InterceptionBehaviorPipeline).GetConstructor(Type.EmptyTypes);
            }
        }

        internal static MethodInfo Add
        {
            get
            {
                return /*StaticReflection.GetMethodInfo((InterceptionBehaviorPipeline pip) => pip.Add(null))*/
                    typeof(InterceptionBehaviorPipeline).GetMethod("Add", new[] { typeof(IInterceptionBehavior) });
            }
        }

        internal static MethodInfo Invoke
        {
            get { return /*StaticReflection.GetMethodInfo((InterceptionBehaviorPipeline pip) => pip.Invoke(null, null))*/
                typeof(InterceptionBehaviorPipeline).GetMethod("Invoke", new[] { typeof(IMethodInvocation), typeof(InvokeInterceptionBehaviorDelegate) });
            }
        }
    }
}
