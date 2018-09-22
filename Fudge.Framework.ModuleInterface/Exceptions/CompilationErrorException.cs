using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

namespace Fudge.Framework.ModuleInterface {
    [global::System.Serializable]
    public class CompilationErrorException : Exception {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public CompilationErrorException() { }
        public CompilationErrorException(string message) : base(message) { }
        public CompilationErrorException(string message, Exception inner) : base(message, inner) { }
        protected CompilationErrorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
