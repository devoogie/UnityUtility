using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class TestBezierCurve
{
    [Test]
    public void TestBezier2DTwo()
    {
        var up = Vector3.up;
        var right = Vector3.right;

        playBezier(up, right);
    }
    [Test]
    public void TestBezier2DParabolic()
    {
        var first = new Vector3(-0.2233475f, 6.38029f, 0);
        var stopover = new Vector3(-0.75f, .6676319f, 0);
        var end = new Vector3(3.994937f, 15.34736f, 0);

        playBezier(first, stopover, end);
    }

    private static void playBezier(params Vector3[] pos)
    {
        for (float f = 0; Mathf.Approximately(1.1f, f) == false;)
        {
            var result = Bezier.Get(f, pos);
            Debug.Log(result);
            f += 0.1f;
        }
    }
}