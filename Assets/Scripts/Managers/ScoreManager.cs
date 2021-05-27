using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    private int totalGems;
    private int score = 0;
    public int Score
    {
        get{ return score; }
      
    }

    public static ScoreManager instance;

    public void PlayerPrefGemArrangement()
    {
        totalGems += score;
        score = 0;
        PlayerPrefs.SetInt("TotalGems", totalGems);

    } 

    private void Awake()
    {
       // PlayerPrefs.DeleteKey("TotalGems");
        EventManager.onLevelFinishUpdate += PlayerPrefGemArrangement;
        EventManager.onScoreUpdate += OnScoreUpdate;
        EventManager.onClearLevel += ClearScoreChanges;
        
        totalGems = PlayerPrefs.GetInt("TotalGems", 0);

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
      
    }

    private void OnScoreUpdate(int score)
    {
        this.score += score;
        Debug.Log(Score);
    }
    private void ClearScoreChanges()
    {
        score = 0;
    }
}
