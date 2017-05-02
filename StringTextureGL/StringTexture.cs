//Written By thinkyfish@github
//License: Public Domain
//some code originally from http://www.opentk.com/node/1554#comment-10625
using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace StringTextureGL
{
	public class StringTexture
	{
		private int textureId;
		public string text;
		public readonly Bitmap TextBitmap;
		public SizeF size;
		private Font font;
		private Brush brush;
		private Color background;
		public StringTexture(string text, Font font, SizeF size, Color foreground, Color background)
		{
			this.textureId = 0;
			this.text = text;
			this.brush = new SolidBrush(foreground);
			this.font = font;
			this.size = size;
			this.background = background;
			this.TextBitmap = new Bitmap(this.Size().Width, this.Size().Height);
			this.CreateTexture();
			this.DrawStringToTexture();
		}

		~StringTexture()
		{
			if (textureId > 0)
				GL.DeleteTexture(textureId);
		}

		public int TextureId()
		{
			return this.textureId;
		}
		public Size Size()
		{
			return size.ToSize();
		}
		public SizeF SizeF()
		{
			return size;
		}
		public void DrawStringToTexture()
		{
			using (Graphics gfx = Graphics.FromImage(TextBitmap))
			{
				gfx.Clear(background);

				gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

				gfx.DrawString(text, font, brush, new PointF(0.0f, 0.0f),
									new StringFormat());

			}



			System.Drawing.Imaging.BitmapData data = TextBitmap.LockBits(new Rectangle(0, 0, TextBitmap.Width, TextBitmap.Height),
			System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, TextBitmap.Width, TextBitmap.Height, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
			TextBitmap.UnlockBits(data);

		}

		private void CreateTexture()
		{
			GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (float)TextureEnvMode.Replace);//Important, or wrong color on some computers
			GL.GenTextures(1, out textureId);
			GL.BindTexture(TextureTarget.Texture2D, textureId);

			BitmapData data = TextBitmap.LockBits(new System.Drawing.Rectangle(0, 0, TextBitmap.Width, TextBitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
			GL.Finish();
			TextBitmap.UnlockBits(data);
		}
	}
}

