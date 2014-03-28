using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor;
using UniversalEditor.Accessors;

namespace Caltron.ModelViewer
{
    public partial class MainWindow : Form
    {
        // TODO: fix issue where in model viewer, each subsequent render after the first will not render textures properly
        // TODO: fix issue in renderer with map textures

        // There could be an issue with getting the image from the extension method ModelObjectModel.ToBitmap backwards,
        // because the entire image is inverted. However, that doesn't explain the sphere mapping issue... Fix both!

        public MainWindow()
        {
            InitializeComponent();
        }

        UniversalEditor.ObjectModels.Multimedia3D.Model.ModelObjectModel modelObj = null;

        private void FileOpen_Click(object sender, EventArgs e)
        {
            modelObj = new UniversalEditor.ObjectModels.Multimedia3D.Model.ModelObjectModel();

            OpenFileDialog ofd = new OpenFileDialog();

            modelObj = new UniversalEditor.ObjectModels.Multimedia3D.Model.ModelObjectModel();
            ofd.Filter = UniversalEditor.Common.Dialog.GetCommonDialogFilter(modelObj.MakeReference());
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(pic.Width, pic.Height);
                Graphics.FromImage(bmp).Clear(System.Drawing.Color.Black);

                pic.Image = bmp;
                System.Windows.Forms.Application.DoEvents();

                UniversalEditor.Common.Reflection.GetAvailableObjectModel<UniversalEditor.ObjectModels.Multimedia3D.Model.ModelObjectModel>(ofd.FileName, ref modelObj);

                bmp = modelObj.ToBitmap(pic.Width, pic.Height);
                pic.Image = bmp;

                Text = System.IO.Path.GetFileName(ofd.FileName) + " - Caltron Model Viewer";
            }
        }

        private void FileSaveAs_Click(object sender, EventArgs e)
        {
            if (modelObj == null)
            {
                MessageBox.Show("Please load a model before saving.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Caltron.Texture.Clear();

            SaveFileDialog sfd = new SaveFileDialog();

            List<UniversalEditor.DataFormatReference> dfrs = new List<UniversalEditor.DataFormatReference>();
            sfd.Filter = UniversalEditor.Common.Dialog.GetCommonDialogFilter(modelObj.MakeReference(), out dfrs); // , false);
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // TODO: Modify the API to allow getting
                //       a common dialog filter without the
                //       "all files" entry

                if (sfd.FilterIndex < 2) return;

                UniversalEditor.DataFormatReference dfr = dfrs[sfd.FilterIndex - 2];
                Document.Load(modelObj, dfr.Create(), new FileAccessor(sfd.FileName), true);
                
                Text = System.IO.Path.GetFileName(sfd.FileName) + " - Caltron Model Viewer";
            }
        }

        private void MainWindow_ResizeEnd(object sender, EventArgs e)
        {
            if (modelObj != null)
            {
                System.Drawing.Bitmap bmp = modelObj.ToBitmap(pic.Width, pic.Height);
                pic.Image = bmp;
            }
        }

        private FormWindowState _WindowState = FormWindowState.Normal;

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if (_WindowState != WindowState && (WindowState == FormWindowState.Maximized || WindowState == FormWindowState.Normal))
            {
                if (modelObj != null)
                {
                    System.Drawing.Bitmap bmp = modelObj.ToBitmap(pic.Width, pic.Height);
                    pic.Image = bmp;
                }
            }
            _WindowState = WindowState;
        }

        private void mnuToolsTextureFlipping_Click(object sender, EventArgs e)
        {

        }

        private void FileNew_Click(object sender, EventArgs e)
        {

        }

        private void FileExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
