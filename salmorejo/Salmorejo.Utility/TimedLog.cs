// 
//  TimedLog.cs
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

namespace Salmorejo.Utility
{
	/// <summary>
	/// Just to track how long it takes to perform an operation.
	/// </summary>
	/// <example>This sample shows usage. We are goingt
	/// <code>
	/// using (TimeLog log = new TimeLog("Call to 'DoSomething", (msg) => logger.Debug(msg))){
	/// 	DoSomething();
	/// }
	/// // Sends to logger.Debug("Call to DoSomething takes xxx miliseconds
	/// </code>
	/// </example>
	public class TimedLog : IDisposable
	{
		private string message;
		private long startTicks;
		Action<DateTime> _logAction;

		public TimedLog (string message, Action<string> logAction)
		{
			this.message = message;
            
			this.startTicks = DateTime.Now.Ticks;
            
			this._logAction = 
				(stop) =>	logAction (String.Format (" {0} takes {1} miliseconds", 
                		  	message, 
                		  	TimeSpan.FromTicks (stop.Ticks - this.startTicks).TotalMilliseconds
                )
            );
		}

       #region IDisposable Members

		public void Dispose ()
		{
			_logAction (DateTime.Now);
		}

        #endregion
	}
}

