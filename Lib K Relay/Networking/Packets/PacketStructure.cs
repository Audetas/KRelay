using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets
{
    public class PacketStructure
    {
        public PacketType Type;

        private List<Tuple<string, string>> _data;

        public PacketStructure(PacketType type)
        {
            _data = new List<Tuple<string, string>>();
            Type = type;
        }

        public string this[int index]
        {
            get { return _data[index].Item2; }
        }

        public string ElementAt(int index)
        {
            return _data[index].Item1;
        }

        public int Elements()
        {
            return _data.Count();
        }

        public void DefineElement(string elementName, string elementType)
        {
            _data.Add(new Tuple<string, string>(elementName, elementType));
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append("Packet Structure for " + Type.ToString() + "\n{\n");

            foreach (var pair in _data)
                s.Append("\t" + pair.Item1 + " => " + pair.Item2 + "\n");

            s.Append("}");

            return s.ToString();
        }
    }
}
