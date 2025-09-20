using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView_UI : MonoBehaviour
{
    public Card[] CardItems;
    public RectTransform[] CardPos;

    public Transform parentCard;
    public RectTransform parentPos;

    float[] Scale = { 1f, 1.2f, 1.8f, 2.2f, 1f };

    private void Start()
    {
        CardItems = new Card[5];
        int i = 0;
        foreach(Transform c in parentCard)
        {
            CardItems[i] = c.gameObject.GetComponent<Card>();
            i++;
        }
        CardPos = new RectTransform[5];
        int j = 0;
        foreach(RectTransform c in parentPos)
        {
            CardPos[j] = c;
            j++;
        }

        InitCard();
    }

    void InitCard()
    {
        foreach(Card c in CardItems)
        {
            c.gameObject.SetActive(false);
            c.index = 0;
            c.SetCard(CardPos[c.index], Scale[c.index]);
        }
    }

}
