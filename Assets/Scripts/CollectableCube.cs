using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCube : MonoBehaviour, IInteractable, ISkillActivate
{
    public static int index = 0;
    
    private float speed;
    private Transform playerPos;

    bool isInteract = true;
    bool isMagnet = false;

    private void Awake()
    {
        EventManager.onClearLevel += ClearIndex;
    }
    
    public void OnActivateSkill(Transform playerPos, float speed)
    {
        isMagnet = true;
        this.playerPos = playerPos;
        this.speed = speed;
    }
    void ClearIndex()
    {
        index = 0;
    }
    public void Interact()
    {
        if (isInteract)
        {
            isMagnet = false;
            EventManager.instance.OnAddPlayerCube(this.gameObject);
            this.gameObject.transform.parent = GameObject.Find("MainCube").transform;
            index++;
            ChangePosition(-index);
            EventManager.instance.OnPlayerPositionUpdate(1);
            isInteract = !isInteract;
        }
    }

    void ChangePosition(int index) {
        transform.localPosition = new Vector3(0, index, 0);
    }

    void Update()
    {
        if (isMagnet)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos.position + new Vector3(0f, 0.5f, 0f), speed * Time.deltaTime);
        }
    }
}
