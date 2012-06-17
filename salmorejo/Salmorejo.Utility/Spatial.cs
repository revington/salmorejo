// 
//  Spatial.cs
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
	public static class Spatial
	{
		const Double EARTH_RADIUS_IN_KM = 6376.5;
		
		/// <summary>
		/// Gets kilometers between two points
		/// </summary>
		/// <returns>Distance in kilometers</returns>
		public static double Kilometers (double lat1, double lng1, double lat2, double lng2)
		{
			return EARTH_RADIUS_IN_KM * 2 * (
	            Math.Asin (
	                Math.Min (1,
	                    Math.Sqrt (
	                        (
	                            Math.Pow (Math.Sin ((DiffRadian (lat1, lat2)) / 2.0), 2.0) +
	                            Math.Cos (ToRadian (lat1)) * Math.Cos (ToRadian (lat2)) *
	                            Math.Pow (Math.Sin ((DiffRadian (lng1, lng2)) / 2.0), 2.0)
	                        )
	                   )
	               )
	           )
	        );
		}

		static double ToRadian (double val)
		{
			return val * (Math.PI / 180);
		}

		static double DiffRadian (double val1, double val2)
		{
			return ToRadian (val2) - ToRadian (val1);
		}
	}
}

