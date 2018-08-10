using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.Handlers
{
    static class Extensions
    {
        public static long ToUnixTime(this DateTime dateTime)
        {
            return (long)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
