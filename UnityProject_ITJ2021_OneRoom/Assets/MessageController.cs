using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MessageController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpro;
    [SerializeField] private float messageDuration;
    [SerializeField] private float messageSpeed;

    //private DOTweenTMPAnimator _tweenText;


    public void WriteMessage(string message)
    {
        tmpro.DOKill();
        tmpro.text = "";
        var seq = DOTween.Sequence();

        seq.Append(tmpro.DOText(message, messageSpeed));
        seq.AppendInterval(messageDuration);
        seq.Append(tmpro.DOFade(0f, 1f)).OnComplete(ResetText);

        seq.Play();

    }

    private void ResetText()
    {
        tmpro.text = "";
        tmpro.DOFade(1f, 0f);
    }
}
