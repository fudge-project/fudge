using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.ModuleInterface;
using System.Reflection;
using Fudge.Framework.Network;
using Fudge.Framework.Database;

namespace Fudge.Modules.Compile {
    public class CompileModule : IModule {
        private const int Port = 5556;

        public void Initialize() {            
            Listener<CompileRequest> listener = new Listener<CompileRequest>(32768, CompileRequest.Convert);
            listener.ReadValue += new EventHandler<Listener<CompileRequest>.ReadValueEventArgs>(OnValueRead);
            listener.Start(Port);
        }

        private void OnValueRead(object sender, Listener<CompileRequest>.ReadValueEventArgs e) {
            string source = e.value.Source.TrimEnd(new[] { '\0', '\r', '\n' });
            Console.WriteLine("[compile] received {0}", source);

            int handle = Host.CreateFile(source);
            Listener<CompileRequest> listener = ((Listener<CompileRequest>)sender);
            FudgeDataContext db = new FudgeDataContext();
            var language = db.Languages.SingleOrDefault(l => l.LanguageId == e.value.LanguageId);
            if (language != null) {
                try {
                    Host.Compile(language, handle);
                    listener.Send(ASCIIEncoding.Default.GetBytes("Compiled Successfully"), e.remoteHost);
                }
                catch (CompilationErrorException ex) {
                    listener.Send(ASCIIEncoding.Default.GetBytes(ex.Message), e.remoteHost);
                }
                
                Host.DeleteFile(handle);
            }
        }

        public void Dispose() {
            
        }

        public IModuleHost Host { get; set; }

        public string Name {
            get { return "Compilation Module"; }
        }

        public string Version {
            get { return "1.0"; }
        }
    }
}
