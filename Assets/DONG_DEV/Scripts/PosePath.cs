using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosePath
{
    public Dictionary<string, List<GameObject>> parts;
    public PosePath()
    {
        parts = new Dictionary<string, List<GameObject>>()
        {
            { "head", new List<GameObject>() },
            { "body", new List<GameObject>() },
            { "shoulder", new List<GameObject>() },
            { "leftHand", new List<GameObject>() },
            { "rightHand", new List<GameObject>() },
            { "leftFoot", new List<GameObject>() },
            { "rightFoot", new List<GameObject>() }
        };
    }
}
