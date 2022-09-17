using System;

namespace BookLib.Enums
{
    [Flags]
    public enum JournalCategory
    {
        Academic = 0,
        Fashion = 1,
        Design = 2,
        Beauty = 4,
        other = 8
    }
}
