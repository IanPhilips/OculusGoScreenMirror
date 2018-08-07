# OculusGoScreenMirror

update: haven't tried this yet but this could be a better option: https://pixvana.com/sharing-your-oculus-go-screen-on-your-laptop/

This shows very poor fps on the receiver side (maybe 1 or 2 fps) and there is a 20ms and 30ms delay every now and then between frames for the jpeg conversion and ReadPixels() computations, but at least is enough to introduce a user to something new/complicated. I'm very open to PRs and would love suggestions for speed improvements. Unity hasn't opened the AsyncGPUReadback API for android yet but that should help when they do. 

In your screen receiver local computer unity project (which should work on mac, pc, linux): place the UdpScreenReceiver.cs object on any object and drag a rawimage gameobject to its public ImageDisplay field

In your Oculus unity project: place the SendUdpFrames.cs on your camera gameobject and set the local computer's IP address to its public IP field
