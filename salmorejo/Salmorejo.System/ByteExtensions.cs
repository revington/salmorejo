// 
//  Byte.cs
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
using System.Text;


namespace Salmorejo.System
{
	public static class ByteExtensions
    {
        /// <summary>
        /// Byte arry to hex string utility
        /// </summary>
        /// <exception cref="System.ArgumentNullException">On null arrays</exception>
        /// <returns></returns>
        public static string ToHexString(this byte[] self)
        {
            if (self == null) throw new ArgumentNullException("this (byte array)");
            
            StringBuilder result = new StringBuilder();
            foreach (byte n in self)
                result.Append(n.ToString("x2"));
            return result.ToString();
        }
    }
}

