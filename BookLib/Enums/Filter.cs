using System;

namespace BookLib.Enums
{
    [Flags]
    public enum Filter
    {
        Price = 0,
        Amount = 1,
        Available = 2
    }
}
