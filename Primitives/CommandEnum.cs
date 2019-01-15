using System;

namespace Primitives
{
    [Flags]
    public enum CommandEnum
    {
        Subscribe,
        Unsubscribe,
        Publish
    }
}
