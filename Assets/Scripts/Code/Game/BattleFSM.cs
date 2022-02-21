using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleClient
{
    private IBattleState _currentState;

    public void Push(IBattleState state)
    {
        LeaveState();
        _currentState = state;
        Enter();
    }
    private void Enter()
    {
        _currentState.Enter();
    }
    private void LeaveState()
    {
        _currentState.Leave();
    }
    public interface IBattleState
    {
        void Enter();
        void Leave();
    }
}
public enum BattleState
{

}
