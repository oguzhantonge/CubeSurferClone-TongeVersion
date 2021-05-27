using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void AddPlayerCube(GameObject cube);
    public delegate void RemovePlayerCube(bool isGameOver);
    public delegate void ScoreUpdate(int score);
    public delegate void PlayerPositionUpdate(int input);
    public delegate void LevelFinishUpdate();
    public delegate void ClearLevel();

    public static event AddPlayerCube onAddPlayerCube;
    public static event RemovePlayerCube onRemovePlayerCube;
    public static event ScoreUpdate onScoreUpdate;
    public static event PlayerPositionUpdate onPlayerPositionUpdate;
    public static event LevelFinishUpdate onLevelFinishUpdate;
    public static event ClearLevel onClearLevel;

    public static EventManager instance;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void OnClearLevel()
    {
        onClearLevel?.Invoke();
    }
    public void OnLevelFinishUpdate()
    {
        onLevelFinishUpdate?.Invoke();
    }
    public void OnAddPlayerCube(GameObject cube)
    {
        onAddPlayerCube?.Invoke(cube);
    }
    public void OnRemovePlayerCube(bool isGameOver)
    {
        onRemovePlayerCube?.Invoke(isGameOver);
    }
    public void OnScoreUpdate(int score)
    {
        onScoreUpdate?.Invoke(score);
    }
    public void OnPlayerPositionUpdate(int input)
    {
        onPlayerPositionUpdate?.Invoke(input);
    }
    
}
