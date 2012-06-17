// 
//  Levenshtein.cs
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

namespace Salmorejo.Utility
{
	public static class Levenshtein
	{
		/// <summary>
		/// Levenshtein distance implementation 
		/// </summary>
		/// <param name='a'>
		/// String 'a 
		/// </param>
		/// <param name='b'>
		/// String 'b
		/// </param>
		public static int Distance (string a, string b)
		{
			if (string.IsNullOrEmpty (a)) {
				if (!string.IsNullOrEmpty (b)) {
					return b.Length;
				}
				return 0;
			}

			if (string.IsNullOrEmpty (b)) {
				if (!string.IsNullOrEmpty (a)) {
					return a.Length;
				}
				return 0;
			}

			Int32 cost;
			Int32[,] d = new int[a.Length + 1, b.Length + 1];
			Int32 min1;
			Int32 min2;
			Int32 min3;

			for (Int32 i = 0; i <= d.GetUpperBound(0); i += 1) {
				d [i, 0] = i;
			}

			for (Int32 i = 0; i <= d.GetUpperBound(1); i += 1) {
				d [0, i] = i;
			}

			for (Int32 i = 1; i <= d.GetUpperBound(0); i += 1) {
				for (Int32 j = 1; j <= d.GetUpperBound(1); j += 1) {
					cost = Convert.ToInt32 (!(a [i - 1] == b [j - 1]));

					min1 = d [i - 1, j] + 1;
					min2 = d [i, j - 1] + 1;
					min3 = d [i - 1, j - 1] + cost;
					d [i, j] = Math.Min (Math.Min (min1, min2), min3);
				}
			}

			return d [d.GetUpperBound (0), d.GetUpperBound (1)];
		}
	}
}

