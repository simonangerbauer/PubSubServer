using System;
namespace Primitives
{
    /// <summary>
    /// State enum, represents state of objects that enter the database.
    /// </summary>
    [Flags]
    public enum StateEnum
    {
        Unchanged,
        Modified, 
        Added,
        Deleted = 3
    }
}
