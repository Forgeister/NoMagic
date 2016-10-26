using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Step : MonoBehaviour {

    int stepPosition;
    string[] steps =
    {
        "Untap Step",
        "Upkeep Step",
        "Draw Step",
        "Main 1",
        "Beginning Combat Step",
        "Declare Attackers Step",
        "Declare Blockers Step",
        "Combat Damage Step",
        "End Combat Step",
        "Main 2",
        "End Step",
        "Cleanup Step"
    };
    GameObject stepName;
    GameObject untapStep;
    GameObject upkeepStep;
    GameObject drawStep;
    GameObject mainOneStep;
    GameObject beginningCombatStep;
    GameObject declareAttackersStep;
    GameObject declareBlockersStep;
    GameObject combatDamageStep;
    GameObject endCombatStep;
    GameObject mainTwoStep;
    GameObject endStep;
    GameObject cleanupStep;

    void Start()
    {
        stepPosition = 0;
        stepName = GameObject.Find("NamePhase");
        untapStep = GameObject.Find("UntapStep");
        upkeepStep = GameObject.Find("UpkeepStep");
        drawStep = GameObject.Find("DrawStep");
        mainOneStep = GameObject.Find("Main1");
        beginningCombatStep = GameObject.Find("BeginningCombatStep");
        declareAttackersStep = GameObject.Find("DeclareAttackersStep");
        declareBlockersStep = GameObject.Find("DeclareBlockersStep");
        combatDamageStep = GameObject.Find("CombatDamageStep");
        endCombatStep = GameObject.Find("EndCombatStep");
        mainTwoStep = GameObject.Find("Main2");
        endStep = GameObject.Find("EndStep");
        cleanupStep = GameObject.Find("CleanupStep");

        SetStep();

    }
    public void StepX(string step)
    {
        for (int i = 0; i < 12; i++)
        {
            if (step == steps[i].Replace(" ", string.Empty))
            {
                stepPosition = i;
            }
        }
        SetStep();
    }

    public void NextStep()
    {
        if (stepPosition >= 11)
        {
            stepPosition = 0;
        }
        else
        {
            stepPosition += 1;
        }
        SetStep();
    }

    public void PreviousStep()
    {
        if (stepPosition <= 0)
        {
            stepPosition = 11;
        }
        else
        {
            stepPosition -= 1;
        }
        SetStep();
    }
    private void SetStep()
    {
        GameObject stepBtn = GameObject.Find(steps[stepPosition].Replace(" ", string.Empty));
        ColorBlock colorBtn = stepBtn.GetComponent<Button>().colors;
        colorBtn.normalColor = new Color(1f, 1f, 1f, 0.3f);

        for (int i = 0; i < 12; i++)
        {
            stepBtn = GameObject.Find(steps[i].Replace(" ", string.Empty));
            stepBtn.GetComponent<Button>().colors = colorBtn;
        }
        stepName.GetComponent<Text>().text = steps[stepPosition];
        stepBtn = GameObject.Find(steps[stepPosition].Replace(" ", string.Empty));
        colorBtn.normalColor = new Color(1f, 1f, 1f, 1f);
        stepBtn.GetComponent<Button>().colors = colorBtn;
    }
}
