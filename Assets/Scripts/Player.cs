using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField]
    private int horizontalSpeed;

    [SerializeField]
    private int verticalSpeed;

    [SerializeField]
    private float minPlayerXPos;

    [SerializeField]
    private float maxPlayerXPos;

    private float playerPosition;

    private Vector2 firstTouchPos;
    private Vector2 currentTouchPos;
    private Vector3 firstPosOfPlayer;

    [SerializeField]
    private bool canPlay = false;
   
    private void ClearPlayerChanges()
    {
        //Debug.Log(firstPosOfPlayer);
        transform.position = firstPosOfPlayer;
    }

    private void Awake()
    {
        firstPosOfPlayer = transform.position;
        EventManager.onClearLevel += ClearPlayerChanges;
        EventManager.onLevelFinishUpdate += ClearPlayerChanges;
    }

    void Start()
    {
        EventManager.onPlayerPositionUpdate += ChangePlayerPosition;
    }

    void ChangePlayerPosition(int input)
    {
        /*
             sequence = DOTween.Sequence();
             sequence.Append(transform.DOMoveY((transform.position.y + input), 1));
             sequence.Join(transform.DOMoveZ((transform.position.z + verticalSpeed), 1));
        */

        /*
            Vector3 targetPos = this.transform.position + Input;
            Vector3 learpedPosition = Vector3.Lerp(transform.position, targetPos, 1.5f * Time.deltaTime);
            transform.position = learpedPosition;
        */
        StartCoroutine(WaintAndChangePlayerPosition(input));
    }

    IEnumerator WaintAndChangePlayerPosition(int input)
    {
        if (input<0)
        {
            yield return new WaitForSeconds(0.18f);
        }
        else
        {
            yield return new WaitForSeconds(0.07f);
        }
        
        Vector3 temp = transform.position;
        temp.y += input;
        transform.position = temp;
    }
    
    void Update()
    {
        if (transform.position.y < 0.5f)
        {
            transform.position = new Vector3(transform.position.x, firstPosOfPlayer.y, transform.position.z);
        }
        if (Input.GetMouseButton(0))
        {
            canPlay = true;
        };
        if (canPlay && !UIManager.instance.beforeStartUIObjects)
        {
            CalculateTouchPos();
            PlayerMovement(horizontalSpeed, verticalSpeed, minPlayerXPos, maxPlayerXPos);
        }
    }

    void PlayerMovement(int horizontalspeed, int verticalSpeed, float minPlayerXPos, float maxPlayerXPos)
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * horizontalSpeed * Time.deltaTime;
        transform.Translate(horizontalMovement, 0, verticalSpeed * Time.deltaTime);
        float playerPosition = Mathf.Clamp(transform.position.x, minPlayerXPos, maxPlayerXPos);
        transform.position = new Vector3(playerPosition, transform.position.y, transform.position.z);
    }

    void CalculateTouchPos()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstTouchPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            currentTouchPos = Input.mousePosition;
            Vector2 touchDelta = (currentTouchPos - firstTouchPos);
            double newPosX = (transform.position.x + (touchDelta.x * 0.01));
            transform.position = new Vector3(Mathf.Clamp((float)newPosX, minPlayerXPos, maxPlayerXPos), transform.position.y, transform.position.z);
            firstTouchPos = Input.mousePosition;
        }
    }
}
