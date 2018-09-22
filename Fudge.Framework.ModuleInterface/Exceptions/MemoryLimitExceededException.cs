﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fudge.Framework.ModuleInterface {
    [global::System.Serializable]
    public class MemoryLimitExceededException : Exception {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public MemoryLimitExceededException() { }
        public MemoryLimitExceededException(string message) : base(message) { }
        public MemoryLimitExceededException(string message, Exception inner) : base(message, inner) { }
        protected MemoryLimitExceededException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
