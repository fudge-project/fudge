using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security;
using System.Text.RegularExpressions;

namespace Fudge.Framework {
    public static class Framework {                            
        public static string FormatError(string error) {            
            return Regex.Replace(error, @"(\w:\\|\w:/){0,1}((\w| )+\\|(\w| )+/)+((\w| )+\.\w+){0,1}", String.Empty);
        }

        public static string GetDirectory() {
            #if DEBUG
                return @"C:\Program Files\Fudge Framework\";
            #else
                return Environment.GetEnvironmentVariable("FUDGE_FX_DIR");
            #endif
        }

        public static string GetDomain() {
            return Environment.MachineName;
        }

        public static string GetCompilerUserName() {
            return Environment.GetEnvironmentVariable("FUDGE_FX_COMPILER_USER");
        }

        public static SecureString GetCompilerUserPassword() {
            SecureString s = new SecureString();
            s.Append("fxcompiler");

            return s;
        }

        public static string GetExecUserName() {
            return Environment.GetEnvironmentVariable("FUDGE_FX_EXEC_USER");
        }

        public static SecureString GetExecUserPassword() {
            SecureString s = new SecureString();
            s.Append("fxexec");

            return s;
        }

        public static string GetLanguageDirectory() {
            return Path.Combine(GetDirectory(), @"Languages\");
        }
        
        public static string GetModuleDirectory() {
            return Path.Combine(GetDirectory(), @"Modules\");
        }
        
        public static int GetProcessQueryRate() {
            return 32;
        }
        
        public static string GetTempDirectory() {
            return Path.Combine(GetDirectory(), @"Temp\");
        }

    }
}
