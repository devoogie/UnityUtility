using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine<T> where T : System.Enum
{
    public IState<T> currentState;

    public bool ApplyState(IState<T> state)
    {
        currentState = state;
        currentState?.OnEnter();
        currentState?.OnProgress();
        return true;
    }
}


public class FSM<T> where T : System.Enum
{
    // public Stack<T> stateStack = new Stack<T>();
    public Dictionary<T, IState<T>> stateDictionary;
    protected FiniteStateMachine<T> stateMachine;
    public IState<T> CurrentState => stateMachine.currentState;
    public T lastStateType;
    public FSM()
    {
        stateDictionary = new Dictionary<T, IState<T>>();
        stateMachine = new FiniteStateMachine<T>();
    }
    public void AddState(T stateType, IState<T> iState)
    {
        iState.Initialize();
        stateDictionary.Add(stateType, iState);
    }
    public void SetState(T stateType)
    {
        string keys = string.Empty;
        foreach (var keyvalue in stateDictionary.Keys)
            keys += keyvalue.ToString() + " ";
        lastStateType = stateType;
        stateMachine.currentState = null;
        if (stateDictionary.ContainsKey(stateType))
            stateMachine.ApplyState(stateDictionary[stateType]);

    }
    public void SwitchState(T stateType)
    {
        string keys = string.Empty;
        foreach (var keyvalue in stateDictionary.Keys)
            keys += keyvalue.ToString() + " ";

        if (CurrentState != null)
        {
            CurrentState.OnLeave();
            stateMachine.currentState = null;
        }
        lastStateType = stateType;
        if (stateDictionary.ContainsKey(stateType))
            SetState(stateType);
    }
    public void LeaveCurrent()
    {
        if (stateMachine.currentState == null)
            return;
        SwitchState(CurrentState.LeaveTo);
    }
    public void Progress()
    {
        if (stateMachine.currentState == null)
            return;
        stateMachine.currentState.OnProgress();
    }

}