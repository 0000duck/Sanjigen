using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace Caltron.ImageViewer
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        PictureObjectModel picObj = null;

        private void FileOpen_Click(object sender, EventArgs e)
        {
            picObj = new UniversalEditor.ObjectModels.Multimedia.Picture.PictureObjectModel();

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = UniversalEditor.Common.Dialog.GetCommonDialogFilter(picObj.MakeReference());
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                UniversalEditor.Common.Reflection.GetAvailableObjectModel<UniversalEditor.ObjectModels.Multimedia.Picture.PictureObjectModel>(ofd.FileName, ref picObj);
                
                Bitmap bmp = picObj.ToBitmap();
                pic.Image = bmp;

                Text = System.IO.Path.GetFileName(ofd.FileName) + " - Caltron Image Viewer";
            }
        }

        private void FileSaveAs_Click(object sender, EventArgs e)
        {
            if (picObj == null)
            {
                MessageBox.Show("Please load an image before saving.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();

            List<UniversalEditor.DataFormatReference> dfrs = new List<UniversalEditor.DataFormatReference>();
            sfd.Filter = UniversalEditor.Common.Dialog.GetCommonDialogFilter(picObj.MakeReference(), out dfrs); // , false);
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // TODO: Modify the API to allow getting
                //       a common dialog filter without the
                //       "all files" entry

                if (sfd.FilterIndex < 2) return;

                UniversalEditor.DataFormatReference dfr = dfrs[sfd.FilterIndex - 2];
                UniversalEditor.DataFormat df = dfr.Create();

                Document.Save(picObj, df, new FileAccessor(sfd.FileName, true, true), true);
                
                Text = System.IO.Path.GetFileName(sfd.FileName) + " - Caltron Image Viewer";
            }
        }

		private void FileExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void FileSave_Click(object sender, EventArgs e)
		{

		}
    }
}
