using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public LevelHolderSO levels;
    public GameObject currentLevelPrefab = null;

    [SerializeField]
    private Image levelSlider;
    
    [SerializeField]
    private Text currentLevelText;
    
    [SerializeField]
    private Text nextLevelText;

   
    private Transform startPos, finishPos,playerPos;
    float totalLevelAmount;

    private void Awake()
    {
       
        LevelGenerator();
        
        EventManager.onLevelFinishUpdate += PlayerPrefLevelArrangement;
        EventManager.onClearLevel += LevelGenerator;
    }

    void PlayerPrefLevelArrangement()
    {
        int currentIndex = PlayerPrefs.GetInt("CurrentLevel");
        currentIndex++;
        PlayerPrefs.SetInt("CurrentLevel", currentIndex);
    } 

    void LevelGenerator()
    {
        int levelIndex = PlayerPrefs.GetInt("CurrentLevel", 0);

        if (levelIndex < levels.levels.Length)
        {
            if (currentLevelPrefab) Destroy(currentLevelPrefab);
            currentLevelPrefab = Instantiate(levels.levels[PlayerPrefs.GetInt("CurrentLevel", 0)]);
        }
        else
        {
            if (currentLevelPrefab) Destroy(currentLevelPrefab);
            currentLevelPrefab = Instantiate(levels.levels[0]);
            PlayerPrefs.SetInt("CurrentLevel", 0);
        }
        startPos = GameObject.Find("MainCube").GetComponent<Player>().transform;
        finishPos = GameObject.Find("EndOfGame").transform;
        totalLevelAmount = finishPos.position.z - startPos.position.z;
        currentLevelText.text = (PlayerPrefs.GetInt("CurrentLevel") + 1).ToString();
        nextLevelText.text = (PlayerPrefs.GetInt("CurrentLevel") + 2).ToString();
    }
    private void Update()
    {
        CalculateLevelFinishAmount();
    }
    private void CalculateLevelFinishAmount()
    {
        Vector3 playerPos = GameObject.Find("MainCube").transform.position;
       
        float levelfinished = playerPos.z / totalLevelAmount;

        LevelSliderFill(levelfinished);
    }
    private void LevelSliderFill(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;

    }

}
