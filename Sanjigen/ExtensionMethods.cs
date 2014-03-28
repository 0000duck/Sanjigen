using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor;

namespace Caltron
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Converts this <see cref="UniversalEditor.ObjectModels.Multimedia3D.Model.ModelObjectModel" /> to a <see cref="System.Drawing.Bitmap" />, rendering it in its default pose.
        /// </summary>
        /// <param name="model">The <see cref="UniversalEditor.ObjectModels.Multimedia3D.Model.ModelObjectModel" /> to convert into a <see cref="System.Drawing.Bitmap" />.</param>
        /// <returns>A <see cref="System.Drawing.Bitmap" /> containing an OpenGL-rendered image of the given <see cref="UniversalEditor.ObjectModels.Multimedia3D.Model.ModelObjectModel" /> in its default pose.</returns>
        public static System.Drawing.Bitmap ToBitmap(this UniversalEditor.ObjectModels.Multimedia3D.Model.ModelObjectModel model)
        {
            return ToBitmap(model, 640, 480);
        }
        /// <summary>
        /// Converts this <see cref="UniversalEditor.ObjectModels.Multimedia3D.Model.ModelObjectModel" /> to a <see cref="System.Drawing.Bitmap" />, rendering it in its default pose.
        /// </summary>
        /// <param name="model">The <see cref="UniversalEditor.ObjectModels.Multimedia3D.Model.ModelObjectModel" /> to convert into a <see cref="System.Drawing.Bitmap" />.</param>
        /// <returns>A <see cref="System.Drawing.Bitmap" /> containing an OpenGL-rendered image of the given <see cref="UniversalEditor.ObjectModels.Multimedia3D.Model.ModelObjectModel" /> in its default pose.</returns>
        public static System.Drawing.Bitmap ToBitmap(this UniversalEditor.ObjectModels.Multimedia3D.Model.ModelObjectModel model, int width, int height)
        {
            return ToBitmap(model, width, height, null);
        }
        /// <summary>
        /// Converts this <see cref="UniversalEditor.ObjectModels.Multimedia3D.Model.ModelObjectModel" /> to a <see cref="System.Drawing.Bitmap" />, rendering it in the given pose.
        /// </summary>
        /// <param name="model">The <see cref="UniversalEditor.ObjectModels.Multimedia3D.Model.ModelObjectModel" /> to convert into a <see cref="System.Drawing.Bitmap" />.</param>
        /// <param name="pose">The <see cref="UniversalEditor.ObjectModels.Multimedia3D.Pose.PoseObjectModel" /> in which to pose the model.</param>
        /// <returns>A <see cref="System.Drawing.Bitmap" /> containing an OpenGL-rendered image of the given <see cref="UniversalEditor.ObjectModels.Multimedia3D.Model.ModelObjectModel" /> in the given pose.</returns>
        public static System.Drawing.Bitmap ToBitmap(this UniversalEditor.ObjectModels.Multimedia3D.Model.ModelObjectModel model, UniversalEditor.ObjectModels.Multimedia3D.Pose.PoseObjectModel pose)
        {
            return ToBitmap(model, 640, 480, pose);
        }
        public static System.Drawing.Bitmap ToBitmap(this UniversalEditor.ObjectModels.Multimedia3D.Model.ModelObjectModel model, int width, int height, UniversalEditor.ObjectModels.Multimedia3D.Pose.PoseObjectModel pose)
        {
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(width, height);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
            graphics.Clear(System.Drawing.Color.Black);

            IntPtr hdc = graphics.GetHdc();

            Canvas canvas = new Canvas(hdc, bitmap.PixelFormat);
            
            canvas.MakeCurrent();
            canvas.Matrix.Mode = MatrixMode.Projection;
            canvas.Matrix.Reset();

            Internal.GLU.Methods.gluPerspective(45.0, 640 / 480, 0.0, 45.0);

            canvas.EnableDepthTesting = true;
            canvas.EnableNormalization = true;
            canvas.EnableTexturing = true;

            Internal.OpenGL.Methods.glTexEnv(Internal.OpenGL.Constants.TextureEnvironmentTarget.TextureEnvironment, Internal.OpenGL.Constants.TextureEnvironmentParameterName.TextureEnvironmentMode, Internal.OpenGL.Constants.TextureEnvironmentParameterValue.Modulate);

            canvas.CullingMode = CullingMode.Front;
            
            canvas.EnableBlending = true;
            Internal.OpenGL.Methods.glBlendFunc(Internal.OpenGL.Constants.GLBlendFunc.SourceAlpha, Internal.OpenGL.Constants.GLBlendFunc.OneMinusSourceAlpha);

            Internal.OpenGL.Methods.glEnable(Internal.OpenGL.Constants.GLCapabilities.AlphaTesting);
            Internal.OpenGL.Methods.glAlphaFunc(Internal.OpenGL.Constants.GLAlphaFunc.GreaterOrEqual, 0.05f);

            World.Lights[0].Position = new PositionVector4(0.45f, 1.45f, -4.0f, 0.0f);
            World.Lights[0].DiffuseColor = Color.FromRGBA(0.9f, 0.9f, 0.9f, 1.0f);
            World.Lights[0].AmbientColor = Color.FromRGBA(0.7f, 0.7f, 0.7f, 1.0f);
            World.Lights[0].SpecularColor = Color.FromRGBA(0.7f, 0.7f, 0.7f, 1.0f);
            World.Lights[0].Enabled = true;

            canvas.Matrix.Mode = MatrixMode.Projection;
            canvas.Reset();
            canvas.Translate(0.0, -1, 0.5);
            canvas.Scale(0.05, 0.08, 0.1);


            canvas.Matrix.Mode = MatrixMode.ModelView;
            canvas.DrawModel(model);

            graphics.ReleaseHdc();
            graphics.Flush();

            bitmap.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipX);

            if (pose != null)
            {
            }

            return bitmap;
        }


    }
}
