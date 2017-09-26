using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Models
{
#if __ANDROID__

    public class MonthResultDataBinding : Com.Telerik.Widget.Chart.Engine.Databinding.DataPointBinding
    {

        private string propertyName;

        public MonthResultDataBinding(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public override Java.Lang.Object GetValue(Java.Lang.Object p0)
        {
            if (propertyName == "Month")
            {
                return ((MonthResult)(p0)).Month;
            }
            return ((MonthResult)(p0)).Result;
        }
    }
#endif
}
