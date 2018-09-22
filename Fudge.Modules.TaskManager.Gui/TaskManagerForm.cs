using System;
using System.Reflection;
using System.Windows.Forms;
using Fudge.Framework.ModuleInterface;
using System.Threading;
using System.Collections.Generic;

namespace Fudge.Modules.TaskManager.Gui {
    public partial class TaskManagerForm : Form, IModule {
        public TaskManagerForm() {
            InitializeComponent();
        }

        public IModuleHost Host { get; set; }
        public TaskManager Manager { get; private set; }

        [STAThread]
        public void Initialize() {
            Console.WriteLine("[task manager gui] Called initialize");
            if (Host != null) {
                foreach (var module in Host.Modules) {
                    Console.WriteLine("[task manager gui] Looking for Task Manager Module");
                    TaskManager manager = module as TaskManager;
                    if (manager != null) {
                        Console.WriteLine("[task manager gui] Found task manager {0}..", module.Name);
                        Manager = manager;
                        Thread.Sleep(5000);
                        foreach (var task in Manager.Tasks) {
                            taskListView.Items.Add(new ListViewItem(new[] { task.Name, task.Interval.ToString(), task.Status.ToString() }));
                        }
                        break;
                    }
                }
            }
            if (Manager == null) {
                Console.WriteLine("[task manager gui] no task manage found!");
                return;
            }
            Application.EnableVisualStyles();
            Application.Run(this);
        }

        string IModule.Name {
            get { return Assembly.GetExecutingAssembly().GetName().Name; }
        }

        string IModule.Version {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            if (Manager != null) {
                if (taskListView.Items.Count != 0) {
                    Task[] tasks = new Task[Manager.Tasks.Count];
                    Manager.Tasks.CopyTo(tasks, 0);
                    for (int i = 0; i < Manager.Tasks.Count; i++) {
                        taskListView.Items[i].SubItems[2].Text = tasks[i].Status.ToString();
                    }
                }
                else {
                    foreach (var task in Manager.Tasks) {
                        taskListView.Items.Add(new ListViewItem(new[] { task.Name, task.Interval.ToString(), task.Status.ToString() }));
                    }
                }
            }
        }
    }
}
