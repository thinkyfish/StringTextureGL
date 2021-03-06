﻿//Written By thinkyfish@github
//License: Public Domain
using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using GLTextTest;
//using QuickFont;

namespace GLTextTest
{
	class Program
	{

		[STAThread]
		public static void Main()
		{


			using (var game = new TestGameWindow())
			{
				game.Load += (sender, e) =>
				{
					game.Mode = TestMode.Menu;
					game.VSync = VSyncMode.On;
				};


				game.KeyUp += (sender, e) =>
				{
					switch (e.Key)
					{

						case Key.Escape:
							game.Exit();
							break;
						case Key.F:
							if (game.WindowState == WindowState.Normal)
							{
								game.WindowState = WindowState.Fullscreen;
							}
							else
							{
								game.WindowState = WindowState.Normal;
							}
							break;
						default:
							break;
					}

				};
				game.UpdateFrame += (sender, e) =>
				{
				};

				game.RenderFrame += (sender, e) =>
				{
					if (game.Height < 600)
						game.Height = 600;
					if (game.Width < 600)
						game.Width = 600;
					// render graphics
					GL.ClearDepth(1.0f);
					GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

					GL.MatrixMode(MatrixMode.Projection);
					GL.LoadIdentity();
					GL.Viewport(0, 0, game.Width, game.Height);
					GL.Ortho(-1.0, 1.0, -1.0, 1.0, -1.0, 1.0);

					game.DrawText(20, 50);

					game.SwapBuffers();
				};
				game.Width = 600;
				game.Height = 600;
				// Run the game at 60 updates per second
				game.Run(60.0);
			}
		}
	}
}