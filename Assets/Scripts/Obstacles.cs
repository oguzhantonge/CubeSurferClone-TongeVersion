using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour, IInteractable 
{
    bool isInteract= true;
    public void Interact()
    {
        if (isInteract)
        {
            EventManager.instance.OnRemovePlayerCube(true);
           
            CollectableCube.index--;
            EventManager.instance.OnPlayerPositionUpdate(-1);
            isInteract = !isInteract;
        }
    }  
   
} 
