﻿using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.DataObjects;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Util;
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
        {
            string[] commands = { "/worldedit" };
            return commands;
        }

        public void Initialize(Proxy proxy)
        {
            proxy.HookPacket(PacketType.USEITEM, OnUseItem);
            proxy.HookPacket(PacketType.PLAYERTEXT, OnPlayerText);
            proxy.HookPacket(PacketType.UPDATEACK, OnUpdateAck);
        }

        private void OnUseItem(ClientInstance client, Packet packet)
        {
            if (Editing && !_cancel)
            {
                UseItemPacket useItem = (UseItemPacket)packet;
                useItem.Send = false;

                UpdatePacket update = (UpdatePacket)Packet.CreateInstance(PacketType.UPDATE);
                update.Drops = new int[0];
                update.NewObjs = new Entity[0];
                update.Tiles = new Tile[1];

                Tile paintedTile = new Tile();
                paintedTile.X = (short)Math.Floor(useItem.ItemUsePos.X);
                paintedTile.Y = (short)Math.Floor(useItem.ItemUsePos.Y);
                paintedTile.Type = Serializer.Tiles[SelectedTile];
                update.Tiles[0] = paintedTile;

                _cancel = true;
                client.SendToClient(update);
            }
        }

        private void OnPlayerText(ClientInstance client, Packet packet)
        {
            PlayerTextPacket playerText = (PlayerTextPacket)packet;
            if (playerText.Text.ToLower() == "/worldedit")
            {
                playerText.Send = false;
                new Thread(() => new FrmWorldEdit(this).ShowDialog()).Start();
            }
        }

        private void OnUpdateAck(ClientInstance client, Packet packet)
        {
            if (_cancel)
            {
                packet.Send = false;
                _cancel = false;
            }
        }
    }
}