using System;

namespace BookLib.Enums
{
    [Flags]
    public enum BookCategory
    {
        Comedy = 0,
        Action = 1,
        Fantasy = 2,
        Horror = 4,
        Mystory = 8,
        Comic = 16,
        Information = 32,
        Other = 64
    }
}
