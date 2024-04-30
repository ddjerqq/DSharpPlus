namespace DSharpPlus.Lavalink.EventArgs;
using System;

using DSharpPlus.AsyncEvents;

/// <summary>
/// Represents event arguments for Lavalink node disconnection.
/// </summary>
[Obsolete("DSharpPlus.Lavalink is deprecated for removal.", true)]
public sealed class NodeDisconnectedEventArgs : AsyncEventArgs
{
    /// <summary>
    /// Gets the node that was disconnected.
    /// </summary>
    public LavalinkNodeConnection LavalinkNode { get; }

    /// <summary>
    /// Gets whether disconnect was clean.
    /// </summary>
    public bool IsCleanClose { get; }

    internal NodeDisconnectedEventArgs(LavalinkNodeConnection node, bool isClean)
    {
        LavalinkNode = node;
        IsCleanClose = isClean;
    }
}
