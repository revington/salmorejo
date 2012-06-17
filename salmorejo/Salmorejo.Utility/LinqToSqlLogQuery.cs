// 
//  LinqToSqlLogQuery.cs
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
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data.Linq;

namespace Salmorejo.Utility
{
	public static class LinqToSqlLogQuery
	{
		public static string SerializeForLog(this ChangeConflictException e, DataContext context)
        {
            StringBuilder builder = new StringBuilder();

            using (StringWriter sw = new StringWriter(builder))
            {
                sw.WriteLine("Optimistic concurrency error:");
                sw.WriteLine(e.Message);

                foreach (ObjectChangeConflict occ in context.ChangeConflicts)
                {
                    Type objType = occ.Object.GetType();
                    MetaTable metatable = context.Mapping.GetTable(objType);
                    object entityInConflict = occ.Object;

                    sw.WriteLine("Table name: {0}", metatable.TableName);

                    var noConflicts =
                        from property in objType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        where property.CanRead &&
                              property.CanWrite &&
                              property.GetIndexParameters().Length == 0 &&
                              !occ.MemberConflicts.Any(c => c.Member.Name != property.Name)
                        orderby property.Name
                        select property;

                    foreach (var property in noConflicts)
                    {
                        sw.WriteLine("\tMember: {0}", property.Name);
                        sw.WriteLine("\t\tCurrent value: {0}",
                            property.GetGetMethod().Invoke(occ.Object, new object[0]));
                    }

                    sw.WriteLine("\t-- Conflicts Start Here --");

                    foreach (MemberChangeConflict mcc in occ.MemberConflicts)
                    {
                        sw.WriteLine("\tMember: {0}", mcc.Member.Name);
                        sw.WriteLine("\t\tCurrent value: {0}", mcc.CurrentValue);
                        sw.WriteLine("\t\tOriginal value: {0}", mcc.OriginalValue);
                        sw.WriteLine("\t\tDatabase value: {0}", mcc.DatabaseValue);
                    }

                    sw.WriteLine("\t-- Conflicts End --");
                }

                sw.WriteLine();
                sw.WriteLine("Attempted SQL: ");

                TextWriter tw = context.Log;

                try
                {
                    context.Log = sw;
                    context.SubmitChanges();
                }
                catch (ChangeConflictException)
                {

                }
                catch
                {
                    sw.WriteLine("Unable to recreate SQL!");
                }
                finally
                {
                    context.Log = tw;
                }

                sw.WriteLine();
            }

            return builder.ToString();
        }
    }
}


