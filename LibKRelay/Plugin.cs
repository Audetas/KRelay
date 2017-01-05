using LibKRelay.Messages.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibKRelay
{
    /// <summary>
    /// Base class for a user created plugin.
    /// </summary>
    public class Plugin
    {
        public virtual string Author { get; private set; }
        public virtual string Name { get; private set; }
        public virtual string Description { get; private set; }

        /// <summary>
        /// Called once per instance. For a plugin to initialize itself.
        /// </summary>
        public virtual void Initialize() { }

        /// <summary>
        /// Starts a message loop for the specified form instance and displays the form.
        /// </summary>
        /// <param name="gui">Form to be shown</param>
        /*
        public static void ShowGUI(Form gui)
        {
            gui.Shown += (s, e) =>
            {
                gui.WindowState = FormWindowState.Minimized;
                gui.Show();
                gui.WindowState = FormWindowState.Normal;
            };

            Task.Run(() => gui.ShowDialog());
        }*/

        /// <summary>
        /// Displays a form containing a configuration panel based off of the provided Settings objects.
        /// </summary>
        /// <param name="settingsObject">Settings to base the form off of</param>
        /// <param name="title">Title of the form to be shown</param>
        /*
        public static void ShowGenericSettingsGUI(dynamic settingsObject, string title)
        {
            ShowGUI(new FrmGenericSettings(settingsObject, title));
        }*/

        /// <summary>
        /// Waits the specified amount of ms then invokes the callback.
        /// (Wait time is accurate down to ~50ms measurements)
        /// </summary>
        /// <param name="ms">Amount of time to wait</param>
        /// <param name="callback">Action to be invoked</param>
        public static void Delay(int ms, Action callback)
        {
            Task.Run(() =>
            {
                Thread.Sleep(ms);
                callback();
            });
        }

        /// <summary>
        /// Displays a notification above a specified object.
        /// </summary>
        /// <param name="objectId">Object to display the notification on</param>
        /// <param name="message">Message of the notification</param>
        /// <returns></returns>
        public static Notification CreateNotification(int objectId, string message)
        {
            return CreateNotification(objectId, 0x00FFFF, message);
        }

        /// <summary>
        /// Displays a notification above a specified object.
        /// </summary>
        /// <param name="objectId">Object to display the notification on</param>
        /// <param name="message">Message of the notification</param>
        /// <param name="color">Color of the notification text</param>
        /// <returns></returns>
        public static Notification CreateNotification(int objectId, int color, string message)
        {
            Notification notif = new Notification();
            notif.ObjectId = objectId;
            notif.Message = "{\"key\":\"blank\",\"tokens\":{\"data\":\"" + message + "\"}}";
            notif.Color = color;
            return notif;
        }

        /// <summary>
        /// Creates an in-game message with the Oryx format and coloring.
        /// </summary>
        /// <param name="sender">Message sender</param>
        /// <param name="message">Message text</param>
        /// <returns></returns>
        public static Text CreateOryxNotification(string sender, string message)
        {
            Text text = new Text();
            text.BubbleTime = 0;
            text.CleanText = message;
            text.Name = "#" + sender;
            text.NumStars = -1;
            text.ObjectId = -1;
            text.Recipient = "";
            text.RawText = message;
            return text;
        }
    }
}
