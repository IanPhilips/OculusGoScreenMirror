# OculusGoScreenMirror


This shows very poor fps but at least is enough to guide a user through any complicated menus. I'm very open to PRs and suggestions

In your screen receiever local computer unity project: place the UdpScreenReceiver.cs object on any object and drag a rawimage gameobject to its public ImageDisplay field

In your Oculus unity project: place the SendUdpFrames.cs on your camera gameobject and set the local computer's IP address to its public IP field
