<%@ WebService Language="C#" Class="Runs" %>

using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Management;
using System.Web.Script.Services;
using System.Web.Services;
using System.Runtime.InteropServices;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Fudge.Framework.Database;

/// <summary>
/// This service compiles and judges a specific run
/// </summary>
[WebService(Namespace = "http://fudge.fit.edu/Services/Runs.asmx")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class Runs : System.Web.Services.WebService {

    FudgeDataContext db = new FudgeDataContext();

    [WebMethod]
    public void Submit(int runId) {

        if (db.Runs.Any(r => r.RunId == runId)) {
            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            sender.Connect(IPAddress.Parse("163.118.202.146"), 5555);
            sender.Send(BitConverter.GetBytes(runId));
            sender.Close();
        }
        else {
            throw new Exception("Run " + runId + " is not a valid run");
        }
    }
}

