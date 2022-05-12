using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 최초 1회 Only execute 1 time
/// </summary>
namespace MVC
{

    public interface IView
    {
        RectTransform rectTransform { get; }
    }
    public interface IModel
    {

    }


}
public interface IState<T> where T : System.Enum
{
    System.Action Leave{get;set;}
    T LeaveTo { get; }
    void Initialize();
    void OnEnter();
    void OnProgress();
    void OnLeave();
}

public interface IAdventureSystem
{
    void Reset();
}
