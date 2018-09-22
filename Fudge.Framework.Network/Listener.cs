using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace Fudge.Framework.Network {
    public class Listener<T> {
        public const int MaximumBacklog = 64;

        public class ReadValueEventArgs : EventArgs {
            public ReadValueEventArgs(T value, Socket remoteHost) {
                this.value = value;
                this.remoteHost = remoteHost;
            }

            public T value;
            public Socket remoteHost;
        }

        public class StateObject {
            public Socket workSocket;
            public int offset = 0;

            public byte[] size;
            public byte[] buffer;

            public StateObject() {
                size = new byte[4];
            }

            public StateObject(byte[] buffer) {
                this.buffer = buffer;
            }
        }

        private ManualResetEvent allDone;
        private Socket listener;
        public int bufferSize = -1;
        private Func<byte[], int, T> converter;

        private event EventHandler<ReadValueEventArgs> readValue;
        public event EventHandler<ReadValueEventArgs> ReadValue {
            add {
                readValue += value;
            }

            remove {
                readValue -= value;
            }
        }

        public Listener(Func<byte[], int, T> converter) {
            allDone = new ManualResetEvent(true);
            this.converter = converter;
        }

        public Listener(int size, Func<byte[], int, T> converter) : this(converter) {
            bufferSize = size;
        }

        private void AcceptCallback(IAsyncResult asyncResult) {

            Socket listener = (Socket)asyncResult.AsyncState;
            Socket handler = listener.EndAccept(asyncResult);

            allDone.Set();

            StateObject state = new StateObject();
            state.workSocket = handler;

            if (bufferSize == -1) {                
                handler.BeginReceive(state.size, 0, 4, 0, new AsyncCallback(ReadCallback), state);
            }
            else {
                state.buffer = new byte[bufferSize];
                handler.BeginReceive(state.buffer, 0, state.buffer.Length, 0, new AsyncCallback(ReadCallback), state);
            }            
        }

        private void ReadCallback(IAsyncResult asyncResult) {

            StateObject state = (StateObject)asyncResult.AsyncState;
            Socket handler = state.workSocket;

            if (state.buffer == null) {
                handler.EndReceive(asyncResult);
                
                state.buffer = new byte[BitConverter.ToInt32(state.size, 0) - 4];                
            }
            else {
                state.offset += handler.EndReceive(asyncResult);
            }

            if (state.offset < state.buffer.Length) {                
                handler.BeginReceive(state.buffer, state.offset, state.buffer.Length - state.offset, 0, new AsyncCallback(ReadCallback), state);
            }
            else {                
                readValue(this, new ReadValueEventArgs(converter(state.buffer, 0), handler));
                //handler.Close();
            }
        }
        
        private void SendCallback(IAsyncResult asyncResult) {
            StateObject state = (StateObject)asyncResult.AsyncState;
            Socket handler = state.workSocket;
            
            state.offset += handler.EndSendTo(asyncResult);
            
            if(state.offset < state.buffer.Length) {
                handler.BeginSend(state.buffer, state.offset, state.buffer.Length - state.offset, 0, new AsyncCallback(SendCallback), state);
            }
            else {
                //handler.Close();
            }
        }

        public void Start(int port) {

            IPEndPoint localEp = new IPEndPoint(IPAddress.Any, port);

            listener = new Socket(localEp.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEp);
            listener.Listen(MaximumBacklog);

            while (true) {
                allDone.Reset();
                listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                allDone.WaitOne();
            }
        }
        
        public void Send(byte[] bytes, Socket remoteHost) {  
            StateObject state = new StateObject(bytes);
            state.workSocket = remoteHost;
                             
            state.workSocket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, new AsyncCallback(SendCallback), state);           
        }
    }
}
