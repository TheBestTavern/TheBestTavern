using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "tutorialStep", menuName = "Turotial/NewTutorial")]
public class TutorialStepDefinition : ScriptableObject
{
    [field: SerializeField] public int tutorialStepID { get; private set; }
    [field: SerializeField] public List<TutorialStepObjectiveDefinition> tutorialID { get; private set; }

}

[Serializable]
public class TutorialStepObjectiveDefinition
{
    public string name { get; private set; }
}