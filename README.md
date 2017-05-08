# StringTextureGL
This is a simple library with the minimal amount of code to put a string into an opengl texture.

Included is a simple test program to demonstrate how to use the library.

First you have to include a font file in the output directory and call:

`yourfont = StringTexture.NewFont("FontFile.ttf","FontName", <Fontsize>);`

Or you can use pull in a system font with the default `Font` class call.

Then you create the texture handler with a call to:

`yourStringTexture = new StringTexture("Text Here", yourfont, Color.YourForegroundColor, Color.YourBackgroundColor);`

Then you can draw the GL texture normally.

```GL.Enable(EnableCap.Texture2D);
GL.BindTexture(TextureTarget.Texture2D, yourStringtexture.TextureId());
GL.Begin (PrimitiveType.Quads);
GL.TexCoord2 (0, 0);
GL.Vertex2 (location.X , location.Y);
GL.TexCoord2 (1, 0);
GL.Vertex2 (location.X + size.Width, location.Y);
GL.TexCoord2 (1, 1);
GL.Vertex2 (location.X + size.Width, location.Y + size.Height);
GL.TexCoord2 (0, 1);
GL.Vertex2 (location.X , location.Y + size.Height)
GL.End ();
```

The texture can be deleted with:

`yourStringTexture = null;`

This library *just* puts the string into a texture, it doesn't know how to draw a string. This lets the user draw in their own way, with their own methods.
