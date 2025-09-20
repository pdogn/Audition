using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Card : MonoBehaviour
{
    RectTransform cardRect;
    public Image img;
    public int index;
    public RectTransform targetPos;
    public float scale;

    private void Start()
    {
        cardRect = this.GetComponent<RectTransform>();
    }

    public void SetCard(RectTransform _target, float _scale)
    {
        targetPos = _target;
        scale = _scale;
        UpdateCard(targetPos, scale, 0.5f);
    }

    public void MoveToTargetAndScale(RectTransform _target,float _scale, float duration)
    {
        cardRect.DOAnchorPos(_target.anchoredPosition, duration).SetEase(Ease.InOutSine);
        cardRect.DOScale(_scale, duration).SetEase(Ease.InOutSine);
    }

    public void UpdateCard(RectTransform _target, float _scale, float duration)
    {
        //load image
        //move to target
        MoveToTargetAndScale(_target, _scale, duration);
    }
}
