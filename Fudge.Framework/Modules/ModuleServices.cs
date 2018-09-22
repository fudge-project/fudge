using Fudge.Framework.ModuleInterface;
using System.Collections.Generic;
using Fudge.Framework.Database;
using System;
using Fudge.Framework;
using System.Threading;
using System.IO;

public class ModuleServices : IModuleHost {
    private class ExeTable : Dictionary<int, Fudge.Framework.Language> {
    }

    private class FileTable : Dictionary<int, string> {
    }

    public const int CompilationLimit = 4;
    public const int ExecutionLimit = 4;
    public const int PollTimeout = 50;

    public IModuleCollection Modules { get; private set; }

    private Queue<int> compilationQueue = new Queue<int>();
    private Queue<int> executionQueue = new Queue<int>();

    private ExeTable exeTable = new ExeTable();
    private FileTable fileTable = new FileTable();

    private int currentCompilations = 0;
    private int currentExecutions = 0;
    private int nextExeHandle = 0;
    private int nextFileHandle = 0;

    public ModuleServices() {
        Modules = new ModuleCollection();
    }

    private void Compile(int exeHandle) {

        lock (compilationQueue) {
            compilationQueue.Enqueue(exeHandle);
        }

        bool flag = false;

        while (!flag) {
            if ((compilationQueue.Peek() == exeHandle) && (currentCompilations < 4)) {
                Console.WriteLine("[fx] Compiling at executable handle {0}", exeHandle);
                Interlocked.Increment(ref currentCompilations);
                try {
                    exeTable[exeHandle].Compile();
                }
                finally {
                    Interlocked.Decrement(ref currentCompilations);
                    lock (compilationQueue) {
                        compilationQueue.Dequeue();
                        flag = true;
                    }
                }
            }

            Thread.Sleep(PollTimeout);
        }
    }

    public int Compile(Fudge.Framework.Database.Language language, int fileHandle) {
        Console.WriteLine("[fx] Queued compilation for file handle {0}", fileHandle);
        int key = 0;

        lock (exeTable) {
            key = nextExeHandle++;
            RenameFile(fileHandle, language.DefaultFile);
            exeTable.Add(key, new Fudge.Framework.Language(Path.Combine(Fudge.Framework.Framework.GetLanguageDirectory(), language.XmlFile), fileTable[fileHandle]));
        }

        Compile(key);
        return key;
    }

    public int CreateFile(string contents) {
        lock (fileTable) {
            string str = Path.Combine(Fudge.Framework.Framework.GetTempDirectory() + nextFileHandle, Path.GetRandomFileName());
            fileTable.Add(nextFileHandle, str);
            Directory.CreateDirectory(Path.GetDirectoryName(str));
            File.WriteAllText(str, contents);
            Console.WriteLine("[fx] Created file {0} with handle {1}", Path.GetFileName(str), nextFileHandle);

            return nextFileHandle++;
        }
    }

    public void DeleteFile(int fileHandle) {
        lock (fileTable) {
            Console.WriteLine("[fx] Deleted folder {0} at file handle {1}", Path.GetDirectoryName(fileTable[fileHandle]), fileHandle);
            Directory.Delete(Path.GetDirectoryName(fileTable[fileHandle]), true);
            fileTable.Remove(fileHandle);
        }
    }

    public ProcessResult Execute(int exeHandle, int memoryLimit, int timeLimit, int outputLimit, string input) {
        Console.WriteLine("[fx] Queued execution for executable handle {0}", exeHandle);
        ProcessResult result = null;

        lock (executionQueue) {
            executionQueue.Enqueue(exeHandle);
        }

        while (result == null) {
            
            if ((executionQueue.Peek() == exeHandle) && (currentExecutions < 4)) {
                Console.WriteLine("[fx] Executing at executable handle {0}", exeHandle);
                Interlocked.Increment(ref currentExecutions);

                try {
                    result = exeTable[exeHandle].Execute(memoryLimit, timeLimit, outputLimit, input);
                }
                finally {
                    Interlocked.Decrement(ref currentExecutions);
                    lock (executionQueue) {
                        executionQueue.Dequeue();
                    }
                }
            }

            Thread.Sleep(PollTimeout);
        }

        Console.WriteLine("[fx] Executed at executable handle {0}", exeHandle);
        return result;
    }

    private void RenameFile(int fileHandle, string fileName) {
        lock (fileTable) {
            FileInfo info = new FileInfo(fileTable[fileHandle]);
            fileTable[fileHandle] = Path.Combine(Path.GetDirectoryName(fileTable[fileHandle]), fileName);
            info.MoveTo(fileTable[fileHandle]);
        }
    }

    public void WriteLogEntry(string entry) {
        entry = DateTime.Now.ToShortTimeString() + " " + entry;
        
        Console.WriteLine(entry);
        new StreamWriter(File.Open(Path.Combine(Framework.GetDirectory(), "log.txt"), FileMode.Append)).WriteLine(entry);       
    }
}

