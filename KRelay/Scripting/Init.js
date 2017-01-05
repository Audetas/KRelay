var LibKRelay = importNamespace("LibKRelay");
var KRelay = importNamespace("KRelay");

KRelay.Program.Listner = new LibKRelay.ClientListener();
KRelay.Program.Loader = new KRelay.PluginLoader();

var server = LibKRelay.Data.ServerStructure.Parse("USNorthWest");
//KRelay.Program.Listener.SetDefaultServer(server);