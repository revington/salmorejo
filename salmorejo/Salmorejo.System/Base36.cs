// 
//  Base36.cs
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
	public static class Base36
	{
		private static char[] base36Chars;

		static Base36 ()
		{
			base36Chars = new char[] { '0', '1', '2', '3', '4', 
				'5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 
				'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 
				'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 
				'W', 'X', 'Y', 'Z' };
		}
        
		public static string Encode (Int64 value)
		{
			return (value == 0) ? null : Encode (value / base36Chars.Length) 
				+ base36Chars [value % base36Chars.Length];
		}

		public static int Decode (string input)
		{
			input = input.Trim ();
			input = input.ToUpper ();
			int returnValue = 0;

			char[] characters = input.ToCharArray ();
			Array.Reverse (characters);

			for (int i = 0; i < characters.Length; i++) {
				int valueindex = Array.IndexOf (base36Chars, characters [i]);

				double temp = valueindex * Math.Pow (base36Chars.Length, i);

				returnValue += Convert.ToInt32 (temp);
			}

			return returnValue;
		}


	}
}

