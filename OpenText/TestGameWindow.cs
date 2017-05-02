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
		public int anonymous_font = 0;
		public Brush white_brush = new SolidBrush(Color.White);

		public void LoadFonts()
		{
			anonymous_font = stb.AddFont("Fonts/Anonymous Pro.ttf", 20);

		}
		public TestGameWindow() : base()
		{
			this.LoadFonts();
			teststring = stb.makeString("Test", white_brush, anonymous_font);
			teststring.TextBitmap.Save("out.bmp");
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
		public void DrawText()
		{
			float Depth = 1.0f;
			GL.PushMatrix();
			GL.LoadIdentity();

			Matrix4 ortho_projection = Matrix4.CreateOrthographicOffCenter(0, this.Width, this.Height, 0, -1, 1);
			GL.MatrixMode(MatrixMode.Projection);
			//GL.PushMatrix();//
			GL.LoadMatrix(ref ortho_projection);
			//GL.Disable(EnableCap.DepthTest);
			//GL.Enable(EnableCap.Blend);
			//GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.Enable(EnableCap.Texture2D);
			GL.BindTexture(TextureTarget.Texture2D, teststring.GetTextureId());
			//GL.Enable(EnableCap.DepthTest);
			//GL.Clear(ClearBufferMask.DepthBufferBit);
			GL.Begin(BeginMode.Quads);
			GL.TexCoord2(0, 0); GL.Vertex3(0, 0, Depth);
			GL.TexCoord2(1, 0); GL.Vertex3(teststring.size.Width, 0, Depth);
			GL.TexCoord2(1, 1); GL.Vertex3(teststring.size.Width, teststring.size.Height, Depth);
			GL.TexCoord2(0, 1); GL.Vertex3(0, teststring.size.Height, Depth);
			//GL.Disable(EnableCap.DepthTest);
			GL.End();
			//GL.ClearDepth(1.0f);
			//GL.ClearDepth(0.5f);
			//GL.Clear(ClearBufferMask.DepthBufferBit);
			GL.PopMatrix();

			//GL.Disable(EnableCap.Blend);
			GL.Disable(EnableCap.Texture2D);
			//GL.Disable (EnableCap.DepthTest);
			//GL.MatrixMode(MatrixMode.Modelview);
			//GL.PopMatrix();
		}

	}
}
