// 
//  ISO8601DurationTests.cs
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
using NUnit.Framework;
using Salmorejo.System;

namespace Salmorejo.System
{
	[TestFixture]
	public class ISO8601DurationTests
	{
		ISO8601Duration one;

		[TestFixtureSetUp()]
		public void Init ()
		{
			one = ISO8601Duration.Parse ("P5Y4M2D");
		}

		[Test]
		public void Test_years ()
		{
			Assert.AreEqual (5, one.Years);
		}

		[Test]
		public void Test_months ()
		{
			Assert.AreEqual (4, one.Months);
		}

		[Test]
		public void Test_days ()
		{
			Assert.AreEqual (2, one.Days);
		}

		[Test()]
		public void Add_years ()
		{
			DateTime a = new DateTime (2000, 1, 1);
			DateTime expected = new DateTime (2005, 1, 1);
			DateTime c = ISO8601Duration.Parse ("P5Y").AddTo (a);
			Assert.AreEqual (expected, c);
		}
		
		[Test()]
		public void Remove_years ()
		{
			DateTime a = new DateTime (2000, 1, 1);
			DateTime expected = new DateTime (1995, 1, 1);
			DateTime c = ISO8601Duration.Parse ("-P5Y").AddTo (a);
			Assert.AreEqual (expected, c);
		}

		[Test()]
		public void Add_months ()
		{
			DateTime a = new DateTime (2000, 1, 1);
			DateTime expected = new DateTime (2001, 1, 1);
			DateTime c = ISO8601Duration.Parse ("P12M").AddTo (a);
			Assert.AreEqual (expected, c);
		}
	
		[Test()]
		public void Add_months_avoid_leap ()
		{
			DateTime a = new DateTime (2012, 2, 29);
			DateTime expected = new DateTime (2013, 2, 28);
			DateTime c = ISO8601Duration.Parse ("P12M").AddTo (a);
			Assert.AreEqual (expected, c);
		}

		[Test()]
		public void Add_one_month ()
		{
			DateTime a = new DateTime (2000, 1, 1);
			DateTime expected = new DateTime (2000, 2, 1);
			DateTime c = ISO8601Duration.Parse ("P1M").AddTo (a);
			Assert.AreEqual (expected, c);
		}

		[Test()]
		public void Add_days ()
		{
			DateTime a = new DateTime (2000, 1, 1);
			DateTime expected = new DateTime (2000, 1, 11);
			DateTime c = ISO8601Duration.Parse ("P10D").AddTo (a);
			Assert.AreEqual (expected, c);
		}

	}
}

