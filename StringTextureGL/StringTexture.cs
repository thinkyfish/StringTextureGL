//Written By thinkyfish@github
//License: Public Domain
//some code originally from http://www.opentk.com/node/1554#comment-10625
using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Diagnostics;
namespace StringTextureGL
{
	public class StringTexture
	{
		private int textureId;
		private string text;
		private readonly Bitmap TextBitmap;
		private SizeF size;
		private Font font;
		private Brush brush;
		private Color background;
		private static PrivateFontCollection pfc = new PrivateFontCollection();

		public StringTexture(string text, Font font, Color foreground, Color background)
		{
			this.textureId = 0;
			this.text = text;
			this.brush = new SolidBrush(foreground);
			this.font = font;
			//Debug.WriteLine(this.font.Name);
			this.size = GetTextSize(font, text);
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
		public static Font NewFont(String filename,String familyname, int size, FontStyle style = FontStyle.Bold)
		{
			pfc.AddFontFile(filename);
			int fontnumber = pfc.Families.Length - 1;
			if (fontnumber >= 0)
			{
				foreach (var family in pfc.Families)
				{
					if (familyname.Equals(family.Name))
					{
						Font f = new Font(family, size, style, GraphicsUnit.Pixel);
						return f;
					}
				}
				

			}
			return null;
		}
		public static SizeF GetTextSize(Font f, String text)
		{
			//making this static resolves the catch-22 of needing the size of the bitmap
			//when you declare the bitmap, yet needing a bitmap to measure the size.
			Size one = new Size(1, 1);
			Bitmap btemp = new Bitmap(one.Width, one.Height);
			Graphics g = Graphics.FromImage(btemp);
			return g.MeasureString(text, f);
		}
		public static FontFamily[] FontFamilies()
		{
			return pfc.Families;
		}
		public int TextureId()
		{
			return this.textureId;
		}
		public String Text()
		{
			return text;
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
				gfx.DrawString(text, font, brush, new PointF(0.0f, 0.0f));

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

