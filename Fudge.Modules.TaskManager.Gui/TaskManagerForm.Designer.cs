namespace Fudge.Modules.TaskManager.Gui {
    partial class TaskManagerForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.taskListView = new System.Windows.Forms.ListView();
            this.TaskNameCol = new System.Windows.Forms.ColumnHeader();
            this.Interval = new System.Windows.Forms.ColumnHeader();
            this.Status = new System.Windows.Forms.ColumnHeader();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // taskListView
            // 
            this.taskListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TaskNameCol,
            this.Interval,
            this.Status});
            this.taskListView.FullRowSelect = true;
            this.taskListView.GridLines = true;
            this.taskListView.Location = new System.Drawing.Point(12, 12);
            this.taskListView.Name = "taskListView";
            this.taskListView.Size = new System.Drawing.Size(386, 363);
            this.taskListView.TabIndex = 0;
            this.taskListView.UseCompatibleStateImageBehavior = false;
            this.taskListView.View = System.Windows.Forms.View.Details;
            // 
            // TaskNameCol
            // 
            this.TaskNameCol.Text = "Task";
            this.TaskNameCol.Width = 208;
            // 
            // Interval
            // 
            this.Interval.Text = "Interval";
            this.Interval.Width = 74;
            // 
            // Status
            // 
            this.Status.Text = "Status";
            this.Status.Width = 98;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(93, 381);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Pause";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 381);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(304, 381);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "End Process";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Interval = 1000;
            this.updateTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // TaskManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 416);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.taskListView);
            this.Name = "TaskManagerForm";
            this.Text = "TaskManager";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView taskListView;
        private System.Windows.Forms.ColumnHeader TaskNameCol;
        private System.Windows.Forms.ColumnHeader Interval;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Timer updateTimer;
    }
}

