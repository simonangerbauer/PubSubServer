using System;
namespace Primitives
{
    [Flags]
    public enum StateEnum
    {
        Unchanged,
        Modified, 
        Added,
        Deleted = 3
    }
}
