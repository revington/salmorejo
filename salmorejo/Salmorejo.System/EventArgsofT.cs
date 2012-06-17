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
	public class EventArgs<T1> : EventArgs
	{
		public EventArgs (T1 t1)
		{
			T1Value = t1;
		}

		public T1 T1Value { get; private set; }
	}

	public class EventArgs<T1, T2> : EventArgs
	{
		public EventArgs (T1 t1, T2 t2)
		{
			T1Value = t1;
			T2Value = t2;
		}

		public T1 T1Value { get; private set; }

		public T2 T2Value { get; private set; }
	}

	public class EventArgs<T1, T2, T3> : EventArgs
	{
		public EventArgs (T1 t1, T2 t2, T3 t3)
		{
			T1Value = t1;
			T2Value = t2;
			T3Value = t3;
		}

		public T1 T1Value { get; private set; }

		public T2 T2Value { get; private set; }

		public T3 T3Value { get; private set; }
	}
}

