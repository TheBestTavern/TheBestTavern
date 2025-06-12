using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectiveUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI stepName;
    [SerializeField] List<ObvTextSlot> obvsSlots = new();

    [SerializeField] Transform obv_TmpTrs;
    [SerializeField] ObvTextSlot obvSlotPref;

    bool isReady;

    public void SetAll()
    {
        Ready();
        for (int i = 0; i < obvsSlots.Count; i++)
        {
            SetObv(i);
        }
    }

    public void Ready()
    {
        gameObject.SetActive(true);
        var stepInstance = TutorialManager.Instance.curTtrStepInstance;
        var stepDef = TutorialManager.Instance.GetCurTtrStepDef();
        stepName.text = stepDef.StepName;

        int rest = obvsSlots.Count - stepDef.TutorialObjectives.Count;
        if (rest < 0)
        {
            for (int j = 0; j < Mathf.Abs(rest); j++)
            {
                obvsSlots.Add(Instantiate(obvSlotPref, obv_TmpTrs));
            }
        }
        else
        {
            for (int k = obvsSlots.Count; k > stepDef.TutorialObjectives.Count; k--)
            {
                obvsSlots[k - 1].gameObject.SetActive(false);
            }
        }
        isReady = true;
    }

    public void SetObv(int index)
    {
        if (!isReady)
        {
            Ready();
        }

        var stepInstance = TutorialManager.Instance.curTtrStepInstance;
        var stepDef = TutorialManager.Instance.GetCurTtrStepDef();

        obvsSlots[index].gameObject.SetActive(true);

        int a = stepInstance.obvCurCounts[index];
        int b = stepDef.TutorialObjectives[index].targetCount;
        string _a;
        if (a >= b)
        {
            _a = $"<b><color=#00FB08>{a}</color></b>";
        }
        else if (a == 0)
        {
            _a = $"<b><color=#FA0000>{a}</color></b>";
        }
        else
        {
            _a = $"<b><color=#FAEC00>{a}</color></b>";
        }

        obvsSlots[index].obvName.text = stepDef.TutorialObjectives[index].stepName;
        obvsSlots[index].obvCount.text = _a + $" / <b><color=#00FB08>{stepDef.TutorialObjectives[index].targetCount}</color></b>";
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        isReady = false;
    }
}