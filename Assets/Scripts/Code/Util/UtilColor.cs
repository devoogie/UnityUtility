using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Util
{
    public static Color ToColor(this string hexcode)
    {
        if (hexcode == null)
            return Color.white;
        if (hexcode.IsNullOfEmpty())
            return Color.white;
        hexcode = hexcode.ToLower();
        if (hexcode.StartsWith("#"))
            hexcode = hexcode.Substring(1);
        if (hexcode.Length == 6)
        {
            hexcode += "FF";
        }
        var hex = System.Convert.ToUInt32(hexcode, 16);
        var r = ((hex & 0xff000000) >> 0x18) / 255f;
        var g = ((hex & 0xff0000) >> 0x10) / 255f;
        var b = ((hex & 0xff00) >> 8) / 255f;
        var a = ((hex & 0xff)) / 255f;

        return new Color(r, g, b, a);
    }
    public static Color FromHex(this Color color,string hexCode)
    {
        return color = hexCode.ToColor();
    }
    public static string ToHex(this Color color)
    {
        return ColorUtility.ToHtmlStringRGBA(color);
    }
    
}
