using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T> : IState where T : System.Enum
{
    event System.Action Leave;
    T LeaveTo { get; }
    
}
public interface IState
{
    void Initialize();
    void OnEnter();
    void OnProgress();
    void OnLeave();
}