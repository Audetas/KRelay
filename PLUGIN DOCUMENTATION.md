#### K Relay Plugin Implementation Guide
----------------------------------------

## Getting Started
----------------------------------------
Please ensure that you're using Visual Studio 2012 or newer otherwise this guide may be innaccurate for you.
First steps:
1) Create a new C# "Class Library" Project.

2) Rightclick your project in the Solution Explorer and select "Add Reference".

3) Go to the "Browse" tab and find and add "Lib K Relay.dll".

4) Rename the default class that was created from "Class1" to something more meaningful and begin editing the class file.

5) Add references the following namespaces in your class file:
- `using Lib_K_Relay;`
- `using Lib_K_Relay.Interface;`
- `using Lib_K_Relay.Networking;`
- `using Lib_K_Relay.Networking.Packets;`
- `using Lib_K_Relay.Networking.Packets.Client;`
- `using Lib_K_Relay.Networking.Packets.Server;`

6) Make your class public and implement "IPlugin". eg "public class MyClass : IPlugin".

## The IPlugin Interface
----------------------------------------
The IPlugin interface consists of 4 methods that define a K Relay plugin. K Relay will scan your assembly (dll) for any classes that implement IPlugin and will load and attach them to the proxy.
Things to note are:
- Only one instance of your plugin will be created.
- Since IPlugin is an interface, you must implement all 4 functions.
- Methods of your plugin may be called at the same time, your plugin should be thread safe.
- If you give your class a custom constructor, it most not have any arguments or the plugin will fail to load.

IPlugin constists of the following methods:
- `string GetAuthor()` This should return the name of the creator of the plugin.
- `string GetName()` This should return the name of the plugin.
- `string GetDescription()` This should return a description of what your plugin does and any other important info.
- `string[] GetCommands()` This should return a list of any commands that your plugin uses.
- `void Initialize(Proxy)` This is called once by the plugin manager when your plugin is created and should be where you register proxy hooks and do other initialization logic.

## The PluginUtils Class
----------------------------------------
The PluginUtils class incorperates a few handy static methods.
- `void ShowGUI(Form f)` Starts a messageQueue thread for your form and shows it. Use this if to show your GUI for your plugins.
- `void Delay(int ms, Action)` Delays an action by the specified amount of milliseconds, than executes it on a thread-pool thread. Usefull for delaying packets or scheduling events.
- `NotificationPacket CreateNotification(int objectId, string message)` Creates and correctly defines the json for a notification over the specified object. Send it off and you're good to go.

## The Proxy Class
----------------------------------------
The Proxy class represents an instance of a K Relay proxy.
It contains many modifiable attributes like the remote address and port that you can modify in your plugin.
An instance of Proxy is passed to your plugn's Initialize(Proxy) method.
It also contains event handlers for the following events that you can attach to:
- `event Action<Proxy> ProxyListenStarted;`
- `event Action<Proxy> ProxyListenStopped;`
- `event Action<ClientInstance> ClientConnected;`
- `event Action<ClientInstance> ClientDisconnected;`
- `event Action<ClientInstance, Packet> ServerPacketRecieved;`
- `event Action<ClientInstance, Packet> ClientPacketRecieved;`

You can hook specific packets using the `Proxy::HookPacket(PacketType, Action<ClientInstance, Packet>)` method.
You can hook specific commands using the `Proxy::HookCommand(string command, Action<ClientInstance, string, string[]>)` method.

Here's an example of attaching to an event listener and hooking a packet:
```C#
void Initialize(Proxy proxy)
{
	proxy.ClientConnected += OnClientConnected; // Attach to an event listener
	proxy.HookPacket(PacketType.PlayerText, OnPlayerText); // Hook a specific packet
	proxy.HookCommand("connect", OnConnectCommand); // Hook a specific command
}

void OnClientConnected(ClientInstance client)
{
	Console.WriteLine("A Client connected!");
}

void OnPlayerText(ClientInstance client, Packet packet)
{
	PlayerTextPacket playerText = (PlayerTextPacket)packet;
	Console.WriteLine("You said: {0}", playerText.Text);
}

void OnConnectCommand(ClientInstance client, string command, string[] args)
{
	if (args.Length == 1)
		Console.WriteLine("Player used /connect to connect to server {0}", args[0]);
	else
		Console.WriteLine("Player used /connect but didn't specify a server!");
}
```

