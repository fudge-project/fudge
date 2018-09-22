using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fudge.Modules.Execute {
    public class ExecuteResponse {
        public int ExecutionTime { get; set; }
        public int MemoryUsage { get; set; }
        public List<string> Outputs { get; set; }
        public int Response { get; set; }

        public ExecuteResponse() {
            Outputs = new List<string>();
        }

        public ExecuteResponse(byte[] bytes) : this() {            
            int numberOfOutputs;
            int offset = 0;

            Response = BitConverter.ToInt32(bytes, offset);
            offset += 4;

            ExecutionTime = BitConverter.ToInt32(bytes, offset);
            offset += 4;

            MemoryUsage = BitConverter.ToInt32(bytes, offset);
            offset += 4;

            numberOfOutputs = BitConverter.ToInt32(bytes, offset);
            offset += 4;

            while (numberOfOutputs-- > 0) {
                int count = BitConverter.ToInt32(bytes, offset);
                offset += 4;

                Outputs.Add(ASCIIEncoding.Default.GetString(bytes, offset, count));
                offset += count;
            }
        }

        public byte[] ToByteArray() {            
            int size = 20 + Outputs.Count * 4;
            int offset = 0;

            foreach (string output in Outputs) {
                size += ASCIIEncoding.Default.GetByteCount(output);
            }

            byte[] bytes = new byte[size];

            Array.Copy(BitConverter.GetBytes(size), 0, bytes, offset, 4);
            offset += 4;

            Array.Copy(BitConverter.GetBytes(Response), 0, bytes, offset, 4);
            offset += 4;

            Array.Copy(BitConverter.GetBytes(ExecutionTime), 0, bytes, offset, 4);
            offset += 4;

            Array.Copy(BitConverter.GetBytes(MemoryUsage), 0, bytes, offset, 4);
            offset += 4;

            Array.Copy(BitConverter.GetBytes(Outputs.Count), 0, bytes, offset, 4);
            offset += 4;

            foreach (string output in Outputs) {
                int count = ASCIIEncoding.Default.GetByteCount(output);

                Array.Copy(BitConverter.GetBytes(count), 0, bytes, offset, 4);
                offset += 4;

                Array.Copy(ASCIIEncoding.Default.GetBytes(output), 0, bytes, offset, count);
                offset += count;
            }

            return bytes;
        }
    }
}
