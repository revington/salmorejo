// 
//  StringExtensions.cs
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
using System.Text.RegularExpressions;
using System.Threading;

namespace Salmorejo.System
{
	public static class StringExtensions
	{
		/// <summary>
		/// Elimina patrones repetidos de subcadenas de texto en cadenas de texto. P.e ----a--b---c---- pasaría a ser -a-b-c-
		/// </summary>
		/// <param name="s">cadena</param>
		/// <param name="p">patrón</param>
		/// <returns></returns>
		private static string removeDuplicatedPattern (string s, string p)
		{
			return (String.IsNullOrEmpty (s) || (s.IndexOf (p + p) == -1))
                ? s
                : removeDuplicatedPattern (s.Replace (p + p, p), p);
		}
		/// <summary>
		/// Elimina patrones repetidos de subcadenas de texto en cadenas de texto. P.e ----a--b---c---- pasaría a ser -a-b-c-
		/// </summary>
		/// <param name="p">Patrón que no deseamos que se duplique</param>
		/// <returns>Cadena sin repeticiones</returns>
		public static string RemoveDuplicatedPattern (this string s, string p)
		{
			return removeDuplicatedPattern (s, p);
		}

		/// <summary>
		/// Reemplaza en una cadena la primera aparición de una subcadena por la subcadena indicada
		/// </summary>
		/// <param name="oldValue">Subcadena que queremos reemplazar</param>
		/// <param name="newValue">Reemplazo de oldValue</param>
		/// <returns>Producto del reemplazo</returns>
		public static string ReplaceFirstOccurrence (this string self, string oldValue, string newValue)
		{
			if (String.IsNullOrEmpty (self))
				return self;

			int i = self.IndexOf (oldValue);

			return (i == -1) ? self : self.Remove (i, oldValue.Length).Insert (i, newValue);
		}


		/// <summary>
		/// Este método supone un atajo a System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase
		/// El resultado dependerá de la cultura del hilo.
		/// </summary>
		/// <returns></returns>
		public static string ToTitleCase (this string self)
		{
			return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase (self.ToLower ());
		}

		/// <summary>
		/// Convierte una cadena de texto hexadecimal (que representa bytes ) en un array de bytes
		/// </summary>
		/// <returns></returns>
		public static byte[] HexStringToByteArray (this string self)
		{
			byte[] returnBytes = new byte[self.Length / 2];
			for (int i = 0; i < returnBytes.Length; i++) {
				returnBytes [i] = Convert.ToByte (self.Substring (i * 2, 2), 16);
			}
			return returnBytes;
		}

		/// <summary>
		/// Parsea esta cadena al valor de la enumeración indicada. Es invariant case. También es posible usar
		/// ParseToEnumExact que si tiene en cuenta la notación. 
		/// </summary>
		/// <typeparam name="T">Tipo de la enumeración</typeparam>
		/// <exception cref="System.ArgumentNullException">Si la cadena es nula o está vacía</exception>
		/// <exception cref="System.ArgumentException">Si T no es enum o no se encuentra el valor en la enumeración que corresponda</exception>
		/// <param name="self"></param>
		/// <returns></returns>
		public static T ParseToEnum<T> (this string self)
		{
			if (string.IsNullOrEmpty (self)) {
				throw new ArgumentNullException ("string");
			}
			return (T)Enum.Parse (typeof(T), self, true);
		}

		/// <summary>
		/// Parsea esta cadena al valor de la enumeración indicada. Es invariant case. También es posible usar
		/// ParseToEnumExact que si tiene en cuenta la notación. 
		/// </summary>
		/// <typeparam name="T">Tipo de la enumeración</typeparam>
		/// <exception cref="System.ArgumentNullException">Si la cadena es nula o está vacía</exception>
		/// <exception cref="System.ArgumentException">Si T no es enum o no se encuentra el valor en la enumeración que corresponda</exception>
		/// <param name="self"></param>
		/// <returns></returns>
		public static T ParseToEnumExact<T> (this string self)
		{
			if (string.IsNullOrEmpty (self)){
				throw new ArgumentNullException ("string");
			}
			return (T)Enum.Parse (typeof(T), self, false);
		}

        
	}
}

