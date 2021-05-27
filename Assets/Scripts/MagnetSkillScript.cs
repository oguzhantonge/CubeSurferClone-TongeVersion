using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSkillScript : MonoBehaviour ,IInteractable
{
    [SerializeField]
    private SkillActivateScript skillActivateScript;

    [SerializeField]
    private float magnetTime = 3f;

    public void Interact()
    {
        skillActivateScript.isMagnet = true;
        skillActivateScript.ActivateMagnetTimer(magnetTime);
        this.gameObject.SetActive(false);
    }

}
