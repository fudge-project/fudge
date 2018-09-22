using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fudge.Framework.Database;

namespace Fudge.Web.AdminConsole.Controls {
    public partial class ForumManager : ManagerControl {

        public Post SelectedPost { get; private set; }

        public ForumManager() {
            InitializeComponent();
        }

        public override void Initialize() {
            foreach (ForumCategory category in DataContext.ForumCategories) {
                forumTreeView.Nodes.Add(category.CategoryId.ToString(), category.Title).Nodes.Add("Loading...");
            }
        }

        private void forumTreeView_AfterExpand(object sender, TreeViewEventArgs e) {

            if (e.Node.Nodes.Count == 1) {
                e.Node.Nodes.Clear();
            }

            switch (e.Node.Level) {
                case 0: {
                        ForumCategory category = DataContext.ForumCategories.SingleOrDefault(c => c.CategoryId == Int32.Parse(e.Node.Name));

                        foreach (Forum forum in category.Forums) {
                            e.Node.Nodes.Add(forum.ForumId.ToString(), forum.Title).Nodes.Add("Loading...");
                        }
                    } break;

                case 1: {
                        Forum forum = DataContext.Forums.SingleOrDefault(f => f.ForumId == Int32.Parse(e.Node.Name));

                        foreach (Topic topic in forum.Topics) {
                            e.Node.Nodes.Add(topic.TopicId.ToString(), topic.Title).Nodes.Add("Loading...");
                        }
                    } break;

                case 2: {
                        Topic topic = DataContext.Topics.SingleOrDefault(t => t.TopicId == Int32.Parse(e.Node.Name));

                        foreach (Post post in topic.Posts) {
                            e.Node.Nodes.Add(post.PostId.ToString(), post.Title + " [" + post.User.FullName + "]");
                        }
                    } break;
            }
        }

        private void forumTreeView_AfterSelect(object sender, TreeViewEventArgs e) {
            if (e.Node.Level == 3) {
                SelectedPost = DataContext.Posts.SingleOrDefault(p => p.PostId == Int32.Parse(e.Node.Name));

                contentRichTextBox.Enabled = true;
                contentRichTextBox.Text = SelectedPost.Message;

                updateButton.Enabled = true;
                deleteButton.Enabled = true;
            }
            else {
                SelectedPost = null;

                contentRichTextBox.Enabled = false;
                contentRichTextBox.Text = "\n\n\n\n\n\n\n\nSelect a post in the tree control to view and edit its contents";
                contentRichTextBox.SelectAll();
                contentRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
                contentRichTextBox.DeselectAll();

                updateButton.Enabled = false;
                deleteButton.Enabled = false;
            }
        }

        private void updateButton_Click(object sender, EventArgs e) {
            if (SelectedPost != null) {
                SelectedPost.Message = contentRichTextBox.Text;
                DataContext.SubmitChanges();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Are you sure you want to delete \"" + SelectedPost.Title + "\"?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                DataContext.Posts.DeleteOnSubmit(SelectedPost);
                DataContext.SubmitChanges();

                forumTreeView.SelectedNode.Remove();
            }
        }
    }
}
