using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Controls;
using MetroFramework.Drawing;
using MetroFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K_Relay.Controls
{
    public class MetroListBox : Control, IMetroControl
    {
        private ListBoxBase baseListBox;
        private MetroScrollBar scrollBar;
        public ListBoxBase ListBox { get { return baseListBox; } }

        public event EventHandler SelectedIndexChanged;

        public MetroListBox()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
            base.TabStop = false;

            InitializeBaseListBox();
            UpdateBaseListBox();
            AddEventHandler();       
        }

        #region Interface

        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public event EventHandler<MetroPaintEventArgs> CustomPaintBackground;
        protected virtual void OnCustomPaintBackground(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintBackground != null)
            {
                CustomPaintBackground(this, e);
            }
        }

        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public event EventHandler<MetroPaintEventArgs> CustomPaint;
        protected virtual void OnCustomPaint(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaint != null)
            {
                CustomPaint(this, e);
            }
        }

        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public event EventHandler<MetroPaintEventArgs> CustomPaintForeground;
        protected virtual void OnCustomPaintForeground(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintForeground != null)
            {
                CustomPaintForeground(this, e);
            }
        }

        private MetroColorStyle metroStyle = MetroColorStyle.Default;
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        [DefaultValue(MetroColorStyle.Default)]
        public MetroColorStyle Style
        {
            get
            {
                if (DesignMode || metroStyle != MetroColorStyle.Default)
                {
                    return metroStyle;
                }

                if (StyleManager != null && metroStyle == MetroColorStyle.Default)
                {
                    return StyleManager.Style;
                }
                if (StyleManager == null && metroStyle == MetroColorStyle.Default)
                {
                    return MetroDefaults.Style;
                }

                return metroStyle;
            }
            set { metroStyle = value; }
        }

        private MetroThemeStyle metroTheme = MetroThemeStyle.Default;
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        [DefaultValue(MetroThemeStyle.Default)]
        public MetroThemeStyle Theme
        {
            get
            {
                if (DesignMode || metroTheme != MetroThemeStyle.Default)
                {
                    return metroTheme;
                }

                if (StyleManager != null && metroTheme == MetroThemeStyle.Default)
                {
                    return StyleManager.Theme;
                }
                if (StyleManager == null && metroTheme == MetroThemeStyle.Default)
                {
                    return MetroDefaults.Theme;
                }

                return metroTheme;
            }
            set { metroTheme = value; }
        }

        private MetroStyleManager metroStyleManager = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MetroStyleManager StyleManager
        {
            get { return metroStyleManager; }
            set { metroStyleManager = value; }
        }

        private bool useCustomBackColor = false;
        [DefaultValue(false)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public bool UseCustomBackColor
        {
            get { return useCustomBackColor; }
            set { useCustomBackColor = value; }
        }

        private bool useCustomForeColor = false;
    
        [DefaultValue(false)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public bool UseCustomForeColor
        {
            get { return useCustomForeColor; }
            set { useCustomForeColor = value; }
        }

        private bool useStyleColors = false;
        [DefaultValue(false)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public bool UseStyleColors
        {
            get { return useStyleColors; }
            set { useStyleColors = value; }
        }

        [Browsable(false)]
        [Category(MetroDefaults.PropertyCategory.Behaviour)]
        [DefaultValue(false)]
        public bool UseSelectable
        {
            get { return GetStyle(ControlStyles.Selectable); }
            set { SetStyle(ControlStyles.Selectable, value); }
        }

        #endregion

        #region Paint Methods

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                Color backColor = BackColor;
                baseListBox.BackColor = BackColor;

                if (!useCustomBackColor)
                {
                    backColor = MetroPaint.BackColor.Button.Normal(Theme);
                    baseListBox.BackColor = MetroPaint.BackColor.Button.Normal(Theme);
                }

                if (backColor.A == 255)
                {
                    e.Graphics.Clear(backColor);
                    return;
                }

                base.OnPaintBackground(e);

                OnCustomPaintBackground(new MetroPaintEventArgs(backColor, Color.Empty, e.Graphics));
            }
            catch
            {
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (GetStyle(ControlStyles.AllPaintingInWmPaint))
                {
                    OnPaintBackground(e);
                }

                OnCustomPaint(new MetroPaintEventArgs(Color.Empty, Color.Empty, e.Graphics));
                OnPaintForeground(e);
            }
            catch
            {
                Invalidate();
            }
        }

        protected virtual void OnPaintForeground(PaintEventArgs e)
        {
            if (useCustomForeColor)
            {
                baseListBox.ForeColor = ForeColor;
            }
            else
            {
                baseListBox.ForeColor = MetroPaint.ForeColor.Button.Normal(Theme);
            }

            Color borderColor = MetroPaint.BorderColor.Button.Normal(Theme);

            if (useStyleColors)
                borderColor = MetroPaint.GetStyleColor(Style);

            using (Pen p = new Pen(borderColor))
            {
                e.Graphics.DrawRectangle(p, new Rectangle(0, 0, Width - 1, Height - 1));
            }
        }

        #endregion

        #region Overridden Methods

        public override void Refresh()
        {
            base.Refresh();
            UpdateBaseListBox();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateBaseListBox();
        }

        private void UpdateBaseListBox()
        {
            if (baseListBox == null) return;
            baseListBox.Location = new Point(3, 3);
            baseListBox.Size = new Size(Width - 16, Height - 6);
            scrollBar.Height = this.baseListBox.Height;
            scrollBar.Location = new Point(baseListBox.Width + 2, 3);

            if (scrollBar.Height == 0) return;

            scrollBar.Maximum = this.ItemHeight * this.baseListBox.Items.Count;
            scrollBar.Minimum = 0;

            scrollBar.LargeChange = scrollBar.Maximum / scrollBar.Height + Height;
            scrollBar.SmallChange = 15;
        }

        #endregion

        private void InitializeBaseListBox()
        {
            this.SuspendLayout();
            if (baseListBox != null) return;

            scrollBar = new MetroScrollBar();
            baseListBox = new ListBoxBase();

            baseListBox.BorderStyle = BorderStyle.None;
            baseListBox.Location = new Point(3, 3);
            baseListBox.Size = new Size(Width - 16, Height - 6);
            scrollBar.Scroll += scrollBar_Scroll;

            Size = new Size(baseListBox.Width + 16, baseListBox.Height + 6);

            baseListBox.TabStop = true;

            scrollBar.Maximum = this.ItemHeight * this.baseListBox.Items.Count;
            scrollBar.Minimum = 0;

            scrollBar.LargeChange = scrollBar.Maximum / scrollBar.Height + Height;
            scrollBar.SmallChange = 15;

            Controls.Add(scrollBar);
            Controls.Add(baseListBox);
        }

        private void AddEventHandler()
        {
            baseListBox.CausesValidationChanged += BaseListBoxCausesValidationChanged;
            baseListBox.ChangeUICues += BaseListBoxChangeUiCues;
            baseListBox.Click += BaseListBoxClick;
            baseListBox.ClientSizeChanged += BaseListBoxClientSizeChanged;
            baseListBox.ContextMenuChanged += BaseListBoxContextMenuChanged;
            baseListBox.ContextMenuStripChanged += BaseListBoxContextMenuStripChanged;
            baseListBox.CursorChanged += BaseListBoxCursorChanged;

            baseListBox.KeyDown += BaseListBoxKeyDown;
            baseListBox.KeyPress += BaseListBoxKeyPress;
            baseListBox.KeyUp += BaseListBoxKeyUp;

            baseListBox.SizeChanged += BaseListBoxSizeChanged;

            baseListBox.TextChanged += BaseListBoxTextChanged;
            baseListBox.SelectedIndexChanged += BaseListBoxSelectedIndexChanged;
        }

        private void BaseListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(sender, e);
        }



        #region Routing Methods

        public event EventHandler AcceptsTabChanged;
        private void BaseListBoxAcceptsTabChanged(object sender, EventArgs e)
        {
            if (AcceptsTabChanged != null)
                AcceptsTabChanged(this, e);
        }

        private void BaseListBoxSizeChanged(object sender, EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        private void BaseListBoxCursorChanged(object sender, EventArgs e)
        {
            base.OnCursorChanged(e);
        }

        private void BaseListBoxContextMenuStripChanged(object sender, EventArgs e)
        {
            base.OnContextMenuStripChanged(e);
        }

        private void BaseListBoxContextMenuChanged(object sender, EventArgs e)
        {
            base.OnContextMenuChanged(e);
        }

        private void BaseListBoxClientSizeChanged(object sender, EventArgs e)
        {
            base.OnClientSizeChanged(e);
        }

        private void BaseListBoxClick(object sender, EventArgs e)
        {
            base.OnClick(e);
        }

        private void BaseListBoxChangeUiCues(object sender, UICuesEventArgs e)
        {
            base.OnChangeUICues(e);
        }

        private void BaseListBoxCausesValidationChanged(object sender, EventArgs e)
        {
            base.OnCausesValidationChanged(e);
        }

        private void BaseListBoxKeyUp(object sender, KeyEventArgs e)
        {
            base.OnKeyUp(e);
        }

        private void BaseListBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
        }

        private void BaseListBoxKeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        private void BaseListBoxTextChanged(object sender, EventArgs e)
        {
            base.OnTextChanged(e);
        }

        #endregion

        public BorderStyle BorderStyle
        {
            get { return baseListBox.BorderStyle; }
            set { baseListBox.BorderStyle = value; }
        }

        public bool FormattingEnabled
        {
            get { return baseListBox.FormattingEnabled; }
            set { baseListBox.FormattingEnabled = value; }
        }

        public bool IntegralHeight
        {
            get { return baseListBox.IntegralHeight; }
            set { baseListBox.IntegralHeight = value; }
        }

        public int ItemHeight
        {
            get { return baseListBox.ItemHeight; }
            set { baseListBox.ItemHeight = value; }
        }

        private void scrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            baseListBox.TopIndex = Math.Max(scrollBar.Value / (ItemHeight + 1), 0);
            scrollBar.Refresh();
            Application.DoEvents();
        }

        public class ListBoxBase : ListBox
        {
            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams cp = base.CreateParams;
                    cp.Style = cp.Style & ~0x200000;
                    return cp;
                }
            }
        }
    }
}
