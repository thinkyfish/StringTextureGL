//Written By thinkyfish@github
//License: Public Domain
using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;
using System.Drawing.Text;
namespace StringTextureGL
{
	public class StringTextureBuilder
	{
		private static Size defaultSize = new Size(1, 1);
		private PrivateFontCollection pfc;
		private int currentfont = 0;
		public StringTextureBuilder()
		{
			pfc = new PrivateFontCollection();
		}
		public Font GetFont(String name, int size) {
			pfc.AddFontFile(name);
			Font f = new Font(pfc.Families[currentfont], size);
			return f;
		}
		public StringTexture MakeString(String text, Font font,Color foreground, Color background)
		{
			SizeF size = GetTextSize(font, text);
			return new StringTexture(text, font, size, foreground, background);
		}
		public static SizeF GetTextSize(Font f, String text)
		{
			Bitmap btemp = new Bitmap(defaultSize.Width, defaultSize.Height);
			Graphics g = Graphics.FromImage(btemp);
			return g.MeasureString(text, f);
		}
	}
}
