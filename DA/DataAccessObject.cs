using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    class DataAccessObject
    {
        static IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
        public DateTime Sql_datetimemin = DateTime.Parse("01/01/1753", provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        public DateTime Sql_datetimemax = DateTime.Parse("01/01/9999", provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
    }
}
