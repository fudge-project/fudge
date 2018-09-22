using System;
using Fudge.Framework.Database;

/// <summary>
/// Summary description for CommentPostedArgs
/// </summary>
public class CommentPostedArgs : EventArgs {
    public Post Post { get; private set; }
    public CommentPostedArgs(Post post) {
        Post = post;
    }
}
