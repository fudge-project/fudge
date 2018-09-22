using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using System.Security;
using System.Runtime.InteropServices;
using System.IO;

namespace Fudge.Framework {
    public static class Util {
        public static void Append(this SecureString obj, string str) {
            foreach (char c in str.ToCharArray()) {
                obj.AppendChar(c);
            }
        }

        public static void KillAndWait(this Process obj) {
            try {
                obj.Kill();
                obj.WaitForExit();
            }
            catch (Exception) { }
        }
    }
}