## The ClientInstance Class
----------------------------------------
The ClientInstance class represents a connect client to the proxy.
An instance of this class will be passed to your event handlers to determine what client is involved with the event.
For the most part, your main interactions with this class will include:
- Creating a hash map (Dictionary) of ClientInstances to store variables on a per-client basis - since there is only one instance of your plugin.
- The `SendToClient(Packet)` method, to send a specified packet to the client.
- The `SendToServer(Packet)` method, to send a specified packet to the server from the client.
- The `ObjectId` field, which is the object id that represents the client that was given by the CREATE_SUCCESS packet.

## The Packet Class
----------------------------------------
The Packet class holds the data of an intercepted packet. Packet is the generic class that is extended by classes of specific types of packets like PlayerTextPacket.
Important Packet fields:
- `byte Id`. The id of the packet. Refrain from using this in your logic. 
- `PacketType Type`. The type of the packet. Use this in your logic instead of Id!
- `bool Send`. Determines if the packet is sent. Set it to false to cancel the packet.

Important Packet Methods:
- `static Packet CreateInstance(PacketType)`. Used to create a new instance of a packet by type. Use this to create your own packets to send off. Do NOT use the packet constructor.
- `string ToString()`. Returns the name of the packet, the id, and a table of its fields and values. This is useful for debugging!

## The Serializer Class
----------------------------------------
The Serializer class is static and contains many useful serializations:
- `Dictionary<string tile, ushort id> Tiles`. All the different types of tiles in the game.
- `Dictionary<string item, ushort id> Items`. All the different items in the game.
- `Dictionary<string object, ushort id> Objects`. All the different game objects in the game.
- `Dictionary<string enemy, ushort id> Enemies`. All the different enemies in the game.
- Packets. Methods `GetPacketPacketType(id)`, `GetPacketId(type)`
- Servers. Methods `GetServerByFullName(fullName)`, `GetServerByShortName(shortName)`

## The PacketType Enumeration
----------------------------------------
The PacketType enum is the reccomended way to determine what packet is what type instead of using the packet's Id.
This way, plugins still work when IDs change.

The enum consists of:

        UNKNOWN,
        FAILURE,
        CREATE_SUCCESS,
        CREATE,
        PLAYERSHOOT,
        MOVE,
        PLAYERTEXT,
        TEXT,
        SHOOT2,
        DAMAGE,
        UPDATE,
        UPDATEACK,
        NOTIFICATION,
        NEW_TICK,
        INVSWAP,
        USEITEM,
        SHOW_EFFECT,
        HELLO,
        GOTO,
        INVDROP,
        INVRESULT,
        RECONNECT,
        PING,
        PONG,
        MAPINFO,
        LOAD,
        PIC,
        SETCONDITION,
        TELEPORT,
        USEPORTAL,
        DEATH,
        BUY,
        BUYRESULT,
        AOE,
        GROUNDDAMAGE,
        PLAYERHIT,
        ENEMYHIT,
        AOEACK,
        SHOOTACK,
        OTHERHIT,
        SQUAREHIT,
        GOTOACK,
        EDITACCOUNTLIST,
        ACCOUNTLIST,
        QUESTOBJID,
        CHOOSENAME,
        NAMERESULT,
        CREATEGUILD,
        CREATEGUILDRESULT,
        GUILDREMOVE,
        GUILDINVITE,
        ALLYSHOOT,
        SHOOT,
        REQUESTTRADE,
        TRADEREQUESTED,
        TRADESTART,
        CHANGETRADE,
        TRADECHANGED,
        ACCEPTTRADE,
        CANCELTRADE,
        TRADEDONE,
        TRADEACCEPTED,
        CLIENTSTAT,
        CHECKCREDITS,
        ESCAPE,
        FILE,
        INVITEDTOGUILD,
        JOINGUILD,
        CHANGEGUILDRANK,
        PLAYSOUND,
        GLOBAL_NOTIFICATION,
        RESKIN,
        ENTER_ARENA

## TODO: Packet Structures
----------------------------------------
## TODO: Extras
----------------------------------------
