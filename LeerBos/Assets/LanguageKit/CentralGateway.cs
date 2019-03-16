using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class CentralGateway : MonoBehaviour
{
    volatile Socket centralConnection = null;
    volatile IPEndPoint centralEndPoint = null;
    volatile byte[] buffer = new byte[1024];

    // Use this for initialization
    void Start()
    {
        string ipadress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString();
        centralConnection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        centralEndPoint = new IPEndPoint(IPAddress.Parse(ipadress), 2237);
        Debug.Log(ipadress);

        SetupCentralConnection();
    }

    //private void OnApplicationQuit()
    //{
    //    centralConnection.Disconnect(false);
    //}

    private void SetupCentralConnection()
    {
        try
        {
            Debug.Log("Connecting to Central");
            centralConnection.BeginConnect(centralEndPoint, ConnectCallback, centralConnection);
        }
        catch (SocketException e)
        {
            Debug.Log("SocketException: " + e);
        }
    }

    private void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            Debug.Log("Connected to Central");
            centralConnection.EndConnect(ar);
            Debug.Log("Start listening to Central");
            centralConnection.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceivedMessage, centralConnection);
        }
        catch (SocketException e)
        {
            Debug.Log("SocketException: " + e);
        }
    }

    private void ReceivedMessage(IAsyncResult ar)
    {
        try
        {
            int received = centralConnection.EndReceive(ar);

            if (received > 0)
            {
                ProcessCommand(buffer.Skip(1).Take(2).ToArray(), buffer[0]);
            }
            centralConnection.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceivedMessage, centralConnection);
        }
        catch (SocketException e)
        {
            Debug.Log("SocketException: " + e);
        }
    }

    public void ProcessCommand(byte[] payload, byte command)
    {
        switch (command)
        {
            case 1:
                string strData = ASCIIEncoding.ASCII.GetString(payload);
                Debug.Log("Received from Central: " + strData);
                ChangeLanguage(strData);
                break;
        }
        buffer = new byte[1024];
    }

    private void ChangeLanguage(string language)
    {
        LanguageController.Instance.LoadLanguage(language);
        Debug.Log("Language: " + language);
    }
}