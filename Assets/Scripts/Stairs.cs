using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour ,IInteractable
{
    public static int scoreMultiplier = 0;

    bool canInteract = true;
    private void Awake()
    {
        EventManager.onClearLevel += clearScoreMultiplier;
    }

    void clearScoreMultiplier()
    {
        scoreMultiplier = 0;
    }

    public void Interact()
    {
        if (canInteract)
        {
            EventManager.instance.OnRemovePlayerCube(false);
            scoreMultiplier++;
            Debug.Log(scoreMultiplier);
            canInteract = false;
        }
    }
}
