using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibKRelay.Data
{
    public class TextureStructure
    {
        public string File { get; private set; }
        public int Index { get; private set; }

        public TextureStructure(XElement texture)
        {
            File = texture.ElemDefault("File", "");
            Index = texture.ElemDefault("Index", "0").ParseInt();
        }
    }
}
