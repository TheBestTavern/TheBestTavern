using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "tutorialStep", menuName = "Turotial/NewTutorial")]
public class TtrStepDef : ScriptableObject
{
    [field: SerializeField] public int TutorialStepID { get; private set; }
    [field: SerializeField] public int NextTutorialStepID { get; private set; }
    [field: SerializeField] public List<TtrStepObvDef> TutorialObjectives { get; private set; }
}

[Serializable]
public class TtrStepObvDef
{
    public string stepName;
    public ObvDoType objectiveDoType;
    public ObvCountType tutorialCountType; // 누적이면, 한번 클리어하면 끝. 
    public string doWhat; // ItemID, SceneName, UIName
    public int targetCount;
}