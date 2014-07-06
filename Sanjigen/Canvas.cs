using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.Multimedia3D.Model;
using Caltron.ObjectModels.TextureFont;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace Caltron
{
	public class Canvas
	{
		private IntPtr mvarHDC = IntPtr.Zero;
		private IntPtr mvarHGLRC = IntPtr.Zero;

		private int mvarGlutHandle = -1;
		private bool useGlut = false;

		private static void InitGL(System.Drawing.Imaging.PixelFormat pixelFormat, IntPtr hdc, out IntPtr hglrc)
		{
			Internal.System.Windows.Structures.PIXELFORMATDESCRIPTOR pfd = new Internal.System.Windows.Structures.PIXELFORMATDESCRIPTOR();

			hglrc = IntPtr.Zero;

			pfd.nSize = (ushort)System.Runtime.InteropServices.Marshal.SizeOf(pfd);
			pfd.nVersion = 1;
			pfd.dwFlags = Internal.System.Windows.Constants.PFD_DRAW_TO_BITMAP | Internal.System.Windows.Constants.PFD_SUPPORT_OPENGL | Internal.System.Windows.Constants.PFD_SUPPORT_GDI;
			pfd.iPixelType = (byte)Internal.System.Windows.Constants.PFD_TYPE_RGBA;
			switch (pixelFormat)
			{
				case System.Drawing.Imaging.PixelFormat.Format16bppArgb1555:
				case System.Drawing.Imaging.PixelFormat.Format16bppGrayScale:
				case System.Drawing.Imaging.PixelFormat.Format16bppRgb555:
				case System.Drawing.Imaging.PixelFormat.Format16bppRgb565:
					pfd.cColorBits = 16;
					break;
				case System.Drawing.Imaging.PixelFormat.Format1bppIndexed:
					pfd.cColorBits = 1;
					break;
				case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
					pfd.cColorBits = 24;
					break;
				case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
				case System.Drawing.Imaging.PixelFormat.Format32bppPArgb:
					pfd.cColorBits = 32;
					break;
			}
			pfd.cDepthBits = 32;
			pfd.iLayerType = Internal.System.Windows.Constants.PFD_MAIN_PLANE;
			int format = Internal.System.Windows.Methods.ChoosePixelFormat(hdc, ref pfd);
			int retval1 = Internal.System.Windows.Methods.SetPixelFormat(hdc, format, ref pfd);
			hglrc = Internal.OpenGL.Methods.glCreateContext(hdc);
		}

		public Canvas(int glutHandle)
		{
			mvarGlutHandle = glutHandle;
			useGlut = true;

			mvarMatrix = new RenderMatrix();
		}
		public Canvas(IntPtr handle, System.Drawing.Imaging.PixelFormat pixelFormat)
		{
			mvarHDC = handle;
			InitGL(pixelFormat, mvarHDC, out mvarHGLRC);

			mvarMatrix = new RenderMatrix(mvarHDC, mvarHGLRC);
		}

		public void MakeCurrent()
		{
			if (useGlut)
			{
				Internal.FreeGLUT.Methods.glutSetWindow(mvarGlutHandle);
			}
			else
			{
				Internal.OpenGL.Methods.glMakeCurrent(mvarHDC, mvarHGLRC);
			}
		}

		private RenderMatrix mvarMatrix = null;
		public RenderMatrix Matrix { get { return mvarMatrix; } }


		private bool mvarRenderBones = false;
		public bool RenderBones
		{
			get { return mvarRenderBones; }
			set { mvarRenderBones = value; }
		}
		private bool mvarRenderModels = true;
		public bool RenderModels
		{
			get { return mvarRenderModels; }
			set { mvarRenderModels = value; }
		}

		public bool EnableTexturing
		{
			get { return Internal.OpenGL.Methods.glIsEnabled(Internal.OpenGL.Constants.GLCapabilities.Texture2D); }
			set
			{
				if (value)
				{
					Internal.OpenGL.Methods.glEnable(Internal.OpenGL.Constants.GLCapabilities.Texture2D);
				}
				else
				{
					Internal.OpenGL.Methods.glDisable(Internal.OpenGL.Constants.GLCapabilities.Texture2D);
				}
			}
		}
		public bool EnableBlending
		{
			get { return Internal.OpenGL.Methods.glIsEnabled(Internal.OpenGL.Constants.GLCapabilities.Blending); }
			set
			{
				if (value)
				{
					Internal.OpenGL.Methods.glEnable(Internal.OpenGL.Constants.GLCapabilities.Blending);
				}
				else
				{
					Internal.OpenGL.Methods.glDisable(Internal.OpenGL.Constants.GLCapabilities.Blending);
				}
			}
		}

		public bool EnableTextureGenerationS
		{
			get { return Internal.OpenGL.Methods.glIsEnabled(Internal.OpenGL.Constants.GLCapabilities.TextureGenS); }
			set
			{
				if (value)
				{
					Internal.OpenGL.Methods.glEnable(Internal.OpenGL.Constants.GLCapabilities.TextureGenS);
				}
				else
				{
					Internal.OpenGL.Methods.glDisable(Internal.OpenGL.Constants.GLCapabilities.TextureGenS);
				}
			}
		}
		public bool EnableTextureGenerationT
		{
			get { return Internal.OpenGL.Methods.glIsEnabled(Internal.OpenGL.Constants.GLCapabilities.TextureGenT); }
			set
			{
				if (value)
				{
					Internal.OpenGL.Methods.glEnable(Internal.OpenGL.Constants.GLCapabilities.TextureGenT);
				}
				else
				{
					Internal.OpenGL.Methods.glDisable(Internal.OpenGL.Constants.GLCapabilities.TextureGenT);
				}
			}
		}
		public bool EnableTextureGenerationR
		{
			get { return Internal.OpenGL.Methods.glIsEnabled(Internal.OpenGL.Constants.GLCapabilities.TextureGenR); }
			set
			{
				if (value)
				{
					Internal.OpenGL.Methods.glEnable(Internal.OpenGL.Constants.GLCapabilities.TextureGenR);
				}
				else
				{
					Internal.OpenGL.Methods.glDisable(Internal.OpenGL.Constants.GLCapabilities.TextureGenR);
				}
			}
		}
		public bool EnableTextureGenerationQ
		{
			get { return Internal.OpenGL.Methods.glIsEnabled(Internal.OpenGL.Constants.GLCapabilities.TextureGenQ); }
			set
			{
				if (value)
				{
					Internal.OpenGL.Methods.glEnable(Internal.OpenGL.Constants.GLCapabilities.TextureGenQ);
				}
				else
				{
					Internal.OpenGL.Methods.glDisable(Internal.OpenGL.Constants.GLCapabilities.TextureGenQ);
				}
			}
		}

		/*
		private Texture mvarActiveTexture = null;
		/// <summary>
		/// Make texture that is associated with the ID the active texture. Any
		/// calls that have to do with OpenGL texture mapping will affect this
		/// texture. It is important that you remember this number since it
		/// will be needed again later on to actually apply the texture to
		/// geometry.
		/// </summary>
		public Texture ActiveTexture
		{
			get { return mvarActiveTexture; }
			set
			{
				if (!Texture.mvarIDsByTexture.ContainsKey(value))
				{
					throw new InvalidOperationException("No ID associated with the given texture");
				}

				mvarActiveTexture = value;
				Internal.OpenGL.Methods.glBindTexture(value.Target, Texture.mvarIDsByTexture[value]);
			}
		}
		*/

		/// <summary>
		/// Returns true if inside a Begin() operation, false if not.
		/// </summary>
		public bool InsideBlock
		{
			get { return mvarOpsBegun > 0; }
		}

		public void Reset()
		{
			Matrix.Reset();
			ResetColorBuffer();
			ResetDepthBuffer();
		}
		public void ResetColorBuffer()
		{
			// Clear Screen And Depth Buffer
			Internal.OpenGL.Methods.glClear(Internal.OpenGL.Constants.GL_COLOR_BUFFER_BIT);
		}
		public void ResetDepthBuffer()
		{
			Internal.OpenGL.Methods.glClear(Internal.OpenGL.Constants.GL_DEPTH_BUFFER_BIT);
		}

		public void Translate(double x, double y)
		{
			Translate(x, y, 0);
		}
		public void Translate(float x, float y)
		{
			Translate(x, y, 0);
		}

		public void Translate(double x, double y, double z)
		{
			if (InsideBlock) throw new InvalidOperationException();
			Internal.OpenGL.Methods.glTranslated(x, y, z);
		}
		public void Translate(float x, float y, float z)
		{
			if (InsideBlock) throw new InvalidOperationException();
			Internal.OpenGL.Methods.glTranslatef(x, y, z);
		}

		public void Rotate(double angle, RotationAxis axis)
		{
			switch (axis)
			{
				case RotationAxis.X:
					{
						Rotate(angle, 0.5, 0, 0);
						break;
					}
				case RotationAxis.Y:
					{
						Rotate(angle, 0, 0.5, 0);
						break;
					}
				case RotationAxis.Z:
					{
						Rotate(angle, 0, 0, 0.5);
						break;
					}
			}
		}
		public void Rotate(double angle, double x, double y, double z)
		{
			if (InsideBlock) throw new InvalidOperationException("Cannot rotate inside a glBegin/End block");
			Internal.OpenGL.Methods.glRotated(angle, x, y, z);
		}
		public void Rotate(float angle, float x, float y, float z)
		{
			if (InsideBlock) throw new InvalidOperationException();
			Internal.OpenGL.Methods.glRotatef(angle, x, y, z);
		}

		private int mvarOpsBegun = 0;

		public void Begin(RenderMode mode)
		{
			Internal.OpenGL.Methods.glBegin((int)mode);

			mvarOpsBegun++;
		}
		public void End()
		{
			Internal.OpenGL.Methods.glEnd();
			mvarOpsBegun--;
		}

		public void DrawVertex(ModelVertex vertex)
		{
			Internal.OpenGL.Methods.glTexCoord(vertex.Texture.U, vertex.Texture.V);
			Internal.OpenGL.Methods.glNormal3dv(new double[] { vertex.Normal.X, vertex.Normal.Y, vertex.Normal.Z });
			Internal.OpenGL.Methods.glVertex3d(vertex.Position.X, vertex.Position.Y, vertex.Position.Z);
		}
		public void DrawVertex(double x, double y)
		{
			Internal.OpenGL.Methods.glVertex2d(x, y);
		}
		public void DrawVertex(float x, float y)
		{
			Internal.OpenGL.Methods.glVertex2f(x, y);
		}
		public void DrawVertex(int x, int y)
		{
			Internal.OpenGL.Methods.glVertex2i(x, y);
		}
		public void DrawVertex(short x, short y)
		{
			Internal.OpenGL.Methods.glVertex2s(x, y);
		}
		public void DrawVertex(double x, double y, double z)
		{
			Internal.OpenGL.Methods.glVertex3d(x, y, z);
		}
		public void DrawVertex(float x, float y, float z)
		{
			Internal.OpenGL.Methods.glVertex3f(x, y, z);
		}
		public void DrawVertex(float[] vertices)
		{
			Internal.OpenGL.Methods.glVertex3fv(vertices);
		}
		public void DrawVertex(int x, int y, int z)
		{
			Internal.OpenGL.Methods.glVertex3i(x, y, z);
		}
		public void DrawVertex(short x, short y, short z)
		{
			Internal.OpenGL.Methods.glVertex3s(x, y, z);
		}
		public void DrawVertex(double x, double y, double z, double w)
		{
			Internal.OpenGL.Methods.glVertex4d(x, y, z, w);
		}
		public void DrawVertex(float x, float y, float z, float w)
		{
			Internal.OpenGL.Methods.glVertex4f(x, y, z, w);
		}
		public void DrawVertex(int x, int y, int z, int w)
		{
			Internal.OpenGL.Methods.glVertex4i(x, y, z, w);
		}
		public void DrawVertex(short x, short y, short z, short w)
		{
			Internal.OpenGL.Methods.glVertex4s(x, y, z, w);
		}

		public void DrawRectangle(double x, double y, double w, double h)
		{
			Begin(RenderMode.Lines);
			Internal.OpenGL.Methods.glVertex2d(x, y);
			Internal.OpenGL.Methods.glVertex2d(x + w, y);

			Internal.OpenGL.Methods.glVertex2d(x, y);
			Internal.OpenGL.Methods.glVertex2d(x, y + h);

			Internal.OpenGL.Methods.glVertex2d(x, y + h);
			Internal.OpenGL.Methods.glVertex2d(x + w, y + h);

			Internal.OpenGL.Methods.glVertex2d(x + w, y);
			Internal.OpenGL.Methods.glVertex2d(x + w, y + h);
			End();
		}
		public void DrawRectangle(float x, float y, float w, float h)
		{
			Begin(RenderMode.Lines);
			Internal.OpenGL.Methods.glVertex2f(x, y + h); // The bottom left corner
			Internal.OpenGL.Methods.glVertex2f(x, y); // The top left corner
			Internal.OpenGL.Methods.glVertex2f(x + w, y); // The top right corner
			Internal.OpenGL.Methods.glVertex2f(x + w, y + h); // The bottom right corner
			End();
		}

		private Dictionary<PictureObjectModel, Texture> _texturesByPicture = new Dictionary<PictureObjectModel, Texture>();
		public void DrawImage(double x, double y, double w, double h, PictureObjectModel picture)
		{
			if (picture == null) return;

			if (!_texturesByPicture.ContainsKey(picture))
			{
				Texture _texture = Texture.FromPicture(picture);
				_texturesByPicture.Add(picture, _texture);
			}
			DrawImage(x, y, w, h, _texturesByPicture[picture]);
		}
		private Dictionary<string, Texture> _texturesByFileName = new Dictionary<string, Texture>();
		public void DrawImage(double x, double y, double w, double h, string imageFileName)
		{
			if (!_texturesByFileName.ContainsKey(imageFileName))
			{
				Texture _texture = Texture.FromFile(imageFileName);
				_texturesByFileName.Add(imageFileName, _texture);
			}
			DrawImage(x, y, w, h, _texturesByFileName[imageFileName]);
		}
		public void DrawImage(double x, double y, double w, double h, Texture texture)
		{
			double r = x + w, b = y + h;

			Internal.OpenGL.Methods.glColor4f(1.0f, 1.0f, 1.0f, 1.0f);
			
			Texture = texture;
			Internal.OpenGL.Methods.glTexEnv(Internal.OpenGL.Constants.TextureEnvironmentTarget.TextureEnvironment, Internal.OpenGL.Constants.TextureEnvironmentParameterName.TextureEnvironmentMode, Internal.OpenGL.Constants.TextureEnvironmentParameterValue.Replace);

			EnableBlending = true;
			Internal.OpenGL.Methods.glBlendFunc(Internal.OpenGL.Constants.GLBlendFunc.SourceAlpha, Internal.OpenGL.Constants.GLBlendFunc.OneMinusSourceAlpha);

			EnableTexturing = true;

			Begin(RenderMode.Quads);
			Internal.OpenGL.Methods.glTexCoord(0, 1);
			Internal.OpenGL.Methods.glVertex2d(x, b); // The bottom left corner
			Internal.OpenGL.Methods.glTexCoord(0, 0);
			Internal.OpenGL.Methods.glVertex2d(x, y); // The top left corner
			Internal.OpenGL.Methods.glTexCoord(1, 0);
			Internal.OpenGL.Methods.glVertex2d(r, y); // The top right corner
			Internal.OpenGL.Methods.glTexCoord(1, 1);
			Internal.OpenGL.Methods.glVertex2d(r, b); // The bottom right corner
			End();

			
			EnableTexturing = false;
			EnableBlending = false;
		}

		public void FillRectangle(double x, double y, double w, double h)
		{
			Begin(RenderMode.Quads);
			Internal.OpenGL.Methods.glVertex2d(x, y + h); // The bottom left corner
			Internal.OpenGL.Methods.glVertex2d(x, y); // The top left corner
			Internal.OpenGL.Methods.glVertex2d(x + w, y); // The top right corner
			Internal.OpenGL.Methods.glVertex2d(x + w, y + h); // The bottom right corner
			End();
		}
		public void FillRectangle(float x, float y, float w, float h)
		{
			Begin(RenderMode.Quads);
			Internal.OpenGL.Methods.glVertex2f(x, y + h); // The bottom left corner
			Internal.OpenGL.Methods.glVertex2f(x, y); // The top left corner
			Internal.OpenGL.Methods.glVertex2f(x + w, y); // The top right corner
			Internal.OpenGL.Methods.glVertex2f(x + w, y + h); // The bottom right corner
			End();
		}

		public void FillRectangle(double x, double y, double w, double h, Color color)
		{
			Internal.OpenGL.Methods.glColor4d(color.Red, color.Green, color.Blue, color.Alpha);

			Begin(RenderMode.Quads);
			Internal.OpenGL.Methods.glVertex2d(x, y + h); // The bottom left corner
			Internal.OpenGL.Methods.glVertex2d(x, y); // The top left corner
			Internal.OpenGL.Methods.glVertex2d(x + w, y); // The top right corner
			Internal.OpenGL.Methods.glVertex2d(x + w, y + h); // The bottom right corner
			End();
		}
		public void FillRectangle(float x, float y, float w, float h, Color color)
		{
			Internal.OpenGL.Methods.glColor4d(color.Red, color.Green, color.Blue, color.Alpha);

			Begin(RenderMode.Quads);
			Internal.OpenGL.Methods.glVertex2f(x, y + h); // The bottom left corner
			Internal.OpenGL.Methods.glVertex2f(x, y); // The top left corner
			Internal.OpenGL.Methods.glVertex2f(x + w, y); // The top right corner
			Internal.OpenGL.Methods.glVertex2f(x + w, y + h); // The bottom right corner
			End();
		}

		public void DrawEquilateralTriangle(float size)
		{
			Begin(RenderMode.Triangles);

			DrawVertex(0.0f, size, 0.0f);     // Top
			DrawVertex(-size, -size, 0.0f);   // Bottom Left
			DrawVertex(size, -size, 0.0f);    // Bottom Right
			End();                            // Finished Drawing The Triangle
		}

		private RenderMode mvarRenderMode = RenderMode.Triangles;
		public RenderMode RenderMode { get { return mvarRenderMode; } set { mvarRenderMode = value; } }

		/// <summary>
		/// Generate a contiguous set of empty display lists.
		/// </summary>
		/// <param name="count">Specifies the number of contiguous empty display lists to be generated.</param>
		/// <returns></returns>
		public int[] CreateLists(int count)
		{
			if (count < 0) throw new ArgumentOutOfRangeException("count", "must be greater than or equal to 0");
			if (mvarOpsBegun > 0) throw new InvalidOperationException("cannot be executed between glBegin/glEnd");

			int start = Internal.OpenGL.Methods.glGenLists(count);
			if (start == 0) return new int[0];

			int[] listIndices = new int[count];
			for (int i = 0; i < count; i++)
			{
				listIndices[i] = start + i;
			}
			return listIndices;
		}

		public void BeginList(int listIndex, Internal.OpenGL.Constants.GLDisplayListMode mode)
		{
			Internal.OpenGL.Methods.glNewList(listIndex, mode);
		}
		public void DrawList(int listIndex)
		{
			Internal.OpenGL.Methods.glCallList(listIndex);
		}
		public void EndList()
		{
			Internal.OpenGL.Methods.glEndList();
		}

		private static Dictionary<ModelObjectModel, int> modelDisplayLists = new Dictionary<ModelObjectModel, int>();
		public void DrawModel(ModelObjectModel model)
		{
			if (model == null) return;

			string basedir = Environment.CurrentDirectory;
			if (model.Accessor is FileAccessor)
			{
				basedir = System.IO.Path.GetDirectoryName((model.Accessor as FileAccessor).FileName);
			}

			if (mvarRenderModels)
			{
				#region Load Materials
				if (!model.MaterialsLoaded && !model.MaterialsLoading)
				{
					model.MaterialsLoading = true;

					if (model.Accessor is FileAccessor)
					{
						Console.WriteLine("Loading materials for model: \"" + (model.Accessor as FileAccessor).FileName + "\"");
					}
					else
					{
						Console.WriteLine("Loading materials for model");
					}

					foreach (ModelMaterial mat1 in model.Materials)
					{
						Console.Write("Loading material " + model.Materials.IndexOf(mat1).ToString() + " / " + model.Materials.Count.ToString() + "... ");

						foreach (ModelTexture texture in mat1.Textures)
						{
							if (texture.TexturePicture != null)
							{
								// Image has already been preloaded
								if (texture.TextureID == null)
								{
									// Store texture ID for this texture
									Texture t1 = Texture.FromPicture(texture.TexturePicture);
									texture.TextureID = t1.ID;
								}
							}
							else
							{
								if (!String.IsNullOrEmpty(texture.TextureFileName))
								{
									if (texture.Flags != ModelTextureFlags.None)
									{
										if (texture.TextureID == null)
										{
											// Store texture ID for this texture
											string textureImageFullFileName = UniversalEditor.Common.Path.MakeAbsolutePath(texture.TextureFileName, basedir);
											if (!System.IO.File.Exists(textureImageFullFileName))
											{
												Console.WriteLine("texture image not found: " + textureImageFullFileName);
												continue;
											}

											Texture t1 = Texture.FromFile(textureImageFullFileName);
											texture.TextureID = t1.ID;
										}
									}
								}
							}

							if ((texture.Flags & (ModelTextureFlags.Map | ModelTextureFlags.AddMap)) != ModelTextureFlags.None)
							{
								if (texture.TexturePicture != null)
								{
									// Image has already been preloaded
									if (texture.MapID == null)
									{
										// Store texture ID for this texture
										Texture t1 = Texture.FromPicture(texture.TexturePicture);
										texture.MapID = t1.ID;
									}
								}
								else
								{
									if (!String.IsNullOrEmpty(texture.MapFileName))
									{
										if (texture.Flags != ModelTextureFlags.None)
										{
											if (texture.TextureID == null)
											{
												// Store texture ID for this texture
												string textureImageFullFileName = UniversalEditor.Common.Path.MakeAbsolutePath(texture.MapFileName, basedir);
												if (!System.IO.File.Exists(textureImageFullFileName))
												{
													Console.WriteLine("texture image not found: " + textureImageFullFileName);
													continue;
												}

												Texture t1 = Texture.FromFile(textureImageFullFileName);
												texture.MapID = t1.ID;
											}
										}
									}
								}
							}
						}

						Console.WriteLine("done!");
					}
					model.MaterialsLoading = false;
					model.MaterialsLoaded = true;
				}
				#endregion

				int vertexIndex = 0;
				if (model.Materials.Count > 0)
				{
					foreach (ModelMaterial mat in model.Materials)
					{
						// update the texture index
						if (mat.TextureIndex < (mat.Textures.Count - 1))
						{
							mat.TextureIndex++;
						}
						else
						{
							mat.TextureIndex = 0;
						}

						// 輪郭・影有無で色指定方法を変える
						// Contour - How to specify color change with or without shadow

						// 半透明でなければポリゴン裏面を無効にする
						// To disable the reverse side must be semi-transparent polygons
						/*
						if (mat.DiffuseColor.Alpha >= 255)
						{
							CullingMode = Caltron.CullingMode.Disabled;
						}
						else
						{
							CullingMode = Caltron.CullingMode.Front;
						}
						*/

						// テクスチャ・スフィアマップの処理
						// Processing of the texture map sphere
						ModelTextureFlags fTexFlag = ModelTextureFlags.None;
						if (mat.TextureIndex > -1 && mat.Textures.Count > 0)
						{
							fTexFlag = mat.Textures[mat.TextureIndex].Flags;
						}

						if (((fTexFlag & ModelTextureFlags.Texture) == ModelTextureFlags.Texture) && mat.Textures[mat.TextureIndex].TextureID != null)
						{
							// テクスチャありならBindする
							// Bind the texture to be there
							Texture = Texture.FromID(mat.Textures[mat.TextureIndex].TextureID.Value);

							EnableTexturing = true;
							EnableTextureGenerationS = false;
							EnableTextureGenerationT = false;
						}
						else if ((((fTexFlag & ModelTextureFlags.Map) == ModelTextureFlags.Map) || ((fTexFlag & ModelTextureFlags.AddMap) == ModelTextureFlags.AddMap)) && (mat.Textures[mat.TextureIndex].MapID != null))
						{
							// スフィアマップありならBindする
							// Bind sphere map, if it exists
							// Texture = Texture.FromID(mat.MapID.Value);
							Texture = Texture.FromID(mat.Textures[mat.TextureIndex].MapID.Value);

							EnableTexturing = false;
							EnableTextureGenerationS = true;
							EnableTextureGenerationT = true;
						}
						else
						{
							// テクスチャもスフィアマップもなし
							// A texture map sphere without any

							EnableTexturing = false;
							EnableTextureGenerationS = false;
							EnableTextureGenerationT = false;
						}

						if (!mat.AlwaysLight && (mat.EdgeFlag || model.IgnoreEdgeFlag))
						{
							// 輪郭・影有りのときは照明を有効にする
							// Contour - When the shadow is there to enable the lighting
							Internal.OpenGL.Methods.glMaterialfv(Internal.OpenGL.Constants.GLFace.Both, Internal.OpenGL.Constants.GL_DIFFUSE, new float[] { (float)mat.DiffuseColor.Red, (float)mat.DiffuseColor.Green, (float)mat.DiffuseColor.Blue, (float)mat.DiffuseColor.Alpha });
							Internal.OpenGL.Methods.glMaterialfv(Internal.OpenGL.Constants.GLFace.Both, Internal.OpenGL.Constants.GL_AMBIENT, new float[] { (float)mat.AmbientColor.Red, (float)mat.AmbientColor.Green, (float)mat.AmbientColor.Blue, (float)mat.AmbientColor.Alpha });
							Internal.OpenGL.Methods.glMaterialfv(Internal.OpenGL.Constants.GLFace.Both, Internal.OpenGL.Constants.GL_SPECULAR, new float[] { (float)mat.SpecularColor.Red, (float)mat.SpecularColor.Green, (float)mat.SpecularColor.Blue, (float)mat.SpecularColor.Alpha });
							Internal.OpenGL.Methods.glMaterialf(Internal.OpenGL.Constants.GLFace.Both, Internal.OpenGL.Constants.GL_SHININESS, (float)mat.Shininess);
							EnableLighting = true;
						}
						else
						{
							// 輪郭・影無しのときは照明を無効にする
							// Contour - When you disable the lighting without shadows
							Internal.OpenGL.Methods.glColor4f((float)((mat.AmbientColor.Red + mat.DiffuseColor.Red)), (float)((mat.AmbientColor.Green + mat.DiffuseColor.Green)), (float)((mat.AmbientColor.Blue + mat.DiffuseColor.Blue)), (float)((mat.AmbientColor.Alpha + mat.DiffuseColor.Alpha)));

							EnableLighting = false;
						}

						// 頂点インデックスを指定してポリゴン描画
						// Specifies the index vertex polygon drawing

						Begin(Caltron.RenderMode.Triangles);
						foreach (ModelTriangle tri in mat.Triangles)
						{
							DrawTriangle(tri);
						}
						End();
					}
				}
			}
			else
			{
				EnableLighting = false;
				EnableTexturing = false;

				Internal.OpenGL.Methods.glColor4f(1, 1, 1, 1);

				for (int i = 0; i < model.Surfaces[0].Vertices.Count; i += 3)
				{
					DrawTriangle(new ModelTriangle(model.Surfaces[0].Vertices[i], model.Surfaces[0].Vertices[i + 1], model.Surfaces[0].Vertices[i + 2]));
				}
			}
			if (mvarRenderBones)
			{
				Internal.OpenGL.Methods.glDisable(Internal.OpenGL.Constants.GLCapabilities.CullFace);
				// Internal.OpenGL.Methods.glFrontFace(Internal.OpenGL.Constants.GLFaceOrientation.Clockwise);
				// Internal.OpenGL.Methods.glCullFace(Internal.OpenGL.Constants.GL_BACK);

				EnableTexturing = false;

				foreach (ModelBone bone in model.Bones)
				{
					Matrix.Push();

					float[] ary = bone.Position.ToFloatArray();
					Matrix.Multiply(ary);

					Color = Color.FromRGBA(1.0f, 0.0f, 1.0f, 1.0f);
					//glutSolidCube( 0.3f );
					float fSize = 0.3f;
					Begin(Caltron.RenderMode.Quads);
					DrawVertex(-fSize / 2.0f, fSize / 2.0f, -fSize / 2.0f);
					DrawVertex(fSize / 2.0f, fSize / 2.0f, -fSize / 2.0f);
					DrawVertex(fSize / 2.0f, -fSize / 2.0f, -fSize / 2.0f);
					DrawVertex(-fSize / 2.0f, -fSize / 2.0f, -fSize / 2.0f);
					DrawVertex(fSize / 2.0f, fSize / 2.0f, -fSize / 2.0f);
					DrawVertex(fSize / 2.0f, fSize / 2.0f, fSize / 2.0f);
					DrawVertex(fSize / 2.0f, -fSize / 2.0f, fSize / 2.0f);
					DrawVertex(fSize / 2.0f, -fSize / 2.0f, -fSize / 2.0f);
					DrawVertex(fSize / 2.0f, fSize / 2.0f, fSize / 2.0f);
					DrawVertex(-fSize / 2.0f, fSize / 2.0f, fSize / 2.0f);
					DrawVertex(-fSize / 2.0f, -fSize / 2.0f, fSize / 2.0f);
					DrawVertex(fSize / 2.0f, -fSize / 2.0f, fSize / 2.0f);
					DrawVertex(fSize / 2.0f, fSize / 2.0f, -fSize / 2.0f);
					DrawVertex(fSize / 2.0f, fSize / 2.0f, fSize / 2.0f);
					DrawVertex(fSize / 2.0f, -fSize / 2.0f, fSize / 2.0f);
					DrawVertex(fSize / 2.0f, -fSize / 2.0f, -fSize / 2.0f);
					End();

					Matrix.Pop();
					Matrix.Push();

					Color = Color.FromRGBA(1.0f, 1.0f, 1.0f, 1.0f);

					if (bone.ParentBone != null)
					{
						Begin(Caltron.RenderMode.Lines);
						DrawVertex(bone.ParentBone.Position.ToFloatArray());
						DrawVertex(bone.Position.ToFloatArray());
						End();
					}

					Matrix.Pop();
				}
			}
		}

		public void DrawTriangle(ModelTriangle triangle)
		{
			DrawVertex(triangle.Vertex1);
			DrawVertex(triangle.Vertex2);
			DrawVertex(triangle.Vertex3);
		}

		private bool mvarEnableLighting = false;
		public bool EnableLighting
		{
			get { return mvarEnableLighting; }
			set
			{
				bool changed = true; // (mvarEnableLighting != value);
				mvarEnableLighting = value;
				if (changed)
				{
					if (mvarEnableLighting)
					{
						Internal.OpenGL.Methods.glEnable(Internal.OpenGL.Constants.GLCapabilities.Lighting);
					}
					else
					{
						Internal.OpenGL.Methods.glDisable(Internal.OpenGL.Constants.GLCapabilities.Lighting);
					}
				}
			}
		}

		private TextureTarget mvarLastTextureTarget = TextureTarget.Texture2D;
		private Texture mvarTexture = null;
		public Texture Texture
		{
			get { return mvarTexture; }
			set
			{
				mvarTexture = value;
				if (mvarTexture == null)
				{
					// Internal.OpenGL.Methods.glBindTexture(mvarLastTextureTarget, UInt32.MaxValue);
				}
				else
				{
					Internal.OpenGL.Methods.glBindTexture(mvarTexture.Target, mvarTexture.ID);
					mvarLastTextureTarget = mvarTexture.Target;
				}
			}
		}

		private Color mvarColor = Colors.White;
		public Color Color
		{
			get { return mvarColor; }
			set
			{
				mvarColor = value;
				Internal.OpenGL.Methods.glColor4d(mvarColor.Red, mvarColor.Green, mvarColor.Blue, mvarColor.Alpha);
			}
		}

		public void DrawWireCone(double baseWidth, double height, int numVerticalSlices, int numHorizontalStacks)
		{
			Internal.FreeGLUT.Methods.glutWireCone(baseWidth, height, numVerticalSlices, numHorizontalStacks);
		}

		private CullingMode mvarCullingMode = CullingMode.Disabled;
		public CullingMode CullingMode
		{
			get { return mvarCullingMode; }
			set
			{
				if (mvarCullingMode == value) return;

				mvarCullingMode = value;
				switch (mvarCullingMode)
				{
					case CullingMode.Front:
					{
						Internal.OpenGL.Methods.glEnable(Internal.OpenGL.Constants.GLCapabilities.CullFace);
						Internal.OpenGL.Methods.glCullFace(Internal.OpenGL.Constants.GLFace.Front);
						break;
					}
					case CullingMode.Back:
					{
						Internal.OpenGL.Methods.glEnable(Internal.OpenGL.Constants.GLCapabilities.CullFace);
						Internal.OpenGL.Methods.glCullFace(Internal.OpenGL.Constants.GLFace.Back);
						break;
					}
					case CullingMode.Both:
					{
						Internal.OpenGL.Methods.glEnable(Internal.OpenGL.Constants.GLCapabilities.CullFace);
						Internal.OpenGL.Methods.glCullFace(Internal.OpenGL.Constants.GLFace.Both);
						break;
					}
					case CullingMode.Disabled:
					{
						Internal.OpenGL.Methods.glDisable(Internal.OpenGL.Constants.GLCapabilities.CullFace);
						break;
					}
				}
			}
		}

		private bool mvarEnableDepthTesting = false;
		public bool EnableDepthTesting
		{
			get { return mvarEnableDepthTesting; }
			set
			{
				mvarEnableDepthTesting = value;
				if (mvarEnableDepthTesting)
				{
					Internal.OpenGL.Methods.glEnable(Internal.OpenGL.Constants.GLCapabilities.DepthTesting);
				}
				else
				{
					Internal.OpenGL.Methods.glDisable(Internal.OpenGL.Constants.GLCapabilities.DepthTesting);
				}
			}
		}
		private bool mvarEnableNormalization = false;
		public bool EnableNormalization
		{
			get { return mvarEnableNormalization; }
			set
			{
				mvarEnableNormalization = value;
				if (mvarEnableNormalization)
				{
					Internal.OpenGL.Methods.glEnable(Internal.OpenGL.Constants.GLCapabilities.Normalization);
				}
				else
				{
					Internal.OpenGL.Methods.glDisable(Internal.OpenGL.Constants.GLCapabilities.Normalization);
				}
			}
		}

		public void Scale(float all)
		{
			Scale(all, all, all);
		}
		public void Scale(double all)
		{
			Scale(all, all, all);
		}

		public void Scale(float x, float y, float z)
		{
			Internal.OpenGL.Methods.glScalef(x, y, z);
		}
		public void Scale(double x, double y, double z)
		{
			Internal.OpenGL.Methods.glScaled(x, y, z);
		}

		public void DrawText(string text, double x, double y, double size = 0.1, float weight = 1.0f, Font font = null)
		{
			if (font == null) font = Fonts.StrokeRoman;
			
			Internal.OpenGL.Methods.glPushMatrix();

			Translate(x, y);
			Scale(size);
			Scale(0.8, -1, 1);

			Internal.OpenGL.Methods.glLineWidth(weight);
			// Internal.OpenGL.Methods.glRasterPos2d(x, y);
			if (font is SystemFont)
			{
				SystemFont sysfont = (font as SystemFont);
				switch (sysfont.FontType)
				{
					case SystemFontType.Stroke:
					{
						Internal.FreeGLUT.Methods.glutStrokeString(sysfont.Handle, text);
						break;
					}
					case SystemFontType.Bitmap:
					{
						Internal.FreeGLUT.Methods.glutBitmapString(sysfont.Handle, text);
						break;
					}
				}
			}
			else if (font is BitmapFont)
			{
				BitmapFont bmpfont = (font as BitmapFont);
				string filename = bmpfont.Font.TextureFileName;
				filename = filename.Replace ("~/", System.IO.Path.GetDirectoryName (System.Reflection.Assembly.GetEntryAssembly ().Location) + "/");
				
				if (!System.IO.File.Exists(filename))
				{
					Console.WriteLine ("bitmap font file not found: \"" + bmpfont.Font.TextureFileName + "\"");
					return;
				}
				
				Texture texture = Texture.FromFile(filename);
				Texture oldTexture = this.Texture;
				this.Texture = texture;
				
				for (int i = 0; i < text.Length; i++)
				{
					char chr = text[i];
					TextureFontCharacter chara = bmpfont.Font.Characters[chr];
					if (chara == null) continue;
						
					int index = chara.Index;
					// TODO: get 
				}
				
				this.Texture = oldTexture;
			}

			Internal.OpenGL.Methods.glPopMatrix();
		}

		public void DrawSunkenBorder(double x, double y, double w, double h, int size = 1)
		{
			for (int i = 0; i < size; i++)
			{
				Color = Colors.DarkGray;
				DrawLine(x + i, y + i, x + w - i, y + i);
				DrawLine(x + i, y + i, x + i, y + h - i);
				Color = Colors.LightGray;
				DrawLine(x + w - i, y + i, x + w - i, y + h - i);
				DrawLine(x + i, y + h - i, x + w - i, y + h - i);
			}
		}
		public void DrawRaisedBorder(double x, double y, double w, double h, int size = 1)
		{
			for (int i = 0; i < size; i++)
			{
				Color = Colors.LightGray;
				DrawLine(x + i, y + i, x + w - i, y + i);
				DrawLine(x + i, y + i, x + i, y + h - i);
				Color = Colors.DarkGray;
				DrawLine(x + w - i, y + i, x + w - i, y + h - i);
				DrawLine(x + i, y + h - i, x + w - i, y + h - i);
			}
		}

		public void DrawLine(double x1, double y1, double x2, double y2)
		{
			Begin(Caltron.RenderMode.Lines);
			Internal.OpenGL.Methods.glVertex2d(x1, y1);
			Internal.OpenGL.Methods.glVertex2d(x2, y2);
			End();
		}

		public Dimension2D MeasureText(string text)
		{
			double width = Internal.FreeGLUT.Methods.glutStrokeLength(Internal.FreeGLUT.Constants.GLUT_STROKE_ROMAN, text);
			double height = Internal.FreeGLUT.Methods.glutStrokeHeight(Internal.FreeGLUT.Constants.GLUT_STROKE_ROMAN);
			width *= 0.1;
			height *= 0.1;
			return new Dimension2D(width, height);
		}

		private const double DEG2RAD = (double)3.14159 / 180;
		public void DrawCircle(double centerPointX, double centerPointY, double radius)
		{
			Begin(Caltron.RenderMode.LineLoop);
			for (int i = 0; i < 360; i++)
			{
				double degInRad = i * DEG2RAD;
				Internal.OpenGL.Methods.glVertex2d(centerPointX + (Math.Cos(degInRad) * radius), centerPointY + (Math.Sin(degInRad) * radius));
			}
			End();
		}
		public void DrawCircle(double x, double y, double w, double h)
		{
			double radius = w / 2;
			double centerPointX = x + radius;
			double centerPointY = y + radius;
			DrawCircle(centerPointX, centerPointY, radius);
		}
		public void FillCircle(double centerPointX, double centerPointY, double radius)
		{
			Begin(Caltron.RenderMode.Polygon);
			for (int i=0; i < 360; i++)
			{
				double degInRad = i*DEG2RAD;
				Internal.OpenGL.Methods.glVertex2d(centerPointX + (Math.Cos(degInRad) * radius), centerPointY + (Math.Sin(degInRad) * radius));
			}
			End();
		}
		public void FillCircle(double x, double y, double w, double h)
		{
			double radius = w / 2;
			double centerPointX = x + radius;
			double centerPointY = y + radius;
			FillCircle(centerPointX, centerPointY, radius);
		}

		public void Clear(UniversalEditor.Color color)
		{
			Internal.OpenGL.Methods.glClearColor(color.Red, color.Green, color.Blue, color.Alpha);
			Internal.OpenGL.Methods.glClear(Internal.OpenGL.Constants.GL_COLOR_BUFFER_BIT);
		}
	}
}
