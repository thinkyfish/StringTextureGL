﻿//Written By thinkyfish@github
//License: Public Domain
using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Text;
using StringTextureGL;
namespace GLTextTest
{
	public enum TestMode { Menu };

	class TestGameWindow : GameWindow
	{

		public bool resize = false;


		public TestMode Mode;
		public StringTexture teststring;
		public Font anonymous_font;
		public Brush white_brush = new SolidBrush(Color.White);


		public TestGameWindow() : base()
		{
			anonymous_font = StringTexture.NewFont("Fonts/Anonymous Pro.ttf", "Anonymous Pro", 20, FontStyle.Bold);
			teststring = new StringTexture("This is a test string\nLine 2 of test string", anonymous_font, Color.White, Color.DarkGreen);
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

			Size size = teststring.Size();
			GL.Begin(PrimitiveType.Quads);
			GL.TexCoord2(0, 0); GL.Vertex3(x + 0, y + 0, Depth);
			GL.TexCoord2(1, 0); GL.Vertex3(x + size.Width, y + 0, Depth);
			GL.TexCoord2(1, 1); GL.Vertex3(x + size.Width, y + size.Height, Depth);
			GL.TexCoord2(0, 1); GL.Vertex3(x + 0, y + size.Height, Depth);

			GL.End();
			GL.Disable(EnableCap.Texture2D);
			GL.PopMatrix();


		}

	}
}
