// 
//  GenericEventArgs.cs
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
	public class GenericEventArgs<T> : EventArgs
	{
		public T Args { get; private set; }

		public static GenericEventArgs<T> Create (T args)
		{
			var ret = new GenericEventArgs<T> ();
			ret.Args = args;
			return ret;
		}
	}

	public class GenericEventArgs<T1, T2> : EventArgs
	{
		public T1 First { get; private set; }

		public T2 Second{ get; private set; }

		public static GenericEventArgs<T1, T2> Create (T1 first, T2 second)
		{
			var ret = new GenericEventArgs<T1, T2> ();
			ret.First = first;
			ret.Second = second;
			return ret;
		}
	}

	public class GenericEventArgs<T1, T2, T3> : EventArgs
	{
		public T1 First { get; private set; }

		public T2 Second { get; private set; }

		public T3 Third { get; private set; }

		public static GenericEventArgs<T1, T2, T3> Create (T1 first, T2 second, T3 third)
		{
			var ret = new GenericEventArgs<T1, T2, T3> ();
			ret.First = first;
			ret.Second = second;
			ret.Third = third;
			return ret;
		}
	}

	public class GenericEventArgs<T1, T2, T3, T4> : EventArgs
	{
		public T1 First { get; private set; }

		public T2 Second { get; private set; }

		public T3 Third { get; private set; }

		public T4 Fourth { get; private set; }

		public static GenericEventArgs<T1, T2, T3, T4> Create (T1 first, T2 second, T3 third, T4 fourth)
		{
			var ret = new GenericEventArgs<T1, T2, T3, T4> ();
			ret.First = first;
			ret.Second = second;
			ret.Third = third;
			ret.Fourth = fourth;
			return ret;
		}
	}
}

