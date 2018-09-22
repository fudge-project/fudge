using System;

/// <summary>
/// Summary description for FriendStatusEventArgs
/// </summary>
public class FriendStatusEventArgs : EventArgs {
    public int FriendId { get; private set; }
    public FriendStatusEventArgs(int friendId) {
        FriendId = friendId;
    }
}
