using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Code_Singleton<UIManager>
{
    public int playerCount = 1;

    public PlayerView_UI playerViews;

    private GameDataSO _gameDataSo;

    public Queue<Sprite> ImageQueue;

    private void Start()
    {
        
    }
    public void SetGameData(GameDataSO gameDataSO)
    {
        _gameDataSo = gameDataSO;
        var _cardItems = _gameDataSo.cardItems;
        ImageQueue = new Queue<Sprite>();
        foreach (var item in _cardItems)
        {
            ImageQueue.Enqueue(item.sprite);
        }
    }

    public void LoadUI()
    {
        //playerViews.CardItems
    }

    public Sprite GetImageInQueue()
    {
        if (ImageQueue.Count > 0)
        {
            return ImageQueue.Dequeue();
        }
        return null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Roll");
            playerViews.RollListview();
        }
    }
}
