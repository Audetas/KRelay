using System;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace K_Relay.Util
{
    public class TextBoxStreamWriter : TextWriter
    {
        RichTextBox _output = null;

        public TextBoxStreamWriter(RichTextBox output)
        {
            _output = output;
        }

        public override void Write(char value) // TODO: Make more efficient
        {
            base.Write(value);
            if (_output.IsHandleCreated)
                _output.Invoke(new MethodInvoker(() => _output.AppendText(value.ToString())));
            else if (!_output.InvokeRequired)
                _output.AppendText(value.ToString());
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}