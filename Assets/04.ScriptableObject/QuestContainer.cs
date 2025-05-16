using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestContainer", menuName = "SOcontainer/new QuestContainer")]
public class QuestContainer : ScriptableObject
{
    [Header("퀘스트-NPC 수치")]
    public int sosoQuest = 20;
    public int goodQuest = 10;
    public int notBadQuest = 5;
    public int failQuest = -5;
}
