using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T> : IState where T : System.Enum
{
    System.Action<T> Leave{get;set;}
    T Current { get; }
    T LeaveTo { get; }
    
}
public interface IState
{
    void Initialize();
    void OnEnter();
    void OnProgress();
    void OnLeave();
}