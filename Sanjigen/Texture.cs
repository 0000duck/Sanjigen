﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace Caltron
{
	public class Texture
	{
		private static Dictionary<uint, Texture> texturesByID = new Dictionary<uint, Texture>();
		private static Dictionary<string, Texture> texturesByFileName = new Dictionary<string, Texture>();
		private static Dictionary<PictureObjectModel, Texture> texturesByPicture = new Dictionary<PictureObjectModel, Texture>();

		public static Texture FromID(uint id)
		{
			if (texturesByID.ContainsKey(id))
			{
				return texturesByID[id];
			}
			return null;
		}

		public static Texture FromFile(string FileName)
		{
			return FromFile(FileName, TextureRotation.None, TextureFlip.None);
		}
		public static Texture FromFile(string FileName, TextureRotation rotation, TextureFlip flip)
		{
			if (texturesByFileName.ContainsKey(FileName))
			{
				Texture tex = texturesByFileName[FileName];
				tex.Rotation = rotation;
				tex.Flip = flip;
				return tex;
			}

			uint[] textureIDs = new uint[1];
			Internal.OpenGL.Methods.glGenTextures(1, textureIDs);
			uint textureID = textureIDs[0];

			Texture texture = new Texture(textureID);
			texture.Target = TextureTarget.Texture2D;

			texture.FileName = FileName;

			texture.MinFilter = TextureFilter.Linear;
			texture.MagFilter = TextureFilter.Linear;
			// texture.TextureWrapR = TextureWrap.Repeat;
			texture.TextureWrapS = TextureWrap.Repeat;
			texture.TextureWrapT = TextureWrap.Repeat;

			texture.Rotation = rotation;
			texture.Flip = flip;

			if (!texturesByID.ContainsKey(textureID))
			{
				texturesByID.Add(textureID, texture);
			}
			if (!texturesByFileName.ContainsKey(FileName))
			{
				texturesByFileName.Add(FileName, texture);
			}
			return texture;
		}

		private TextureTarget mvarTarget = TextureTarget.Texture2D;
		public TextureTarget Target { get { return mvarTarget; } set { mvarTarget = value; } }

		private TextureRotation mvarRotation = TextureRotation.None;
		public TextureRotation Rotation
		{
			get { return mvarRotation; }
			set { mvarRotation = value; }
		}
		private TextureFlip mvarFlip = TextureFlip.None;
		public TextureFlip Flip
		{
			get { return mvarFlip; }
			set { mvarFlip = value; }
		}

		private TextureFilter mvarMinFilter = TextureFilter.Linear;
		public TextureFilter MinFilter
		{
			get { return mvarMinFilter; }
			set
			{
				mvarMinFilter = value;

				Internal.OpenGL.Methods.glBindTexture(mvarTarget, mvarID);
				Internal.OpenGL.Methods.glErrorToException();

				Internal.OpenGL.Methods.glTexParameter(Internal.OpenGL.Constants.TextureParameterTarget.Texture2D, Internal.OpenGL.Constants.TextureParameterName.MinimumFilter, (int)mvarMinFilter);
				Internal.OpenGL.Methods.glErrorToException();
			}
		}
		private TextureFilter mvarMagFilter = TextureFilter.Linear;
		public TextureFilter MagFilter
		{
			get { return mvarMagFilter; }
			set
			{
				mvarMagFilter = value;

				Internal.OpenGL.Methods.glBindTexture(mvarTarget, mvarID);
				Internal.OpenGL.Methods.glErrorToException();

				Internal.OpenGL.Methods.glTexParameter(Internal.OpenGL.Constants.TextureParameterTarget.Texture2D, Internal.OpenGL.Constants.TextureParameterName.MaximumFilter, (int)mvarMagFilter);
				Internal.OpenGL.Methods.glErrorToException();
			}
		}

		private TextureWrap mvarTextureWrapS = TextureWrap.Repeat;
		public TextureWrap TextureWrapS
		{
			get { return mvarTextureWrapS; }
			set
			{
				mvarTextureWrapS = value;

				Internal.OpenGL.Methods.glTexParameter(Internal.OpenGL.Constants.TextureParameterTarget.Texture2D, Internal.OpenGL.Constants.TextureParameterName.TextureWrapS, (int)mvarTextureWrapS);
				Internal.OpenGL.Methods.glErrorToException();
			}
		}
		private TextureWrap mvarTextureWrapT = TextureWrap.Repeat;
		public TextureWrap TextureWrapT
		{
			get { return mvarTextureWrapT; }
			set
			{
				mvarTextureWrapT = value;

				Internal.OpenGL.Methods.glTexParameter(Internal.OpenGL.Constants.TextureParameterTarget.Texture2D, Internal.OpenGL.Constants.TextureParameterName.TextureWrapT, (int)mvarTextureWrapT);
				Internal.OpenGL.Methods.glErrorToException();
			}
		}
		private TextureWrap mvarTextureWrapR = TextureWrap.Repeat;
		public TextureWrap TextureWrapR
		{
			get { return mvarTextureWrapR; }
			set
			{
				mvarTextureWrapR = value;

				Internal.OpenGL.Methods.glTexParameter(Internal.OpenGL.Constants.TextureParameterTarget.Texture2D, Internal.OpenGL.Constants.TextureParameterName.TextureWrapR, (int)mvarTextureWrapR);
				Internal.OpenGL.Methods.glErrorToException();
			}
		}

		private Texture(uint id)
		{
			mvarID = id;
		}

		private uint mvarID = 0;
		public uint ID { get { return mvarID; } }

		private string mvarFileName = String.Empty;
		public string FileName
		{
			get { return mvarFileName; }
			set
			{
				mvarFileName = value;

				if (!System.IO.File.Exists(mvarFileName)) return;

				PictureObjectModel pic = UniversalEditor.Common.Reflection.GetAvailableObjectModel<PictureObjectModel>(new UniversalEditor.Accessors.FileAccessor(mvarFileName));
				if (pic == null) return;

				TextureImage = pic;
			}
		}


		private PictureObjectModel mvarTextureImage = null;
		public PictureObjectModel TextureImage
		{
			get { return mvarTextureImage; }
			set
			{
				mvarTextureImage = value;
				
				System.Drawing.Bitmap bmp = mvarTextureImage.ToBitmap();
				#region Rotate None
				if (mvarRotation == TextureRotation.None && mvarFlip == TextureFlip.None)
				{
					// bmp.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipNone);
				}
				else if (mvarRotation == TextureRotation.None && mvarFlip == TextureFlip.FlipX)
				{
					bmp.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipX);
				}
				else if (mvarRotation == TextureRotation.None && mvarFlip == TextureFlip.FlipY)
				{
					bmp.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipY);
				}
				else if (mvarRotation == TextureRotation.None && mvarFlip == TextureFlip.FlipBoth)
				{
					bmp.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipXY);
				}
				#endregion
				#region Rotate 90
				else if (mvarRotation == TextureRotation.Rotate90 && mvarFlip == TextureFlip.None)
				{
					bmp.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
				}
				else if (mvarRotation == TextureRotation.Rotate90 && mvarFlip == TextureFlip.FlipX)
				{
					bmp.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipX);
				}
				else if (mvarRotation == TextureRotation.Rotate90 && mvarFlip == TextureFlip.FlipY)
				{
					bmp.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipY);
				}
				else if (mvarRotation == TextureRotation.Rotate90 && mvarFlip == TextureFlip.FlipBoth)
				{
					bmp.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipXY);
				}
				#endregion
				#region Rotate 180
				else if (mvarRotation == TextureRotation.Rotate180 && mvarFlip == TextureFlip.None)
				{
					bmp.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
				}
				else if (mvarRotation == TextureRotation.Rotate180 && mvarFlip == TextureFlip.FlipX)
				{
					bmp.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipX);
				}
				else if (mvarRotation == TextureRotation.Rotate180 && mvarFlip == TextureFlip.FlipY)
				{
					bmp.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipY);
				}
				else if (mvarRotation == TextureRotation.Rotate180 && mvarFlip == TextureFlip.FlipBoth)
				{
					bmp.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipXY);
				}
				#endregion
				#region Rotate 270
				else if (mvarRotation == TextureRotation.Rotate270 && mvarFlip == TextureFlip.None)
				{
					bmp.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);
				}
				else if (mvarRotation == TextureRotation.Rotate270 && mvarFlip == TextureFlip.FlipX)
				{
					bmp.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipX);
				}
				else if (mvarRotation == TextureRotation.Rotate270 && mvarFlip == TextureFlip.FlipY)
				{
					bmp.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipY);
				}
				else if (mvarRotation == TextureRotation.Rotate270 && mvarFlip == TextureFlip.FlipBoth)
				{
					bmp.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipXY);
				}
				#endregion

				System.Drawing.Imaging.BitmapData bmpBits = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);

				Internal.OpenGL.Methods.glBindTexture(mvarTarget, mvarID);
				Internal.OpenGL.Methods.glErrorToException();
				
				Internal.OpenGL.Constants.GLTextureInternalFormat internalFormat = Internal.OpenGL.Constants.GLTextureInternalFormat.RGBA8;
				Internal.OpenGL.Constants.GLTextureFormat format = Internal.OpenGL.Constants.GLTextureFormat.BGRA;
				/*
				switch (bmp.PixelFormat)
				{
					case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
					{
						format = Internal.OpenGL.Constants.GLTextureFormat.ColorIndex;
						internalFormat = Internal.OpenGL.Constants.GLTextureInternalFormat.RGB;
						break;
					}
					case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
					{
						format = Internal.OpenGL.Constants.GLTextureFormat.RGB;
						break;
					}
				}
				 */

				Internal.OpenGL.Methods.glTexImage2D(mvarTarget, 0, internalFormat, bmp.Width, bmp.Height, 0, format, Internal.OpenGL.Constants.GLTextureType.UnsignedByte, bmpBits.Scan0);
				Internal.OpenGL.Methods.glErrorToException();

				bmp.UnlockBits(bmpBits);
				bmp.Dispose();
			}
		}

		public static void Clear()
		{
			texturesByFileName.Clear();
			texturesByID.Clear();
		}

		public static Texture FromPicture(PictureObjectModel pic)
		{
			return FromPicture(pic, TextureRotation.None, TextureFlip.None);
		}
		public static Texture FromPicture(PictureObjectModel pic, TextureRotation rotation, TextureFlip flip)
		{
			if (texturesByPicture.ContainsKey(pic))
			{
				Texture tex = texturesByPicture[pic];
				tex.Rotation = rotation;
				tex.Flip = flip;
				return tex;
			}

			uint[] textureIDs = new uint[1];
			
			Internal.OpenGL.Methods.glGenTextures(1, textureIDs);
			Internal.OpenGL.Methods.glErrorToException();

			uint textureID = textureIDs[0];

			Texture texture = new Texture(textureID);
			texture.Target = TextureTarget.Texture2D;
			texture.TextureImage = pic;

			texture.MinFilter = TextureFilter.Linear;
			texture.MagFilter = TextureFilter.Linear;
			texture.TextureWrapR = TextureWrap.Repeat;
			texture.TextureWrapS = TextureWrap.Repeat;
			texture.TextureWrapT = TextureWrap.Repeat;

			texture.Rotation = rotation;
			texture.Flip = flip;

			if (!texturesByID.ContainsKey(textureID))
			{
				texturesByID.Add(textureID, texture);
			}
			if (!texturesByPicture.ContainsKey(pic))
			{
				texturesByPicture.Add(pic, texture);
			}
			return texture;
		}
	}
}
