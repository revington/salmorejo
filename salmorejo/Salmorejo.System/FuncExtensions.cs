// 
//  FuncExtensions.cs
//  
//  Author:
//       Pedro Narciso García Revington <p.revington@gmail.com>
// 
//  Copyright (c) 2012 Pedro Narciso García Revington
// 
//  This program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 2 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//  
//  You should have received a copy of the GNU General Public License
//  along with this program; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// 
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;

namespace Salmorejo.System
{
	public static class FuncExtensions
	{
		delegate Func<T, R> Recursive<T,R> (Recursive<T, R> r);

		/// <summary>
		/// A simple "Fixed Point Combinator" implementation http://en.wikipedia.org/wiki/Y_combinator
		/// </summary>
		/// <typeparam name="T">Argument type</typeparam>
		/// <typeparam name="R">Result type</typeparam>
		/// <param name="f"></param>
		/// <returns></returns>
		/// <example>
		/// <![CDATA[
		/// // Anonymous and recursive reverse string function
		/// Func<string, string> reverse = Y<string, string>(
		/// f => 
		///     s => 
		///         (string.IsNullOrEmpty(s) || s.Length == 1)
		///         ? s 
		///         : f(s.Substring(1)) + s[0]);
		///         ]]>
		/// </example>
		public static Func<T, R> Y<T, R> (Func<Func<T, R>, Func<T, R>> f)
		{
			Recursive<T, R> rec =
                r =>
                    a =>
                        f (r (r)) (a);
			return rec (rec);
		}

		/// <summary>
		/// Currify 
		/// </summary>
		/// <typeparam name="A0"></typeparam>
		/// <typeparam name="A1"></typeparam>
		/// <typeparam name="A2"></typeparam>
		/// <typeparam name="A3"></typeparam>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="func">function to currify</param>
		/// <param name="arg"></param>
		/// <returns></returns>
		public static Func<A1, A2, A3, T> Curry<A0, A1, A2, A3, T> (this Func<A0, A1, A2, A3, T> func, A0 arg)
		{
			return (a1, a2, a3) => func (arg, a1, a2, a3);
		}
		/// <summary>
		/// Currify 
		/// </summary>
		/// <typeparam name="A0"></typeparam>
		/// <typeparam name="A1"></typeparam>
		/// <typeparam name="A2"></typeparam>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="func">function to currify</param>
		/// <param name="arg"></param>
		/// <returns></returns>
		public static Func<A1, A2, T> Curry<A0, A1, A2, T> (this Func<A0, A1, A2, T> func, A0 arg)
		{
			return (a1, a2) => func (arg, a1, a2);
		}
		/// <summary>
		/// Currify
		/// </summary>
		/// <typeparam name="A0"></typeparam>
		/// <typeparam name="A1"></typeparam>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="func">Function to currify</param>
		/// <param name="arg"></param> /// <returns></returns>
		public static Func<A1, T> Curry<A0, A1, T> (this Func<A0, A1, T> func, A0 arg)
		{
			return (a1) => func (arg, a1);
		}
		/// <summary>
		/// Currify
		/// </summary>
		/// <typeparam name="A0"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="func">function to currify</param>
		/// <param name="arg"></param>
		/// <returns></returns>
		public static Func<T> Curry<A0, T> (this Func<A0, T> func, A0 arg)
		{
			return () => func (arg);
		}
		
		/// <summary>
		/// Memoize the specified func.
		/// </summary>
		/// <param name='func'>
		/// Func.
		/// </param>
		/// <typeparam name='T'>
		/// The 1st type parameter.
		/// </typeparam>
		/// <typeparam name='TResult'>
		/// Function result type
		/// </typeparam>
		public static Func<T, TResult> Memoize<T, TResult> (this Func<T, TResult> func)
		{
			var t = new Dictionary<T, TResult> ();
			return n =>
			{
				if (t.ContainsKey (n)) {
					return t [n];
				} else {
					var result = func (n);
					t.Add (n, result);
					return result;
				}
			};
		}
		
		/// <summary>
		/// Basic memoize + memento implementation
		/// </summary>
		public static Func<T, TResult> MemoizeAndMemento<T, TResult>(this Func<T, TResult> func, string path)
        {
            Dictionary<T, TResult> t = new Dictionary<T, TResult>();
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    IFormatter formatter = new BinaryFormatter();
                    t = (Dictionary<T, TResult>)formatter.Deserialize(fs);
                }
            }
                
            return n =>
            {
                if (t.ContainsKey(n)){
					return t[n];
				}
                else
                {
                    var result = func(n);
                    Monitor.Enter(t);
                    try
                    {
                        t.Add(n, result);
                        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                        {
                            IFormatter formatter = new BinaryFormatter();
                            formatter.Serialize(fs, t);
                        }
                    }
                    finally
                    {
                        Monitor.Exit(t);
                    }
                    
                    return result;
                }
            };
        }


	}
}

