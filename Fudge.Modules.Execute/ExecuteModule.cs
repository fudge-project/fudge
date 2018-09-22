using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.ModuleInterface;
using Fudge.Framework.Network;
using Fudge.Framework.Database;
using System.Net.Sockets;

namespace Fudge.Modules.Execute {
    public class ExecuteModule : IModule {

        private const int Port = 5557;
        private const int OutputLimit = 262144;        

        public void Initialize() {
            Listener<ExecuteRequest> listener = new Listener<ExecuteRequest>(ExecuteRequest.Convert);
            listener.ReadValue += new EventHandler<Listener<ExecuteRequest>.ReadValueEventArgs>(listener_ReadValue);
            listener.Start(Port);
        }

        public static ExecuteResponse SendRequest(ExecuteRequest request, Socket sender) {
            byte[] size = new byte[4];
            byte[] data;

            sender.Send(request.ToByteArray());
            sender.Receive(size, 0, 4, SocketFlags.None);

            data = new byte[BitConverter.ToInt32(size, 0)];
            sender.Receive(data);

            return new ExecuteResponse(data);
        }

        void listener_ReadValue(object sender, Listener<ExecuteRequest>.ReadValueEventArgs e) {
            ExecuteResponse response = new ExecuteResponse();
            
            if (e.value != null)
            {
                FudgeDataContext db = new FudgeDataContext();                
                Language language = db.Languages.SingleOrDefault(l => l.LanguageId == e.value.LanguageId);

                int sourceHandle = Host.CreateFile(e.value.Source);

                try {
                    int executableHandle = Host.Compile(language, sourceHandle);

                    for (int i = 0; i < e.value.Inputs.Count; ++i) {
                        ProcessResult result = Host.Execute(executableHandle, e.value.MemoryLimit, e.value.TimeLimit, OutputLimit, e.value.Inputs[i]);

                        response.ExecutionTime += (int)result.ExecutionTime;
                        response.MemoryUsage = (int)Math.Max(response.MemoryUsage, result.WorkingSet);

                        if (response.ExecutionTime > e.value.TimeLimit) {
                            throw new TimeLimitExceededException();
                        }
                        
                        response.Outputs.Add(result.Output);
                    }
                }

                catch (CompilationErrorException ex) {
                    response.Response = 1;
                    response.Outputs.Add(ex.Message);
                }

                catch (TimeLimitExceededException) {
                    response.Response = 2;
                }

                catch (MemoryLimitExceededException) {
                    response.Response = 3;
                }

                catch (RuntimeErrorException ex) {
                    response.Response = 4;
                    response.Outputs.Add(ex.Message);
                }
            }

            ((Listener<ExecuteRequest>)sender).Send(response.ToByteArray(), e.remoteHost);
        }

        public void Dispose() {

        }

        public IModuleHost Host { get; set;}

        public string Name {
            get { return "Execute Module";}
        }

        public string Version {
            get { return "1.0";}
        }

    }
}
