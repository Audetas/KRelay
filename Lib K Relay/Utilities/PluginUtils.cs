using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lib_K_Relay.Utilities
{
    public static class PluginUtils
    {
        public static void LogPluginException(Exception e, string caller)
        {
            MethodBase site = e.TargetSite;
            string methodName = site == null ? "<null method reference>" : site.Name;
            string className = site == null ? "" : site.ReflectedType.Name;

            Console.WriteLine(
                "[Plugin Error] An exception was thrown\nwithin a {0} callback\nat {1}\nHere's the exception report:\n{2}",
                caller, className + "." + methodName, e);
        }

        public static void ShowGUI(Form gui)
        {
            gui.Shown += (s, e) =>
                {
                    gui.WindowState = FormWindowState.Minimized;
                    gui.Show();
                    gui.WindowState = FormWindowState.Normal;
                };

            Thread messageLoop = new Thread(() => gui.ShowDialog());
            messageLoop.Start();
        }

        public static void ShowGenericSettingsGUI(dynamic settingsObject, string title, TitleColor style = TitleColor.Pink)
        {
            ShowGUI(new FrmGenericSettings(settingsObject, title, style));
        }

        public static void Delay(int ms, Action callback)
        {
            Task.Run(() =>
                {
                    Thread.Sleep(ms);
                    callback();
                });
        }

        public static NotificationPacket CreateNotification(int objectId, string message)
        {
            return CreateNotification(objectId, 0x00FFFF, message);
        }

        public static NotificationPacket CreateNotification(int objectId, int color, string message)
        {
            NotificationPacket notif = (NotificationPacket)Packet.Create(PacketType.NOTIFICATION);
            notif.ObjectId = objectId;
            notif.Message = "{\"key\":\"blank\",\"tokens\":{\"data\":\"" + message + "\"}}";
            notif.Color = color;
            return notif;
        }

        public static TextPacket CreateOryxNotification(string sender, string message)
        {
            TextPacket tpacket = (TextPacket)Packet.Create(PacketType.TEXT);
            tpacket.BubbleTime = 0;
            tpacket.CleanText = message;
            tpacket.Name = "#" + sender;
            tpacket.NumStars = -1;
            tpacket.ObjectId = -1;
            tpacket.Recipient = "";
            tpacket.Text = message;
            return tpacket;
        }
    }
}
