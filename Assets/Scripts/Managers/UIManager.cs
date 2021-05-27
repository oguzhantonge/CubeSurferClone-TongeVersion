using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> beforeStartObjects = new List<GameObject>();

    [SerializeField]
    private GameObject beforeStartUI;

    public bool beforeStartUIObjects
    {
        get { return beforeStartUI.activeInHierarchy; }
    }

    [SerializeField]
    private TextMeshProUGUI gemScore;

    [SerializeField]
    private GameObject textShowPrefab;
    public GameObject levelUpUI;
    public GameObject gameOverUI;

    public static UIManager instance;
    
    public void NextLevelTrigger()
    {
        
        EventManager.instance.OnClearLevel();
        beforeStartUI.SetActive(true);
    }
  
    public void HomeTrigger()
    {
        levelUpUI.SetActive(false);
        gameOverUI.SetActive(false);

    }
    public void RestartButton()
    {
        EventManager.instance.OnClearLevel();
    }

    private void Awake()
    {
        EventManager.onClearLevel += ClearUIManager;
        gemScore = GameObject.Find("GemScore").GetComponent<TextMeshProUGUI>();
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gemScore.text = "Gem Score: " + ScoreManager.instance.Score;
        EventManager.onScoreUpdate += ScoreUpdate;
        EventManager.onScoreUpdate += ScoreShow;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EnableBeforeStartObjects(false);
        }
    }

    void EnableBeforeStartObjects(bool isEnable)
    {
         beforeStartUI.SetActive(isEnable);
    }

    void ScoreUpdate(int a)
    {
       gemScore.text = "Gem Score: " + ScoreManager.instance.Score;
    }

    public void ScoreShow(int a)
    {
        GameObject scoreShowText = Instantiate(textShowPrefab, Vector3.zero, Quaternion.identity);
        scoreShowText.GetComponent<Text>().text = "+1";
        scoreShowText.transform.SetParent(GameObject.Find("Canvas").transform);
        scoreShowText.transform.position = new Vector3(600f, 250f, 0f);
        Destroy(scoreShowText.gameObject, 0.4f);
    }
    private void ClearUIManager()
    {
        EnableBeforeStartObjects(true);
        gemScore.text = "Gem Score: " + 0;
        levelUpUI.SetActive(false);
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
