# OculusGoScreenMirror for Unity

update: haven't tried this yet but this could be a better option if you don't want to do anything other than display the oculus screen: https://pixvana.com/sharing-your-oculus-go-screen-on-your-laptop/

This shows very poor fps on the receiver side (1 frame receieved per 27 frames on the sender side) and there is a 20ms and 30ms delay every now and then on the sending side between frames for the jpeg conversion and ReadPixels() computations, but at least is enough to introduce a new user to something new/complicated. I'm very open to PRs and would love suggestions for speed improvements. Unity hasn't opened the AsyncGPUReadback API for android yet but that should help the fps when they do. 

## Usage
1. In your screen receiver local computer unity project (which should work on mac, pc, linux): place the UdpScreenReceiver.cs object on any object and drag a rawimage gameobject to its public ImageDisplay field

2. In your Oculus unity project: place the SendUdpFrames.cs on your camera gameobject and set the local computer's IP address to its public IP field

3. build on the oculus and press play or build and run on the receiver side
