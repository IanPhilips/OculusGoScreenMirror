
using System;
using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEditor;
using UnityEngine.UI;

// receive image frames from the send and cast them on a raw image game object
public class UdpScreenReceiver : MonoBehaviour {
   
    // TODO: drag and set this to a Raw image object located with a canvas as a parent
    public RawImage ImageDisplay;
    
    // receiving Thread
    Thread receiveThread;
 
    // udpclient object
    UdpClient client;
    private Texture2D newTexture;
    private int port = 8051;
    private byte[] newBytes;

    // start from unity3d
    public void Start()
    {
       newTexture = new Texture2D(1228, 792);
        
        // even if you click away, this program will continue to run for testing with other unity udp clients
        // set this to false if you don't need it to stay on
        Application.runInBackground = true;
        init();
    }

       
    // init
    private void init()
    {
        print("UDPSend.init()");
        // print feedback
        print("Sending to 127.0.0.1 : "+port);
        // start the thread
        receiveThread = new Thread(
            new ThreadStart(ReceiveData));
        receiveThread.IsBackground = false;
        receiveThread.Start();
 
    }

    private void Update()
    {
        if (newBytes != null)
        {
            // draw the frames on the raw image gameobject
            newTexture.LoadImage(newBytes);
            ImageDisplay.texture = newTexture;
            newBytes = null;
        }

    }

    // receive thread
    private  void ReceiveData()
    {
        client = new UdpClient(port);
        while (true)
        {
            try
            {
                // get the bytes from the sender
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                newBytes = client.Receive(ref anyIP);

            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }
 
}

