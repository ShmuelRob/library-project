using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib.Enums
{
    [Flags]
    public enum JournalSpan
    {
        Daily = 0,
        Weekly = 1,
        TwiceInMonth = 2,
        Monthly = 4,
        yearly = 8
    }
}
