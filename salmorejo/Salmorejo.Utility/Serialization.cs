// 
//  DeepCopy.cs
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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Salmorejo.Utility
{
	public static class Serialization
	{
		public static T DeepCopy<T> (T obj)
		{
			T result;

			using (MemoryStream ms = new MemoryStream()) {
				BinaryFormatter bf = new BinaryFormatter ();
				bf.Serialize (ms, obj);
				ms.Position = 0;
				result = (T)bf.Deserialize (ms);
			}

			return result;
		}
		
		public static T DeserializeObjectFromBinaryFile<T> (string path)
		{
			T result;

			using (FileStream fs = new FileStream(path, FileMode.Open)) {

				IFormatter formatter = new BinaryFormatter ();
				fs.Position = 0;
				result = (T)formatter.Deserialize (fs);
			}

			return result;
		}
		
		public static void SerilizeObjectToBinaryFile (object self, string path)
		{
			using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write)) {
				IFormatter formatter = new BinaryFormatter ();
				formatter.Serialize (fs, self);
				fs.Flush ();
			}
		}
	}
}

