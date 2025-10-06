using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseMatcher : Code_Singleton<PoseMatcher>
{
    public PlayerManager playerManager;

    private readonly Dictionary<string, int[]> jointIndexGroups = new Dictionary<string, int[]>
    {
        { "head",       new [] { 3, 2, 20 } },
        { "body",       new [] { 20, 1, 0 } },
        { "shoulder",   new [] { 4, 20, 8 } },
        { "leftHand",   new [] { 4, 5, 6, 7 } },
        { "rightHand",  new [] { 8, 9, 10, 11 } },
        { "leftFoot",   new [] { 12, 13, 14 } },
        { "rightFoot",  new [] { 16, 17, 18 } }
    };

    public void LoadPath(PosePath _posePath, GameObject[] bodyJoints)
    {
        foreach (var group in jointIndexGroups)
        {
            foreach (int id in group.Value)
            {
                if (id < 0 || id >= bodyJoints.Length)
                {
                    Debug.LogWarning($"Index {id} không hợp lệ cho nhóm {group.Key}");
                    continue;
                }

                _posePath.parts[group.Key].Add(bodyJoints[id]);
            }
        }
    }
}
