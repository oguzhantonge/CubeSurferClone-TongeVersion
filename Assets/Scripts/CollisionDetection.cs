using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CollisionDetection : MonoBehaviour
{
    List<GameObject> cubes = new List<GameObject>();

    public static int total = 3;
   // public static int cubecount;

    //bool isGameStop = false;

    private void Awake()
    {
      EventManager.onClearLevel += ClearCubes;
    }

    void ClearCubes()
    {
        if (cubes.Count>0)
        {
            for (int i = 0; i < cubes.Count; i++)
            {
                Destroy(cubes[i]);
                cubes.RemoveAt(i);
                i--;
            }
        }
    }

    void Start()
    {
        EventManager.onAddPlayerCube += AddNewCube;
        EventManager.onRemovePlayerCube += RemoveCube;
    }

    void AddNewCube(GameObject cube)
    {
        cubes.Add(cube);
    }
    void RemoveCube(bool isGameOver)
    {
        if (cubes.Count <= 0 && isGameOver)
        {
            Debug.Log("Game Over");
            EventManager.instance.OnPlayerPositionUpdate(+1);
            UIManager.instance.gameOverUI.SetActive(true);
            Time.timeScale = 0f;
            return;
        }
        if (cubes.Count <= 0 && isGameOver == false)
        {
            UIManager.instance.levelUpUI.SetActive(true);

            EventManager.instance.OnScoreUpdate((ScoreManager.instance.Score * Stairs.scoreMultiplier) - ScoreManager.instance.Score);
            EventManager.instance.OnLevelFinishUpdate();
            GameObject TotalGemText = GameObject.Find("TotalGemText");
            if (TotalGemText)
            {
                TotalGemText.GetComponent<Text>().text = PlayerPrefs.GetInt("TotalGems", 0).ToString();
            }
            Debug.Log("You Won!");
            Debug.Log("Your score : " + (ScoreManager.instance.Score * Stairs.scoreMultiplier));
            Time.timeScale = 0f;
            
            return;
        }
        else
        {
            Destroy(cubes[cubes.Count - 1]);
            cubes.RemoveAt(cubes.Count - 1);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ENDGAME")
        {
            UIManager.instance.levelUpUI.SetActive(true);

            EventManager.instance.OnScoreUpdate((ScoreManager.instance.Score * Stairs.scoreMultiplier) - ScoreManager.instance.Score);
           //Debug.Log("Your score : " + (ScoreManager.instance.Score * Stairs.scoreMultiplier));
            EventManager.instance.OnLevelFinishUpdate();
            GameObject TotalGemText = GameObject.Find("TotalGemText");
            if (TotalGemText)
            {
                TotalGemText.GetComponent<Text>().text = PlayerPrefs.GetInt("TotalGems", 0).ToString();
            }
            //Debug.Log("You Won!");
           
            Time.timeScale = 0f;
            
        }
        IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();
        interactable?.Interact();

        IScoreManager scoreManager = collision.gameObject.GetComponent<IScoreManager>();
        scoreManager?.OnScoreUpdate(1);
    }

}
