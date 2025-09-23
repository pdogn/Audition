using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Card : MonoBehaviour
{
    RectTransform cardRect;
    public Image cardImage;
    [SerializeField] private int index;
    public int Index
    {
        get => index;
        set
        {
            if(value != index)
            {
                index = value;

                if(index == 4)
                {
                    this.transform.GetChild(0).gameObject.SetActive(false);
                }
                if (index == 1 && cardImage.sprite != null)
                {
                    this.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }

    public RectTransform targetPos;
    public float scale;

    private void Start()
    {
        cardRect = this.GetComponent<RectTransform>();
        cardImage = this.transform.GetChild(0).gameObject.GetComponent<Image>();
    }

    public void SetCard(RectTransform _target, float _scale)
    {
        targetPos = _target;
        scale = _scale;
        UpdateCard(targetPos, scale, 0.5f);
    }

    void MoveToTargetAndScale(RectTransform _target,float _scale, float duration)
    {
        cardRect.DOAnchorPos(_target.anchoredPosition, duration).SetEase(Ease.InOutSine);
        cardRect.DOScale(_scale, duration).SetEase(Ease.InOutSine);
    }

    public void LoadImage()
    {
        cardImage.sprite = UIManager.Instance.GetImageInQueue();
        if (cardImage.sprite == null)
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void UpdateCard(RectTransform _target, float _scale, float duration)
    {
        //load image
        //LoadImage();
        //move to target
        MoveToTargetAndScale(_target, _scale, duration);
    }
}


[System.Serializable]
public class CardData
{
    public Sprite sprite;
}
