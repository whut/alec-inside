using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Practices.Unity
{
    /// <summary>
    /// Action
    /// </summary>
    /// <typeparam name="T1">param1 type</typeparam>
    /// <typeparam name="T2">param2 type</typeparam>
    /// <param name="t1">param1</param>
    /// <param name="t2">param2</param>
    public delegate void Action<T1,T2>( T1 t1, T2 t2); 
}
