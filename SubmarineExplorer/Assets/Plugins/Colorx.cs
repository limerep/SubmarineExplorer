/*
	Colorx.cs
	© Fri Jul  7 20:17:52 CDT 2006 Graveck Interactive
	by Jonathan Czeck
*/
using UnityEngine;

public class Colorx
{
	public static Color Slerp(Color a, Color b, float t)
	{
		return (HSBColor.Lerp(HSBColor.FromColor(a), HSBColor.FromColor(b), t)).ToColor();
	}
}
