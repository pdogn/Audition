using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Code_Singleton<LevelManager>
{
    [SerializeField] private LevelCatalog levelCatalog;

    [SerializeField] private int currentLevel;

    public LevelCatalog Catalog => levelCatalog;

    public int CurrentLevel => currentLevel;

}
