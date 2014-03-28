using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Caltron
{
    public partial class WindowsForm : Form
    {
        private Canvas mvarCanvas = null;

        public WindowsForm()
        {
            InitializeComponent();
        }

        private IntPtr mvarHDC = IntPtr.Zero;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            mvarHDC = Internal.System.Windows.Methods.GetDC(this.Handle);
            mvarCanvas = new Canvas(mvarHDC, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= Internal.System.Windows.Constants.WS_CLIPSIBLINGS | Internal.System.Windows.Constants.WS_CLIPCHILDREN;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            mvarCanvas.Color = Colors.White;

            Internal.OpenGL.Methods.glSwapBuffers(mvarHDC);
        }

    }
}
