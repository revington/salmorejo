// 
//  GenericComparer.cs
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
using System;
using System.Collections.Generic;
namespace Salmorejo.System.Collections.Generic
{
	/// <summary>
    /// A Generic comparer.
    /// </summary>
    public class GenericComparer<T> : IEqualityComparer<T>
    {
        readonly Func<T, T, bool> equals;
        
        public bool Equals(T a, T b)
        {
            return equals(a, b);
        }

        readonly Func<T, int> getHashCode;
        
        public int GetHashCode(T obj)
        {
            return getHashCode(obj);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="equals">Test two objects equality</param>
        /// <param name="getHashCode">Generates a hash code for object</param>
        public GenericComparer(Func<T, T, bool> equals, Func<T, int> getHashCode)
        {
            this.equals = equals;
            this.getHashCode = getHashCode;
        }

    }
}

