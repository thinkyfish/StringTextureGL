using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.Drawing;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Text;


namespace OpenText
{
	public enum TestMode {Menu};
    
    class TestGameWindow : GameWindow
    {

		public bool resize = false;

		public StringTextureBuilder stb = new StringTextureBuilder();
		public TestMode Mode;
		public void LoadFonts()
		{
			stb.AddFont("Fonts/Anonymous Pro.ttf");
		}
		public TestGameWindow() : base(){
			this.LoadFonts();
		}

    }	
}
