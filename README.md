# StringTextureGL
This is a simple library with the minimal amount of code to put a string into an opengl texture.

Included is a simple test program to demonstrate how to use the library.

First you have to provide a font file and call 

`yourStringTextureBuilder = new StringTextureBuilder()`

`yourfont = yourStringTextureBuilder.GetFont("FontName.ttf", <Fontsize>)`.

Then you create the texture handler with a call to 

`yourStringTexture = yourStringTextureBuilder.MakeString("Text Here", new SolidBrush(Color.YourForegroundColor), yourfont, Color.YourBackgroundColor)`

Then you can draw the GL texture normally with the id as 

`yourStringTexture.TextureId()`

This library *just* puts the string into a texture, it doesn't know how to draw a string. This lets the user draw in their own way, with their own methods.
