using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T> : IState where T : System.Enum
{
    System.Action Leave{get;set;}
    T LeaveTo { get; }
    
}
public interface IState
{
    void Initialize();
    void OnEnter();
    void OnProgress();
    void OnLeave();
}