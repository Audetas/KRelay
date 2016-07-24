using Lib_K_Relay;
using Lib_K_Relay.GameData;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.DataObjects;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorldEdit
{
    public class WorldEdit : IPlugin
    {
        public bool Editing = false;
        public string SelectedTile = null;

        private bool _cancel = false;

        public string GetAuthor()
        { return "KrazyShank / Kronks, thanks to 059"; }

        public string GetName()
        { return "Live World Editor"; }

        public string GetDescription()
        { return "Allows you to edit the tiles of the map while in game.\nUse the /worldedit command to begin."; }

        public string[] GetCommands()
        { return new string[] { "/worldedit" }; }

        public void Initialize(Proxy proxy)
        {
            proxy.HookPacket(PacketType.USEITEM, OnUseItem);
            proxy.HookPacket(PacketType.UPDATEACK, OnUpdateAck);
            proxy.HookCommand("worldedit", OnWorldEditCommand);
        }

        private void OnUseItem(Client client, Packet packet)
        {
            if (Editing && !_cancel)
            {
                UseItemPacket useItem = (UseItemPacket)packet;
                useItem.Send = false;

                UpdatePacket update = (UpdatePacket)Packet.Create(PacketType.UPDATE);
                update.Drops = new int[0];
                update.NewObjs = new Entity[0];
                update.Tiles = new Tile[1];

                Tile paintedTile = new Tile();
                paintedTile.X = (short)Math.Floor(useItem.ItemUsePos.X);
                paintedTile.Y = (short)Math.Floor(useItem.ItemUsePos.Y);
                paintedTile.Type = GameData.Tiles.ByName(SelectedTile).ID;
                update.Tiles[0] = paintedTile;

                _cancel = true;
                client.SendToClient(update);
            }
        }

        private void OnWorldEditCommand(Client client, string command, string[] args)
        {
            PluginUtils.ShowGUI(new FrmWorldEdit(this));
        }

        private void OnUpdateAck(Client client, Packet packet)
        {
            if (_cancel)
            {
                packet.Send = false;
                _cancel = false;
            }
        }
    }
}
