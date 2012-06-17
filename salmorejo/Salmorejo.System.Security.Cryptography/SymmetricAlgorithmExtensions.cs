// 
//  SymmetricAlgorithmExtensions.cs
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
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;

namespace Salmorejo.System.Security.Cryptography
{
	public static class SymmetricAlgorithmExtensions
	{
		/// <summary>
		/// Cifra con algoritmo simétrico y serializa con base hexadecimal
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static string EncryptToHexString (this SymmetricAlgorithm self, object o)
		{
			return self.Encrypt (o).ToHexString ();
		}

		/// <summary>
		/// Descifra con algoritmo simétrico una cadena cifrada mediante SymmetricEncryptAndSerializeToHexString
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="s"></param>
		/// <returns></returns>
		public static T DecryptAndDeserializeFromHexString<T> (this SymmetricAlgorithm self, string s)
		{
			return self.Decrypt<T> (s.HexStringToByteArray ());
		}

		/// <summary>
		/// Descifra una matriz de bytes
		/// </summary>
		/// <typeparam name="T">Tipo original de objeto</typeparam>
		/// <param name="buffer">El objeto en sí</param>
		/// <returns>Si el objeto no se deserealizó correctamente devolverá default(T)</returns>
		public static T Decrypt<T> (this SymmetricAlgorithm self, byte[] buffer)
		{
			BinaryFormatter formatter = new BinaryFormatter ();

			ICryptoTransform decryptor = self.CreateDecryptor (self.Key, self.IV);

			T result = default(T);

			using (MemoryStream msDecrypt = new MemoryStream(buffer)) {
				using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) {
					buffer = new byte[msDecrypt.Length];

					csDecrypt.Read (buffer, 0, buffer.Length);

					using (MemoryStream ms = new MemoryStream(buffer)) {
						result = (T)formatter.Deserialize (ms);
					}
				}
			}
			return result;
		}

		/// <summary>
		/// Cifra un objeto 
		/// </summary>
		/// <param name="self"></param>
		/// <param name="o">Objeto que debe ser cifrado</param>
		/// <returns></returns>
		public static byte[] Encrypt (this SymmetricAlgorithm self, object o)
		{
			byte[] buffer = o.ToByteArray ();
			ICryptoTransform encryptor = self.CreateEncryptor (self.Key, self.IV);
			using (MemoryStream msEncrypt = new MemoryStream()) {
				using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) {
					csEncrypt.Write (buffer, 0, buffer.Length);
					csEncrypt.FlushFinalBlock ();
				}
				buffer = msEncrypt.ToArray ();
			}
			return buffer;
		}
	}
}

