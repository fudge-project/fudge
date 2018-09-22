using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.ModuleInterface;
using System.Threading;
using System.Reflection;

namespace Fudge.Modules.Debug {
    public class DebugModule : IModule {

        public IModuleHost Host { get; set; }

        public string Name {
            get { return Assembly.GetExecutingAssembly().GetName().Name; }
        }

        public string Version {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        public DebugModule() {
            throw new Exception("NeedALifeException");
        }

        public void Initialize() {
            throw new Exception("NeverGotLaidException");
        }

        public void Dispose() {
            throw new NotImplementedException();
        }

    }
}
