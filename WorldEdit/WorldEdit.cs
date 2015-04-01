using Lib_K_Relay;
using Lib_K_Relay.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldEdit
{
    public class WorldEdit : IPlugin
    {
        public string GetAuthor()
        { return "KrazyShank / Kronks, thanks to 059"; }

        public string GetName()
        { return "Live World Editor"; }

        public string GetDescription()
        { return "Allows you to edit the tiles of the map while in game.\nUse the /worldedit command to begin."; }

        public void Initialize(Proxy proxy)
        {
            throw new NotImplementedException();
        }
    }
}
