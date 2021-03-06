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
	public static partial class Enumerable2
	{
		
	}

	public static class EnumerableExtensions
	{

		
		
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

