using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.ModuleInterface;
using Fudge.Framework.Database;

namespace Fudge.Modules.Helloworld {
    public class HelloWorldModule : IModule{

        #region IModule Members

        public IModuleHost Host {
            get;
            set;
        }

        public string Name {
            get { return "Hello world! Module"; }
        }

        public string Version {
            get { return "1.0"; }
        }

        public void Initialize() {
            FudgeDataContext db = new FudgeDataContext();
            Language cpluscplus = db.Languages.SingleOrDefault(l => l.Name.Contains("C++"));
            
            int handle = Host.CreateFile("#include<iostream>\nint main() { std::cout << \"Hello, World!\";}");

            try {
                handle = Host.Compile(cpluscplus, handle);
            }
            catch (CompilationErrorException ex) {
                Console.WriteLine(ex.Message);
            }

            ProcessResult result = Host.Execute(handle, 16000000, 5000, 128, String.Empty);
            Console.WriteLine(result.Output);
        }

        public void Dispose() {
            
        }

        #endregion
    }
}
