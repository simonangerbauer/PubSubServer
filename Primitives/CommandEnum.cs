using System;

namespace Primitives
{
    [Flags]
    enum CommandEnum
    {
        Subscribe,
        Unsubscribe,
        Publish
    }
}
