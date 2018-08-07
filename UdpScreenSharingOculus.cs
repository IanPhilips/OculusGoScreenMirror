
 
/*
 
    -----------------------
    UDP-Send
    -----------------------
    // [url]http://msdn.microsoft.com/de-de/library/bb979228.aspx#ID0E3BAC[/url]
   
    // > gesendetes unter
    // 127.0.0.1 : 8050 empfangen
   
    // nc -lu 127.0.0.1 8050
 
        // todo: shutdown thread at the end
*/
using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class UdpScreenSharingOculus : MonoBehaviour
{   
    // TODO: set your own ip address
    public string IP="192.168.43.158";  
    private int port = 8051; 
   
    // connections
    IPEndPoint remoteEndPoint;
    UdpClient client;
    
    // automatically called by Unity   
    public void Start()
    {
        init();
    }
    
    // close client
    private void OnDestroy()
    {
        if (client!= null)
            client.Close();
    }
   
    // initialize client + endpoint
    public void init()
    {
        print("UDPSend.init()");
        // initialize the endpoint
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
        client = new UdpClient();
       
        // print feedback
        print("Sending to "+IP+" : "+port);
   
    }

    // send frame data to the receiever
    public void sendFrame(byte[] data)
    {
        try
        {
            client.Send(data, data.Length, remoteEndPoint);
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }

   
}