using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Fudge.Modules.TaskManager {
    /// <summary>
    /// Abstract class for tasks
    /// </summary>
    public abstract class Task {
        private object _lockObject = new object();
        public static TimeSpan Day = TimeSpan.FromDays(1);
        public static TimeSpan Hour = TimeSpan.FromHours(1);
        public static TimeSpan HalfHour = TimeSpan.FromMinutes(30);
        public static TimeSpan FifteenMinutes = TimeSpan.FromMinutes(15);
        private TaskStatus _status;
        private DateTime _lastRun = DateTime.MaxValue;

        public Task() {
            _status = TaskStatus.Pending;
        }

        public abstract string Name { get; }
        public abstract TimeSpan Interval { get; }

        public virtual Func<DateTime, bool> Start {
            get {
                return null;
            }
        }

        public virtual TaskStatus Status {
            get {
                return _status;
            }
            set {
                _status = value;
            }
        }

        public DateTime LastRun {
            get {
                if (_lastRun == DateTime.MaxValue && File.Exists(LogFile)) {
                    string lastEntryTime = File.ReadAllLines(LogFile).Last().Split('|').Last();
                    _lastRun = DateTime.Parse(lastEntryTime);
                }
                return _lastRun;
            }
        }

        private string LogFile {
            get {
                return Path.Combine(TaskManager.TaskLog, Name + ".txt");
            }
        }

        private void WriteToLog(string entry) {
            using (StreamWriter sw = new StreamWriter(new FileStream(LogFile, FileMode.Append))) {                
                sw.WriteLine(entry);
            }
            Console.WriteLine("new log entry created for [" + Name + "] " + DateTime.Now);
        }

        public abstract void DoWork();

        public void Log(string message, params object[] args) {
            WriteToLog(String.Format("[{0}]|{1}|{2}", Name, String.Format(message, args), DateTime.Now));
            _lastRun = DateTime.Now;
        }
    }
}
