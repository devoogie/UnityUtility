using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using UnityEngine;

public class TestTryCatch
{
    [Test]
    public void TestIndexOut()
    {
        UnityEngine.Debug.Log("Result OK".AddColor(Color.green));
        StartWithTry(0);
        StartWithThrow(0);
        StartWithIf(0);
        UnityEngine.Debug.Log("Result Error".AddColor(Color.red));
        StartWithTry(-1000);
        StartWithThrow(-1000);
        StartWithIf(-1000);
    }
    void StartWithTry(int a)
    {
        Stopwatch stopwatch = new Stopwatch();
        int[] array = new int[1000];
        stopwatch.Reset();
        stopwatch.Start();
        for(int i = a ; i < a + 1000000; i++)
        {
            try
            {
                array[i] = 1;
            }
            catch
            {

            }
        }
        stopwatch.Stop();
        UnityEngine.Debug.Log("StartWithTry " + stopwatch.Elapsed);
    }
    void StartWithIf(int a)
    {
        Stopwatch stopwatch = new Stopwatch();
        int[] array = new int[1000];
        stopwatch.Reset();
        stopwatch.Start();
        for(int i = a ; i < a + 1000000; i++)
        {
            if(array.IsValid(i))
                array[i] = 1;
            
        }
        stopwatch.Stop();

        UnityEngine.Debug.Log("StartWithIf " + stopwatch.Elapsed);
    }
    void StartWithThrow(int a)
    {
        Stopwatch stopwatch = new Stopwatch();
        int[] array = new int[1000];
        stopwatch.Reset();
        stopwatch.Start();
        for(int i = a ; i < a + 1000000; i++)
        {
            try
            {
                if(array.IsValid(i) == false)
                    throw new System.Exception();
                array[i] = 1;
            }
            catch
            {

            }
        }
        stopwatch.Stop();
        UnityEngine.Debug.Log("StartWithThrow " + stopwatch.Elapsed);
    }
    [Test]
    public void TestIndexOutList()
    {
        UnityEngine.Debug.Log("Result OK".AddColor(Color.green));
        ListWithTry(0);
        ListWithThrow(0);
        ListWithIf(0);
        UnityEngine.Debug.Log("Result Error".AddColor(Color.red));
        ListWithTry(-1000);
        ListWithThrow(-1000);
        ListWithIf(-1000);
    }
    void ListWithTry(int a)
    {
        Stopwatch stopwatch = new Stopwatch();
        var array = new List<int>(1000);

        stopwatch.Reset();
        stopwatch.Start();
        for(int i = a ; i < a + 1000000; i++)
        {
            try
            {
                array[i] = 1;
            }
            catch
            {

            }
        }
        stopwatch.Stop();
        UnityEngine.Debug.Log("ListWithTry " + stopwatch.Elapsed);
    }
    void ListWithIf(int a)
    {
        Stopwatch stopwatch = new Stopwatch();
        var array = new List<int>(1000);
        stopwatch.Reset();
        stopwatch.Start();
        for(int i = a ; i < a + 1000000; i++)
        {
            if(array.IsValid(i))
                array[i] = 1;
            
        }
        stopwatch.Stop();

        UnityEngine.Debug.Log("ListWithIf " + stopwatch.Elapsed);
    }
    void ListWithThrow(int a)
    {
        Stopwatch stopwatch = new Stopwatch();
        var array = new List<int>(1000);
        stopwatch.Reset();
        stopwatch.Start();
        for(int i = a ; i < a + 1000000; i++)
        {
            try
            {
                if(array.IsValid(i) == false)
                    throw new System.Exception();
                array[i] = 1;
            }
            catch
            {

            }
        }
        stopwatch.Stop();
        UnityEngine.Debug.Log("ListWithThrow " + stopwatch.Elapsed);
    }
    
    [Test]
    public void TestDivZero()
    {
        UnityEngine.Debug.Log("Result Ok".AddColor(Color.green));
        DivZeroWithTry(100000,1);
        DivZeroWithThrow(100000,1);
        DivZeroWithIf(100000,1);
        UnityEngine.Debug.Log("Result Error".AddColor(Color.red));
        DivZeroWithTry(100000,0);
        DivZeroWithThrow(100000,0);
        DivZeroWithIf(100000,0);
    }
    void DivZeroWithTry(int repeat,int divWith)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Reset();
        stopwatch.Start();
        int[] array = new int[repeat];
        for(int i = 0 ; i < repeat; i++)
        {
            try
            {
                var a = i/divWith;
                array[i] = a;
            }
            catch
            {

            }
        }
        stopwatch.Stop();

        UnityEngine.Debug.Log("DivZeroWithTry " +" "+ divWith + " "+ stopwatch.Elapsed);
    }
    void DivZeroWithIf(int repeat,int divWith)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Reset();
        stopwatch.Start();
        int[] array = new int[repeat];
        for(int i = 0 ; i < repeat; i++)
        {
            if(divWith == 0)
                continue;
            var a = i/divWith;
            array[i] = a;
            
        }
        stopwatch.Stop();
        UnityEngine.Debug.Log("DivZeroWithIF " +" "+ divWith + " "+ stopwatch.Elapsed);

    }
    void DivZeroWithThrow(int repeat,int divWith)
    {
        Stopwatch stopwatch = new Stopwatch();
        int[] array = new int[repeat];
        stopwatch.Reset();
        stopwatch.Start();
        for(int i = 0 ; i < repeat; i++)
        {
            try
            {

            if(divWith == 0)
                throw new System.Exception();
            var a = i/divWith;
            array[i] = a;
            }
            catch
            {

            }
            
        }
        stopwatch.Stop();
        UnityEngine.Debug.Log("DivZeroWithThrow " +" "+ divWith + " "+ stopwatch.Elapsed);
    } 
}
