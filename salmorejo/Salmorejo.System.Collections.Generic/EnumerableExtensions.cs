// 
//  EnumerableExtensions.cs
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
using System.Linq;
using System.Collections.Generic;

namespace Salmorejo.System.Collections.Generic
{
	public static class Enumerable2
	{
		public static IEnumerable<DateTime> Range (DateTime start, DateTime end)
		{
			return Range (start, end, 1);
		}

		public static IEnumerable<DateTime> Range (DateTime start, DateTime end, int stepDays)
		{
			while (start <= end) {
				yield return start;
				start = start.AddDays (stepDays);
			}
		}
	}

	public static class EnumerableExtensions
	{

		/// <summary>
		/// Sobrecarga para distinct que permite indicar el método que debe utilizarse para realizar la comparación y el que debe utilizarse para obtener el hash de los objetos
		/// </summary>
		/// <exception cref="System.ArgumentNullException">Ni la secuencia ni el comparador puede ser nulo</exception>
		/// <param name="comparer">Método de comparación</param>
		/// <param name="hasher">Método para obtener el hash</param>
		/// <returns>Los distintos elementos de la secuencia</returns>
		public static IEnumerable<TSource> Distinct<TSource> (this IEnumerable<TSource> self, Func<TSource, TSource, bool> comparer, Func<TSource, int> hasher)
		{
			if (self == null)
				throw new ArgumentNullException ("sequence is null");

			if (comparer == null)
				throw new ArgumentNullException ("comparer is null");

			return self.Distinct (new GenericComparer<TSource> (comparer, hasher));
		}

		public static TSource FirstOrDefault<TSource> (this IEnumerable<TSource> self, TSource defaultValue)
		{
			return self.FirstOrDefault (x => true, defaultValue);
		}

		public static TSource FirstOrDefault<TSource> (this IEnumerable<TSource> self, Func<TSource, bool> test, TSource defaultValue)
		{
			foreach (var item in self) {
				if (test (item))
					return item;
			}
			return defaultValue;
		}
		
		/// <summary>
		/// Chunkify the specified this sequence into a 
		/// sequence of chuncks of the given size
		/// </summary>
		/// <param name='self'>
		/// Self.
		/// </param>
		/// <param name='size'>
		/// Size.
		/// </param>
		public static IEnumerable<IEnumerable<TSource>> Chunkify<TSource> (this IEnumerable<TSource> self, int size)
		{
			List<TSource> chunk = new List<TSource> (size);

			foreach (var element in self) {
				chunk.Add (element);
				if (chunk.Count == size) {
					yield return chunk;
					chunk = new List<TSource> (size);
				}
			}

			if (chunk.Any ()) {
				yield return chunk;
			}
		}
		
		public static IEnumerable<T> Filter<T> (this IEnumerable<T> self, Predicate<T> predicate)
		{
			if (self == null)
				throw new ArgumentNullException ("self");
			if (predicate == null)
				throw new ArgumentNullException ("predicate");

			foreach (var item in self) {
				if (predicate (item))
					yield return item;
			}
		}
		
		/// <summary>
		/// Get the specified page of the given collection.
		/// </summary>
		/// <param name='self'>
		/// Self.
		/// </param>
		/// <param name='page'>
		/// Page.
		/// </param>
		/// <param name='itemsPerPage'>
		/// Specify how much items per page.
		/// </param>
		public static IEnumerable<T> Page<T> (this IEnumerable<T> self, int page, int itemsPerPage)
		{
			return self.Skip (page * itemsPerPage).Take (itemsPerPage);
		}
	}
}

