using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using System.Reflection;

public class Buttons : MonoBehaviour, IPointerDownHandler {

    LifePanel lifePanel;
    Step step;

    void Start () {
        lifePanel = GameObject.Find("Database").GetComponent<LifePanel>();
        step = GameObject.Find("Database").GetComponent<Step>();

	}

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject != null)
        {
            if ((gameObject.transform.parent.parent.parent.name != "Phases")&&((gameObject.name!="NextPhase")&&(gameObject.name!="PrevPhase")))
            {
                MethodInfo mi = lifePanel.GetType().GetMethod(gameObject.name);

                if (Input.GetMouseButtonDown(1))
                {
                    for (int i = 0; i < 5; i++)
                        mi.Invoke(lifePanel, null);
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    mi.Invoke(lifePanel, null);
                }
            }else
            {
                if (gameObject.name == "NextPhase")
                {
                    step.NextStep();
                }else if(gameObject.name == "PrevPhase")
                {
                    step.PreviousStep();
                }else
                {
                    step.StepX(gameObject.name);
                }

            }
        }
    }
}
