using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Models
{
#if __ANDROID__
    
    public class MonthResult : Java.Lang.Object
    {
        public MonthResult(string month, double result)
        {
            this.Month = month;
            this.Result = result;
        }

        public string Month { get; set; }
        public double Result { get; set; }
    }
#endif

#if WINDOWS_UWP
    
    public class MonthResult
    {
        public MonthResult(string month, double result)
        {
            this.Month = month;
            this.Result = result;
        }

        public string Month { get; set; }
        public double Result { get; set; }
    }

#endif
}
