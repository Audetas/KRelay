using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lib_K_Relay.Interface
{
    public interface IPlugin
    {
        string GetAuthor();
        string GetName();
        string GetDescription();
        string[] GetCommands();

        void Initialize(Proxy proxy);
    }
}
