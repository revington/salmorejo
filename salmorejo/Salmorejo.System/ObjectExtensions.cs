// 
//  ObjectExtensions.cs
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

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using Salmorejo.System.Security.Cryptography;

namespace Salmorejo.System
{
	public static class ObjectExtensions
	{
		/// <summary>
        /// Serialize object to byte array
        /// </summary>
        /// <exception cref="System.Runtime.Serialization.SerializationException">The object is not marked as serializabel</exception>
        /// <exception cref="System.Security.SecurityException">Not enough permissions</exception>
        /// <returns></returns>
        public static byte[] ToByteArray(this object self)
        {
            byte[] result = null;

            if (self != null)
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (MemoryStream ms = new MemoryStream())
                {
                    formatter.Serialize(ms, self);
                    result = ms.ToArray();
                }
            }
            
            return result;
        }

        /// <summary>
        /// Digest SHA1 de este objeto con formato de cadena de texto (hexadecimal). Si el objeto es null devolverá null
        /// </summary>
        /// <returns>Digest SHA1 de este objeto con formato de cadena de texto (hexadecimal)</returns>
        public static string GetSHA1Digest(this object self)
        {
            if (self == null) return null;
            return (SHA1.Create()).GetHexHashString(self.ToByteArray()).ToUpperInvariant();
        }
	}
}

