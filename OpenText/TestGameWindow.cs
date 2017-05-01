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
		private PrivateFontCollection pfc;
		private FontFamily[] families;


		public TestMode Mode;
		public void LoadFonts(){
			pfc = new PrivateFontCollection ();
			pfc.AddFontFile ("Fonts/Anonymous Pro.ttf");
			families = pfc.Families;


		}
		public TestGameWindow() : base(){
			this.LoadFonts();
		}

    }	
}
