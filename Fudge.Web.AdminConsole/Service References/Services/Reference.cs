﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fudge.Web.AdminConsole.Services {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://fudge.fit.edu/Services/Runs.asmx", ConfigurationName="Services.RunsSoap")]
    public interface RunsSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://fudge.fit.edu/Services/Runs.asmx/Submit", ReplyAction="*")]
        void Submit(int runId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface RunsSoapChannel : Fudge.Web.AdminConsole.Services.RunsSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class RunsSoapClient : System.ServiceModel.ClientBase<Fudge.Web.AdminConsole.Services.RunsSoap>, Fudge.Web.AdminConsole.Services.RunsSoap {
        
        public RunsSoapClient() {
        }
        
        public RunsSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RunsSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RunsSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RunsSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void Submit(int runId) {
            base.Channel.Submit(runId);
        }
    }
}
