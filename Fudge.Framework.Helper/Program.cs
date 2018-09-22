using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace Fudge.Framework.Helper {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main() {
            ServiceBase[] ServicesToRun;
            
            ServicesToRun = new ServiceBase[] 
			{ 
				new Helper() 
			};
            
            ServiceBase.Run(ServicesToRun);
        }
    }
}
