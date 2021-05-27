using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GemScript : MonoBehaviour, IScoreManager, IInteractable, ISkillActivate
{
    bool isScoreUpdated = false;
    bool isMagnet = false;

    public Transform canvas;
    private Transform playerPos;

    [SerializeField]
    private Transform targetPos;

    [SerializeField]
    private GameObject gemImage;
    
    private float speed;
    public Ease movingGemEaseType;

    private void Awake()
    {
        canvas = GameObject.Find("Canvas").transform;
        targetPos = GameObject.Find("GemScore").transform;
    }
    void Update()
    {
        if (isMagnet)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos.position + new Vector3(0f, 0.5f, 0f), speed * Time.deltaTime);
        }
    }

    public void OnActivateSkill(Transform playerPos, float speed)
    {
        isMagnet = true;
        this.playerPos = playerPos;
        this.speed = speed;
    }

    public void Interact()
    {
        Vector3 playerPos = GameObject.Find("MainCube").GetComponent<Player>().transform.position;
        Vector3 createPos = Camera.main.WorldToScreenPoint(playerPos + new Vector3(0f, 1f, 0f));
        GameObject createGemImage = Instantiate(gemImage, createPos, Quaternion.identity, canvas);
        
        createGemImage.transform.DOScale(createGemImage.transform.localScale * 1.3f, 0.2f).SetEase(Ease.InSine).OnComplete(() =>createGemImage.transform.DOScale(createGemImage.transform.localScale / 4.1f, 0.4f).SetEase(Ease.OutSine));
        createGemImage.transform.DOMove(targetPos.position, .6f).SetEase(movingGemEaseType).OnComplete(() => { Destroy(createGemImage); });

        this.gameObject.SetActive(false);
    }

    public void OnScoreUpdate(int score)
    {
        if (!isScoreUpdated)
        {
            EventManager.instance.OnScoreUpdate(score);
            isScoreUpdated = !isScoreUpdated;
        }
    }
   
}
