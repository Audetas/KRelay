// --------------------------------------------
// This is the K Relay initialization script.
// It provides the syntactic structure for
// the command and scripting interface, as
// well as starts the proxy and loads plugins.
// --------------------------------------------
// This file can be modified to suit your needs!
// Only modify it if you know what you're doing.
// Please read the language design doc to fully
// understand how the scripting interface works.
// --------------------------------------------

// TODO: add random guid to char/list requests

// Syntactic sugar
define Builtin., KRelay.Scripting.ScriptHelpers.__Builtin_
define .\\, $scripts
define annotate , constant __help__
define help:, ok $__help__

// Bind built in functions to commands
// These are basically the commands available to you by DEFAULT
// In the KRelay console and in scripts.
bind run, KRelay.Scripting.ScriptHost.LoadAndRun
annotate run, Loads and runs a script from the specified path or url.\nUsage: run <path/url>

bind log, LibKRelay.ConsoleEx.Log, LibKRelay
annotate log, Logs to the console.\nUsage: log <string>

bind ok, LibKRelay.ConsoleEx.Ok, LibKRelay
annotate ok, Logs to the console.\nUsage: ok <string>

bind warn, LibKRelay.ConsoleEx.Warn, LibKRelay
annotate warn, Logs to the console.\nUsage: warn <string>

bind error, LibKRelay.ConsoleEx.Error, LibKRelay
annotate error, Logs to the console.\nUsage: error <string>

bind write, System.Console.Write
annotate write, Writes to the console.\nUsage: write <string>

bind input, LibKRelay.ConsoleEx.Input, LibKRelay
annotate input, Reads the user's console input.\nUsage: input

bind backcolor, System.Console.BackgroundColor
annotate backcolor, Sets the console text's back color.\nUsage: backcolor <ConsoleColor enum value>

bind forecolor, System.Console.ForegroundColor
annotate forecolor, Sets the console text's fore color.\nUsage: forecolor <ConsoleColor enum value>

bind async, Builtin.AsyncRun
annotate async, Runs a script from the path or url asynchronously.\nUsage: async <path/url>

bind sleep, System.Threading.Thread.Sleep
annotate sleep, Sleeps for the specified amount of milliseconds.\nUsage: sleep <int>

bind clear, System.Console.Clear
annotate clear, Clears the console log.\nUsage: clear

bind about, KRelay.Program.About
annotate about, Displays info about the program.\nUsage: about

bind verbose, KRelay.Scripting.ScriptHost.Verbose
annotate verbose, Enables or disables echoing of (post-parsed) commands when entered.\nUsage: verbose <bool>

bind settitle, System.Console.Title
annotate settitle, Sets the console title.\nUsage: settitle <string>

bind setserver, KRelay.Program.SetDefaultServer
annotate setserver, Parses and sets the default server based on input.\nUsage: setserver <server name/abbreviation/address>

bind startlisten, KRelay.Program.StartListen
annotate startlisten, Starts listening for clients on the specified address and port.\nUsage: startlisten <string:address> <int:port>

bind stoplisten, KRelay.Program.StopListen
annotate stoplisten, Stops the local client listener.\nUsage: stoplisten

bind listclients, KRelay.Scripting.ScriptHelpers.ListClients
annotate listclients, Lists the clients currently connected to the proxy.\nUsage: listclients

bind sendtoserver, KRelay.Scripting.ScriptHelpers.SendToServer
annotate sendtoserver, Creates and sends the specified packet to the specified client index.\nUsage: sendtoserver <string:packet class name> <int:client index>

bind sendtoclient, KRelay.Scripting.ScriptHelpers.SendToClient
annotate sendtoclient, Creates and sends the specified packet to the specified client index.\nUsage: sendtoclient <string:packet class name> <int:client index>

bind logpacket, KRelay.Scripting.ScriptHelpers.LogPacket
annotate logpacket, Logs the specified packet type for the specified amount of times.\nUsage: logpacket <string:packet class name> <int:amount(0 for infinity)>

bind blockpacket, KRelay.Scripting.ScriptHelpers.BlockPacket
annotate blockpacket, Blocks the specified packet type for the specified amount of times.\nUsage: blockpacket <string:packet class name> <int:amount(0 for infinity)>

bind quit, Builtin.Exit
annotate quit, Asks if you want to exit the program.\nUsage: quit

// Version Check
async .\\VersionCheck.ks

// Constants/Defaults
constant buildname, 27.7.XMAS
constant listenaddress, 127.0.0.1
constant listenport, 2050
constant defaultserver, USNorthWest

// Welcome the user
settitle K Relay - Build: $buildname
forecolor Green
log \e
log  __   ___       _______    _______  ___            __       ___  ___   
log  |/"| /  ")     /"      \  /"     "||"  |          /""\     |"  \/"  | 
log  (: |/   /     |:        |(: ______)||  |         /    \     \   \  /  
log  |    __/      |_____/   ) \/    |  |:  |        /' /\  \     \\  \/   
log  (// _  \       //      /  // ___)_  \  |___    //  __'  \    /   /    
log  |: | \  \     |:  __   \ (:      "|( \_|:  \  /   /  \\  \  /   /     
log  (__|  \__)    |__|  \___) \_______) \_______)(___/    \___)|___/      
log                                                                        
log \e

// Start the proxy
setserver $defaultserver
startlisten $listenaddress, $listenport

// Info
forecolor White
log \e
log Enter a command or type 'quit' to close K Relay.
log Enter help:<command_name> to get info and usage details for a command.
log \e