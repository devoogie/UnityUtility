using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

public abstract class GameManager : MonoSingleton<GameManager>, IManager
{
    [SerializeField]
    private InputManager input;
    public static InputManager Input => Instance.input;
    protected FSM<GameState> gameState;
    public abstract bool IsGameOver {get;}
    public ItemSystem itemSystem = new ItemSystem();
    public override void Initialize()
    {
        input.Initialize();
        gameState = new FSM<GameState>();

    }
    void Update()
    {
        Clock.AddTime(Time.deltaTime);
    }
}
public enum GameState
{
    Lobby,
    InGame,
}

public interface IGameState
{
    void OnEnter();
    void OnExit();
}