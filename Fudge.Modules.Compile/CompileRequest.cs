using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fudge.Modules.Compile {
    public class CompileRequest {
        public int LanguageId { get; private set; }
        public string Source { get; private set; }
        public const int Size = 32768;
        
        public CompileRequest(int languageId, string source) {
            LanguageId = languageId;
            Source = source;
        }

        public static Func<byte[], int, CompileRequest> Convert = (bytes, index) =>
            new CompileRequest(BitConverter.ToInt32(bytes, index), 
            ASCIIEncoding.Default.GetString(bytes, index + 4, bytes.Length - 4));
    }
}
