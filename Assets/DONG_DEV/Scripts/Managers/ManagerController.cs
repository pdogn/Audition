using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerController : MonoBehaviour
{
    [SerializeField] private LevelCatalog levelCatalog;

    private void Start()
    {
        LoadGameData();
    }

    public void LoadGameData()
    {
        Debug.Log("lll");
        var level = levelCatalog.gameLevels[LevelManager.Instance.CurrentLevel];
        Debug.Log("lll: " + level.cardItems.Count);
        UIManager.Instance.SetGameData(level);
    }
}
