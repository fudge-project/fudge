<%@ WebService Language="C#" Class="Compiler" %>

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Linq;

/// <summary>
/// This service compiles code with specific options
/// </summary>
[WebService(Namespace = "http://fudge.fit.edu/Services/Compiler.asmx")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class Compiler : System.Web.Services.WebService {

    [WebMethod]
    public string Analyze(string rule) {
        return String.Empty;
    }

    //TODO: warningsaserrors, warninglevel
    [WebMethod]
    public string Compile(int languageId, string code) {
        byte[] compileBytes = new byte[32768];
        byte[] compileOutput = new byte[32768];
        byte[] codeBytes = ASCIIEncoding.ASCII.GetBytes(code);

        Array.Copy(BitConverter.GetBytes(languageId), compileBytes, 4);
        Array.Copy(codeBytes, 0, compileBytes, 4, codeBytes.Length);

        Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        sender.Connect(IPAddress.Parse("163.118.202.146"), 5556);
        sender.Send(compileBytes);
        sender.Receive(compileOutput);
        sender.Close();

        return ASCIIEncoding.ASCII.GetString(compileOutput.TakeWhile(b => b != 0).ToArray());        
    }

}

