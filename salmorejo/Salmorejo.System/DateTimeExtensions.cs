// 
//  DateTimeExtensions.cs
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

namespace Salmorejo.System
{
	public static class DateTimeExtensions
	{
		/// <summary>
		/// Finds the next date whose day of the week equals the specified day of the week.
		/// </summary>
		/// <param name="startDate">
		///		The date to begin the search.
		/// </param>
		/// <param name="desiredDay">
		///		The desired day of the week whose date will be returneed.
		/// </param>
		/// <returns>
		///	Next occurrence
		/// </returns>
		public static DateTime GetNextDateForDay (this DateTime startDate, DayOfWeek desiredDay)
		{
			return startDate.AddDays (DaysToAdd (startDate.DayOfWeek, desiredDay));
		}

		/// <summary>
		/// Calculates the number of days to add to the given day of
		/// the week in order to return the next occurrence of the
		/// desired day of the week.
		/// </summary>
		/// <param name="current">
		///		The starting day of the week.
		/// </param>
		/// <param name="desired">
		///		The desired day of the week.
		/// </param>
		/// <returns>
		///		The number of days to add to <var>current</var> day of week
		///		in order to achieve the next <var>desired</var> day of week.
		/// </returns>
		public static int DaysToAdd (this DayOfWeek current, DayOfWeek desired)
		{
			// f( c, d ) = g( c, d ) mod 7, g( c, d ) > 7
			//           = g( c, d ), g( c, d ) < = 7
			//   where 0 <= c < 7 and 0 <= d < 7

			int c = (int)current;
			int d = (int)desired;
			int n = (7 - c + d);

			return (n > 7) ? n % 7 : n;
		}
	}
}

