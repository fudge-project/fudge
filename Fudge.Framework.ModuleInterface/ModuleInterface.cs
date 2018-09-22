using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.Database;
using System.Reflection;
using System.IO;

namespace Fudge.Framework.ModuleInterface {
    public class ProcessResult {
        
        public long ExecutionTime { get; set; }
        public long WorkingSet { get; set; }
        public string Output { get; set; }

        public ProcessResult() : this(0, 0, String.Empty) {
        }

        public ProcessResult(long executionTime, long maxWorkingSet, string output) {
            ExecutionTime = executionTime;
            WorkingSet = maxWorkingSet;
            Output = output;
        }
    }


    public interface IModule { //todo: idisposable
        IModuleHost Host { get; set; }

        string Name { get; }
        string Version { get; }

        void Initialize();
        void Dispose();
        //void Command<T>(T data);
    }

    public abstract class BaseModule : IModule {
        public IModuleHost Host { get; set; }

        public virtual string Name {
            get { return Assembly.GetExecutingAssembly().GetName().Name; }
        }

        public virtual string Version {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        public void WriteLogEntry(string entry) {
            //Host.WriteLogEntry(" [" + Name + "] " + entry);
        }

        public abstract void Initialize();
        public abstract void Dispose();
    }
    
    public interface IModuleCollection : ICollection<IModule> {
        
    }

    public interface IModuleHost {
        IModuleCollection Modules { get; }
    
        int CreateFile(string contents);
        void DeleteFile(int fileHandle);
        int Compile(Language language, int fileHandle);
        ProcessResult Execute(int exeHandle, int memoryLimit, int timeLimit, int outputLimit, string input);
        //void WriteLogEntry(string entry);
    }
}
