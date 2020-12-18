using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "ScriptableObjects/Quest", order = 0)]
public class Quest : ScriptableObject
{
    [TextArea(3, 10)]
    public string fact;
    [TextArea(3, 10)]
    public string[] answers;
    public int trueAnswer;
    public bool answeredWell;
}

