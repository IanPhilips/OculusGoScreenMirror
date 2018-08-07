using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

// sends the frames from the camera to the udp receiever on a computer on the local network
public class SendUdpFrames : MonoBehaviour
{
		
	// TODO: set your own ip address
	public string IP="192.168.43.158";  
	
	private Texture2D tempTex;
	private int step = 0;
	private byte[] jpeg;
	
	private int port = 8051; 
   
	// connections
	IPEndPoint remoteEndPoint;
	UdpClient client;


	
    private void Start()
    {
        // udpClient = gameObject.GetComponent<UdpScreenSharingOculus>();
	    init();

		tempTex = new Texture2D(1228, 792, TextureFormat.RGB24, false);
    }

	// TODO: investigate we may want to move the computations to different OnCalls, i.e OnPreRender
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
		// we space out the computations every nth frame to reduce impact on the user's experience
		if (Time.frameCount % 9 == 0)
		{
			// used for timing costly operations
			// start = Time.realtimeSinceStartup;
			
			// 0th step is getting a texture from the renderer and reading the pixels
			if (step == 0)
			{   
				var tempRT = RenderTexture.GetTemporary(source.width, source.height); 
				Graphics.Blit(source, tempRT);
				tempTex.ReadPixels(new Rect(0, 0, source.width, source.height), 0, 0, false);
//		            Debug.Log(Time.realtimeSinceStartup - start + " readpixels"); // timed @ .02s
//		            start = Time.realtimeSinceStartup;
				RenderTexture.ReleaseTemporary(tempRT);
				step = 1;
			}

			// next step is to encode the frame to jpeg, otherwise udp client throws "TMI" error
			// this encoding could be really nice to speed up, it takes forever to do
			else if (step == 1)
			{
				step = 2;
				jpeg = tempTex.EncodeToJPG(40);
//		            Debug.Log(Time.realtimeSinceStartup - start + " make jpg"); // timed .035 seconds
//		            start = Time.realtimeSinceStartup;
			}
			
			// send the frame, relatively quick
			else
			{
				step = 0;
				sendFrame(jpeg);
			}
			
			// pass the image on
			Graphics.Blit(source, destination);
		}
		
		else
		{	
			Graphics.Blit(source, destination);
		}
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

