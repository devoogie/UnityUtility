using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
public enum RichTextOption
{
    voffset
}
public enum Align
{
    left,
    center,
    right
}
public static partial class Utility
{
    private static System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
    public const string RichTextFormat = "<{0}={1}>{2}</{0}>";
    public const string RichTextFormatSimple = "<{0}>{1}</{0}>";

    public static bool IsNullOfEmpty(this string content)
    {
	    if (content == "null")
		    return true;
	    
        return string.IsNullOrEmpty(content);
    }
    public static string StringAppend(params string[] contents)
    {
        stringBuilder.Clear();
        foreach(var content in contents)
            stringBuilder.Append(content);
        return stringBuilder.ToString();
    }
    public static string StringFormat(string origin, params object[] contents)
    {
        stringBuilder.Clear();
        stringBuilder.AppendFormat(origin,contents);
        return stringBuilder.ToString();
    }
    public static string StringReplace(string origin, string key,string replace)
    {
        stringBuilder.Clear();
        stringBuilder.Append(origin);
        stringBuilder.Replace(key,replace);
        return stringBuilder.ToString();
    }
    public static string AddColor(this string text,Color color)
    {
        string hex = StringAppend("#",color.ToHex());
        stringBuilder.Clear();
        stringBuilder.AppendFormat(RichTextFormat,"color",hex,text);
        return stringBuilder.ToString();
    }
    
    public static string SetLink(this string text,string key)
    {
        stringBuilder.Clear();
        stringBuilder.AppendFormat(RichTextFormat,"link",key,text);
        var linkResult = stringBuilder.ToString();
        
        return linkResult;
    }
    public static string SetLinkHighlight(this string text,string key)
    {
        stringBuilder.Clear();
        stringBuilder.AppendFormat(RichTextFormat,"link",key,text);
        var linkResult = stringBuilder.ToString();
        return linkResult.SetUnderLine().AddColor(ColorSet.Link);
    }
    public static string SetSize(this string text,float size)
    {
        stringBuilder.Clear();
        stringBuilder.AppendFormat(RichTextFormat, "size", size + "%", text);
        return stringBuilder.ToString();
    }
    public static string SetSizeFixed(this string text,float size)
    {
        stringBuilder.Clear();
        stringBuilder.AppendFormat(RichTextFormat, "size", size, text);
        return stringBuilder.ToString();
    }
    public static string SetVOffset(this string text, float em)
    {
        stringBuilder.Clear();
        stringBuilder.AppendFormat(RichTextFormat, "voffset", em.ToString() + "em", text);
        return stringBuilder.ToString();
    }
    public static string SetAlign(this string text,Align align)
    {
        stringBuilder.Clear();
        stringBuilder.AppendFormat(RichTextFormat,"align",align,text);
        return stringBuilder.ToString();
    }
    public static string SetIndent(this string text, float indent)
    {
        stringBuilder.Clear();
        stringBuilder.AppendFormat(RichTextFormat, "indent", indent + "%", text);
        return stringBuilder.ToString();
    }
    public static string SetUnderLine(this string text)
    {
        stringBuilder.Clear();
        stringBuilder.AppendFormat(RichTextFormatSimple, "u", text);
        return stringBuilder.ToString();
    }

	public static string IntToCommaString(long num)
    {
        bool minus = false;

        if (num < 0)
        {
            minus = true;
            num = -num;
        }

        string result = string.Empty;

        do
        {
            long v = num % 1000;
            num /= 1000;

            if (num > 0)
                result = string.Format(",{0:D3}", v) + result;
            else
                result = v.ToString() + result;
        }
        while (num > 0);

        if (minus)
        {
            result = "-" + result;
        }

        return result;
    }
    public static string AddLevel(this string contents, int level)
    {
        if (level <= 0)
            return contents;
        string lv = " +";
        if (level > 1)
            lv = Utility.StringAppend(lv, (level).ToString());
        contents = Utility.StringAppend(contents, lv.SetVOffset(0.4f).SetSize(70));
        return contents;
    }

    private static System.Random random = new System.Random((int)DateTime.Now.Ticks & 0x0000FFFF); //랜덤 시드값

    public static string RandomString(int _nLength = 16)
    {
        const string strPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";  //문자 생성 풀
        char[] chRandom = new char[_nLength];

        for (int i = 0; i < _nLength; i++)
        {
            chRandom[i] = strPool[random.Next(strPool.Length)];
        }
        string strRet = new String(chRandom);   // char to string
        return strRet;
    }
    
    static public StringBuilder GetStringBuilder()
    {
	    if (stringBuilder == null)
		    stringBuilder = new StringBuilder();

	    stringBuilder.Length = 0;

	    return stringBuilder;
    }

}   
