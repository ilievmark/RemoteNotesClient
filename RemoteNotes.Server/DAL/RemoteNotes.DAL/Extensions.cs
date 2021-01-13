using System;
using System.Collections.Generic;
using System.Data.Common;

namespace RemoteNotes.DAL
{
    public static class Extensions
    {
        public static IEnumerable<T> Select<T>(this DbDataReader reader, Func<DbDataReader, T> projection)
        {

            while (reader.Read())
            {
                yield return projection(reader);
            }
        }   
    }
}