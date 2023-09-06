using UnityEngine;
using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;

public class TCP
{
    public static bool connected;

    private Thread clientReceiveThread;
    public static TcpClient client;
    public static String serverIP;
    public static Int32 port;

    public GameObject blocker;

    public static NetworkStream stream;

    public static Byte[] data = new Byte[256];

    public delegate void Answer(string message);
    public static event Answer AnswerWasGetted;

    public static Dictionary<string, GlobalParameters> messageList = new Dictionary<string, GlobalParameters>();

    public TCP(String IP, Int32 port)
    {
        serverIP = IP;
        TCP.port = port;
    }

    public void TryConnect()
    {
        blocker.SetActive(true);
        
        try
        {
            clientReceiveThread = new Thread(new ThreadStart(Connect));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }

    private void Connect()
    {
        client = new TcpClient();

        try
        {
            client.Connect(serverIP, port);
            stream = client.GetStream();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            connected = false;
        }

        connected = stream != null;
    }

    public void SendMessage(String message)
    {
        data = System.Text.Encoding.UTF8.GetBytes(message);

        stream.Write(data, 0, data.Length);
        data = new Byte[256];
    }

    public string ReadCallback()
    {
        stream.Read(data, 0, data.Length);
        string callback = System.Text.Encoding.UTF8.GetString(data);
        data = new Byte[256];

        return callback;
    }

    public void CloseStream()
    {
        if (stream != null)
        {
            stream.Close();
            client.Close();
        }
    }

    public void SendGlobalProperties(GlobalParameters globalParameters)
    {
        Debug.Log("Global Parameter was send to board!");
        if (messageList.Count != 0)
        {
            AnswerWasGetted?.Invoke("300");
            Debug.Log("Bad Connection!!!");
        }
        string currentHash = globalParameters.CreateHash();
        messageList.Add(currentHash, globalParameters);

        SendMessage(JsonUtility.ToJson(globalParameters));
        //AnswerWasGetted?.Invoke(UnityEngine.Random.Range(0, 2) == 1 ? "300" : "200");
        string callback = "200";// ReadCallback();
        Debug.Log(callback);
        AnswerWasGetted?.Invoke(callback);
        messageList.Remove(currentHash);
    }
}
