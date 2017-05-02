using System;
using System.Drawing;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;
using System.Drawing.Text;
namespace OpenText
{
	public class StringTextureBuilder
	{
		private static Size defaultSize = new Size(1, 1);
		private PrivateFontCollection pfc;
		//private FontFamily[] families;
		public List<Font> fonts = new List<Font>();
		private int currentfont = 0;
		public StringTextureBuilder()
		{
			pfc = new PrivateFontCollection();

			//families = pfc.Families;

		}
		public int AddFont(String name, int size) {
			pfc.AddFontFile(name);
			Font f = new Font(pfc.Families[currentfont], size);
			fonts.Add(f);
			return currentfont++;
		}
		public StringTexture makeString(String text, Brush b, int font = 0)
		{
			SizeF size = GetTextSize(fonts[font], text);
			return new StringTexture(text, b, fonts[font], size);
		}
		public static SizeF GetTextSize(Font f, String text)
		{
			Bitmap btemp = new Bitmap(defaultSize.Width, defaultSize.Height);
			Graphics g = Graphics.FromImage(btemp);
			return g.MeasureString(text, f);
		}
	}
}
