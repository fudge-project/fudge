using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Fudge.Framework.Helper {

    [RunInstaller(true)]
    public class HelperInstaller : Installer {
        public HelperInstaller() {
            ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
            ServiceInstaller serviceInstaller = new ServiceInstaller();

            serviceProcessInstaller.Account = ServiceAccount.User;

            serviceProcessInstaller.Username = @"MILK\FUDGE_FX_COMPILER";
            serviceProcessInstaller.Password = @"fxcompiler";

            //serviceProcessInstaller.Username = Framework.GetCompilerUserName();
            //serviceProcessInstaller.Password = Framework.GetCompilerUserPassword().ToString();

            serviceInstaller.DisplayName = "Fudge Framework Helper Service";
            serviceInstaller.StartType = ServiceStartMode.Automatic;

            serviceInstaller.ServiceName = "Fudge Framework Helper Service";

            Installers.Add(serviceProcessInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
