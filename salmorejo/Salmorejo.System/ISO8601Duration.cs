// 
//  ISO8601Duration.cs
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
using System.Text.RegularExpressions;

namespace Salmorejo.System
{
	public class ISO8601Duration
	{
		static Regex expr = 
			new Regex (@"(-?)P((\d{1,4})Y)?((\d{1,4})M)?((\d{1,4})D)?(T((\d{1,4})H)?((\d{1,4})M)?((\d{1,4}(\.\d{1,3})?)S)?)?", RegexOptions.Compiled | RegexOptions.CultureInvariant);
		
		public  int Years{ private set; get; }

		public  int Months{ private set; get; }

		public  int Days{ private set; get; }

		public  int Hours{ private set; get; }

		public  int Minutes{ private set; get; }

		public  int Seconds{ private set; get; }
		
		public string Expression { private set; get; }

		private bool PositiveDuration;
				
		private ISO8601Duration (string input)
		{
			if (string.IsNullOrEmpty (input)) {
				throw new ArgumentNullException ("input", "input can not be empty");
			}
			
			this.Expression = input;
			this.PositiveDuration = !(input [0] == '-');
			
			if (input [PositiveDuration?0:1] != 'P') {
				throw new ArgumentException ("input", "Malformed xs:duration (no 'P')");
			}
			
			CopyValues();
		}
		
		void CopyValues(){
			MatchCollection matches = expr.Matches (this.Expression);
			var g = matches [0];
			Func<int,int> getNumber = x => {
				if (g.Groups.Count < x || string.IsNullOrEmpty (g.Groups [x].ToString ())) {
					return 0;
				}
				
				int a = int.Parse (g.Groups [x].ToString ());
				
				return PositiveDuration ? a : a * -1;
				
			};
			
			
			this.Years = getNumber (3);
			this.Months = getNumber (5);
			this.Days = getNumber (7);
			this.Hours = getNumber (10);
			this.Minutes = getNumber (12);
			this.Seconds = getNumber (14);
		}
		
		public static ISO8601Duration Parse (string input)
		{
			return new ISO8601Duration (input);
		}
		
		public DateTime AddTo (DateTime input)
		{
			DateTime ret = new DateTime (input.Ticks);
			ret = ret.AddYears (this.Years);
			ret = ret.AddMonths (this.Months);
			ret = ret.AddDays (this.Days);
			ret = ret.AddHours (this.Hours);
			ret = ret.AddSeconds (this.Seconds);
			return ret;
		}
	}
}

