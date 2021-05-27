using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillActivateScript : MonoBehaviour
{
    public bool isMagnet = false;
    public void ActivateMagnetTimer(float time)
    {
        StartCoroutine(MagnetTimer(time));
    }

    IEnumerator MagnetTimer(float time)
    {
        yield return new WaitForSeconds(time);
        isMagnet = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (isMagnet)
        {
            ISkillActivate iSkillActivate = other.gameObject.GetComponent<ISkillActivate>();
            iSkillActivate?.OnActivateSkill(this.gameObject.GetComponentInParent<Player>().transform, 11f);
        }
    }

}
