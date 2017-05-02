using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Text;


namespace OpenText
{
	public enum TestMode { Menu };

	class TestGameWindow : GameWindow
	{

		public bool resize = false;

		public StringTextureBuilder stb = new StringTextureBuilder();
		public TestMode Mode;
		public StringTexture teststring;
		public Font anonymous_font;
		public Brush white_brush = new SolidBrush(Color.White);

		public void LoadFonts()
		{
			anonymous_font = stb.GetFont("Fonts/Anonymous Pro.ttf", 20);

		}
		public TestGameWindow() : base()
		{
			this.LoadFonts();
			teststring = stb.makeString("Test", white_brush, anonymous_font, Color.DarkGreen);
		}
		public void TestDraw()
		{
			GL.Begin(BeginMode.Quads);
			GL.Vertex2(0, 0);
			GL.Vertex2(1.0f, 0);
			GL.Vertex2(1, -1);
			GL.Vertex2(0, -1);
			GL.End();
		}
		public void DrawText(int x, int y, float Depth = 1.0f)
		{

			GL.PushMatrix();
			GL.LoadIdentity();

			Matrix4 ortho_projection = Matrix4.CreateOrthographicOffCenter(0, this.Width, this.Height, 0, -1, 1);
			GL.MatrixMode(MatrixMode.Projection);

			GL.LoadMatrix(ref ortho_projection);

			GL.Enable(EnableCap.Texture2D);
			GL.BindTexture(TextureTarget.Texture2D, teststring.TextureId());

			GL.Begin(BeginMode.Quads);
			GL.TexCoord2(0, 0); GL.Vertex3(x + 0, y + 0, Depth);
			GL.TexCoord2(1, 0); GL.Vertex3(x + teststring.size.Width, y + 0, Depth);
			GL.TexCoord2(1, 1); GL.Vertex3(x + teststring.size.Width, y + teststring.size.Height, Depth);
			GL.TexCoord2(0, 1); GL.Vertex3(x + 0, y + teststring.size.Height, Depth);

			GL.End();
			GL.Disable(EnableCap.Texture2D);
			GL.PopMatrix();


		}

	}
}
