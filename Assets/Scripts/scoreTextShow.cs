using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class scoreTextShow : MonoBehaviour
{
    private Sequence sequence;
    void floatingText()
    {
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY((transform.position.y + 200f), 0.4f));
    }

} // Class
