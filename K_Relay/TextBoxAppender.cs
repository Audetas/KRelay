using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K_Relay
{
    public static class TextBoxAppender
    {
        public static List<Appending> CurrentAppendings { get; private set; }

        static TextBoxAppender()
        {
            CurrentAppendings = new List<Appending>();
        }

        public static void ClearBoxCache(RichTextBox box)
        {
            List<Appending> boxes = new List<Appending>();

            foreach (var app in CurrentAppendings)
                boxes.Add(app);

            foreach (var a in boxes)
                CurrentAppendings.Remove(a);
        }

        public static Appending AppendText(RichTextBox box, string text, Color color, Boolean bold)
        {
            var append = Appending.Create(box, text, color, bold);
            if (CurrentAppendings.Count(_ => _.IsEqualTo(box, text, bold)) > 0)
                CurrentAppendings.RemoveAt(CurrentAppendings.FindIndex(_ => _.IsEqualTo(box, text, bold)));
            CurrentAppendings.Add(append);
            return append;
        }

        public class Appending
        {
            public RichTextBox TextBox { get; private set; }
            public string Text { get; private set; }
            public Color Color { get; private set; }
            public bool Bold { get; private set; }

            internal static Appending Create(RichTextBox box, string text, Color color, bool bold)
            {
                return new Appending
                {
                    TextBox = box,
                    Text = text,
                    Color = color,
                    Bold = bold
                };
            }

            public bool IsEqualTo(RichTextBox box, string text, bool bold)
            {
                return box == this.TextBox && text == this.Text && bold == this.Bold;
            }
        }

        public static void ClearCurrentBoxes()
        {
            foreach (var box in CurrentAppendings)
                box.TextBox.Clear();
        }
    }
}
