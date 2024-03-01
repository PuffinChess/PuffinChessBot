using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public GameState GameState;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState state)
    {
        GameState = state;
        switch (state)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnPieces:
                //
                break;
            default: 
                break;
        }
    }

}


public enum GameState
{
    GenerateGrid,
    SpawnPieces
}