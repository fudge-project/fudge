using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Fudge.Framework.ModuleInterface;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Fudge.Modules.TaskManager {
    public class TaskManager : IModule {
        private FileSystemWatcher watcher = new FileSystemWatcher(TasksDirectory);
        private List<Task> _tasks = new List<Task>();

        public static string TasksDirectory {
            get {
                return Path.Combine(Framework.Framework.GetDirectory(), @"Tasks\");
            }
        }

        public static string TaskLog {
            get {
                return Path.Combine(Framework.Framework.GetDirectory(), @"Logs\");
            }
        }

        public IModuleHost Host { get; set; }

        public string Name {
            get { return Assembly.GetExecutingAssembly().GetName().Name; }
        }

        public string Version {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        public ReadOnlyCollection<Task> Tasks {
            get {
                return _tasks.AsReadOnly();
            }
        }

        public static int TaskCount = 0;

        private void LoadTasksAndRun(string file) {
            if (Path.GetExtension(file) == ".dll") {
                Assembly taskAssembly = Assembly.LoadFrom(file);
                //get all public tasks in the tasks folder
                foreach (Type taskType in taskAssembly.GetTypes().Where(t => t.IsPublic)) {
                    if (typeof(Task).IsAssignableFrom(taskType)) {
                        try {
                            Task task = (Task)Activator.CreateInstance(taskType);
                            //create a thread for each task
                            Thread taskThread = new Thread(new ThreadStart(() => {
                                if (task.Start != null) {
                                    //tasks that specifically need to be started based on specific conditions
                                    try {
                                        while (task.Status != TaskStatus.Stopped) {
                                            while (!task.Start(DateTime.UtcNow)) {
                                                Thread.Sleep(500);
                                            }
                                            task.DoWork();
                                            Thread.Sleep(task.Interval);
                                        }
                                    }
                                    catch (Exception e) {
                                        task.Log("task failed :{0}", e.Message);
                                    }

                                }
                                else {
                                    //run the task every interval
                                    try {
                                        Console.WriteLine("checking last update for [" + task.Name + "]");
                                        if (task.LastRun != DateTime.MaxValue) {
                                            TimeSpan elapsedSinceLastRun = DateTime.Now - task.LastRun;
                                            if (elapsedSinceLastRun < task.Interval) {
                                                Thread.Sleep(task.Interval - elapsedSinceLastRun);
                                                Thread.Sleep(1000);
                                            }
                                        }
                                        while (task.Status != TaskStatus.Stopped) {
                                            task.Status = TaskStatus.Running;
                                            task.DoWork();
                                            task.Status = TaskStatus.Pending;
                                            Thread.Sleep(task.Interval);
                                        }
                                    }
                                    catch (Exception e) {
                                        task.Status = TaskStatus.Stopped;
                                        task.Log("task failed :{0}", e.Message);
                                    }

                                }
                            }));
                            _tasks.Add(task);                            
                            //start running the task
                            Console.WriteLine("[taskrunner] starting task {0}", task.Name);
                            taskThread.Start();
                            TaskCount++;
                        }
                        catch (Exception e) {
                            Console.WriteLine("failed to load task {0} :{1}", taskType.Name, e.Message);
                        }
                    }
                }
            }
        }

        public void Initialize() {
            //watch the task folder to load tasks dynamically
            watcher.Created += new FileSystemEventHandler(OnTasksAdded);
            watcher.EnableRaisingEvents = true;

            foreach (string file in Directory.GetFiles(TasksDirectory)) {
                LoadTasksAndRun(file);
            }

            while (TaskCount == 0) {
                //wait until at least one task is running
                Thread.Sleep(5000);
            }
        }

        private void OnTasksAdded(object sender, FileSystemEventArgs e) {
            //if a task was added to the directory
            LoadTasksAndRun(e.FullPath);
        }

        public void Dispose() {

        }
    }
}
