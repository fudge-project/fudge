using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fudge.Modules.Execute {
    public class ExecuteRequest {
        public int LanguageId { get; private set; }
        public string Source { get; private set; }
        public int TimeLimit { get; private set; }
        public int MemoryLimit { get; private set; }
        public List<string> Inputs { get; private set; }        

        public ExecuteRequest(int languageId, string source, int timeLimit, int memoryLimit, List<string> inputs) {
            LanguageId = languageId;
            Source = source;
            TimeLimit = timeLimit;
            MemoryLimit = memoryLimit;
            Inputs = inputs;            
        }

        public byte[] ToByteArray() {
            int size = 24 + Inputs.Count * 4;
            int offset = 0, count;

            foreach (string input in Inputs) {
                size += ASCIIEncoding.Default.GetByteCount(input);
            }

            size += ASCIIEncoding.Default.GetByteCount(Source);

            byte[] bytes = new byte[size];

            Array.Copy(BitConverter.GetBytes(size), 0, bytes, offset, 4);
            offset += 4;

            Array.Copy(BitConverter.GetBytes(LanguageId), 0, bytes, offset, 4);
            offset += 4;

            count = ASCIIEncoding.Default.GetByteCount(Source);

            Array.Copy(BitConverter.GetBytes(count), 0, bytes, offset, 4);
            offset += 4;
           
            Array.Copy(ASCIIEncoding.Default.GetBytes(Source), 0, bytes, offset, count);
            offset += count;

            Array.Copy(BitConverter.GetBytes(TimeLimit), 0, bytes, offset, 4);
            offset += 4;

            Array.Copy(BitConverter.GetBytes(MemoryLimit), 0, bytes, offset, 4);
            offset += 4;

            Array.Copy(BitConverter.GetBytes(Inputs.Count), 0, bytes, offset, 4);
            offset += 4;

            foreach (string input in Inputs) {
                count = ASCIIEncoding.Default.GetByteCount(input);

                Array.Copy(BitConverter.GetBytes(count), 0, bytes, offset, 4);
                offset += 4;

                Array.Copy(ASCIIEncoding.Default.GetBytes(input), 0, bytes, offset, count);
                offset += count;
            }

            return bytes;
        }

        public static Func<byte[], int, ExecuteRequest> Convert = (bytes, index) => {
            int languageId;
            string source;
            int timeLimit;
            int memoryLimit;
            int numberOfTestCases;
            List<string> inputs = new List<string>();            
            int length;
            int offset = index;            

            try {
                languageId = BitConverter.ToInt32(bytes, offset);
                offset += 4;                

                length = BitConverter.ToInt32(bytes, offset);
                offset += 4;                

                source = ASCIIEncoding.Default.GetString(bytes, offset, length);
                offset += length;                

                timeLimit = BitConverter.ToInt32(bytes, offset);
                offset += 4;                

                memoryLimit = BitConverter.ToInt32(bytes, offset);
                offset += 4;                

                numberOfTestCases = BitConverter.ToInt32(bytes, offset);
                offset += 4;                

                while (numberOfTestCases-- > 0) {
                    length = BitConverter.ToInt32(bytes, offset);
                    offset += 4;

                    inputs.Add(ASCIIEncoding.Default.GetString(bytes, offset, length));
                    offset += length;
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Unable to read data: {0}", ex.Message);
                
                return null;
            }

            return new ExecuteRequest(languageId, source, timeLimit, memoryLimit, inputs);
        };
    }
}
