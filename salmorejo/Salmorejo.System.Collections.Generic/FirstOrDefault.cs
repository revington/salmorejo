// 
//  FirstOrDefault.cs
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
	public static partial class Enumerable2
	{
		public static TSource FirstOrDefault<TSource> (
            this IEnumerable<TSource> source, TSource defaultItem)
		{
			if (source == null) {
				throw new ArgumentNullException ("source");
			}
			using (IEnumerator<TSource> iterator = source.GetEnumerator()) {
				return iterator.MoveNext () ? iterator.Current : defaultItem;
			}
		}
	
	public static TSource FirstOrDefault<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate, TSource defaultItem)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    return item;
                }
            }
            return defaultItem;
        }
	}
}

