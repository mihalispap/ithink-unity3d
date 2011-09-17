using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripperAgent : MonoBehaviour
{
    iThinkBrain brain;

    public void Awake()
    {
        brain = new iThinkBrain();

        // Sensing GameObjects
        List<String> tags = new List<String>();
        tags.Add("block");
        brain.sensorySystem.OmniscientUpdate(this.gameObject, brain.gameParts, tags);

        // Building knowledge
        Gripper7();
        brain.curState = brain.startState;

        // Building Actions from knowledge
        brain.actionSet = new iThinkActionSet(brain.getKnownFacts());
        brain.actionSet.Init(this.gameObject, brain.getKnownObjects());

        // Init search        
        renderCurrentState();
        brain.planner.forwardSearch(brain.startState, brain.goalState, brain.actionSet, 1);
    }

    public void Update()
    {
        if (Time.time - brain.lastUpdate >= 1)
        {
            if (brain.planner.getPlan().getActionCount() != 0)
                applyNextAction();
            brain.lastUpdate = Time.time;

            renderCurrentState();
        }
    }

    private void renderCurrentState()
    {
        int i;
        BitArray stacks = new BitArray(brain.getKnownObjects().Count);

        foreach (iThinkFact fact in brain.curState.getFactList())
        {
            if (fact.getName().Equals("onTable"))
            {
                i = findNextFreeStack(stacks);
                fact.getObj(0).transform.position = new Vector3((float)(-1.5 + i * 1.5), 0, 0);
                stacks.Set(i - 1, true);
            }
        }

        foreach (iThinkFact fact in brain.curState.getFactList())
        {
            if (fact.getName().Equals("on"))
            {
                fact.getObj(0).transform.position = fact.getObj(1).transform.position;
                fact.getObj(0).transform.Translate(0, (float)1.5, 0);
            }

            else if (fact.getName().Equals("holding"))
            {
                fact.getObj(0).transform.position = new Vector3(15, 5, 0);
            }
        }
    }
    static int findNextFreeStack(BitArray bitmap)
    {
        int count = 0;
        foreach (bool bit in bitmap)
        {
            if (bit)
                return count;
            count++;
        }
        return count;
    }
    void applyNextAction()
    {
        List<iThinkAction> actionList = brain.planner.getPlan().getActions();

        brain.curState = new iThinkState(actionList[0].applyEffects(brain.curState));

        actionList.RemoveAt(0);
    }

    void GripperTest()
    {
        ///////////////////////////
        // Building current state//
        ///////////////////////////
        brain.factList = new List<iThinkFact>();
        brain.factList.Add(new iThinkFact("onTable", GameObject.Find("A")));
        brain.factList.Add(new iThinkFact("onTable", GameObject.Find("B")));
        brain.factList.Add(new iThinkFact("onTable", GameObject.Find("C")));
        brain.factList.Add(new iThinkFact("onTable", GameObject.Find("D")));
        brain.factList.Add(new iThinkFact("onTable", GameObject.Find("E")));
        brain.factList.Add(new iThinkFact("onTable", GameObject.Find("F")));
        brain.factList.Add(new iThinkFact("clear", GameObject.Find("A")));
        brain.factList.Add(new iThinkFact("clear", GameObject.Find("B")));
        brain.factList.Add(new iThinkFact("clear", GameObject.Find("C")));
        brain.factList.Add(new iThinkFact("clear", GameObject.Find("D")));
        brain.factList.Add(new iThinkFact("clear", GameObject.Find("E")));
        brain.factList.Add(new iThinkFact("clear", GameObject.Find("F")));

        brain.factList.Add(new iThinkFact("gripEmpty"));
        brain.startState = new iThinkState("Initial", new List<iThinkFact>(brain.factList));

        /////////////////
        // Create goal //
        /////////////////
        brain.factList.Clear();
        brain.factList.Add(new iThinkFact("on", GameObject.Find("A"), GameObject.Find("B")));
        brain.factList.Add(new iThinkFact("on", GameObject.Find("B"), GameObject.Find("C")));
        brain.factList.Add(new iThinkFact("on", GameObject.Find("C"), GameObject.Find("D")));
        brain.factList.Add(new iThinkFact("on", GameObject.Find("D"), GameObject.Find("E")));
        brain.factList.Add(new iThinkFact("on", GameObject.Find("E"), GameObject.Find("F")));

        brain.goalState = new iThinkState("Goal", new List<iThinkFact>(brain.factList));
    }
    void Gripper3Easy()
    {
        ///////////////////////////
        // Building current state//
        ///////////////////////////
        brain.factList = new List<iThinkFact>();
        brain.factList.Add(new iThinkFact("onTable", GameObject.Find("A")));
        brain.factList.Add(new iThinkFact("onTable", GameObject.Find("B")));
        brain.factList.Add(new iThinkFact("onTable", GameObject.Find("C")));
        brain.factList.Add(new iThinkFact("clear", GameObject.Find("A")));
        brain.factList.Add(new iThinkFact("clear", GameObject.Find("B")));
        brain.factList.Add(new iThinkFact("clear", GameObject.Find("C")));
        brain.factList.Add(new iThinkFact("gripEmpty"));
        brain.startState = new iThinkState("Initial", new List<iThinkFact>(brain.factList));

        /////////////////
        // Create goal //
        /////////////////
        brain.factList.Clear();
        brain.factList.Add(new iThinkFact("on", GameObject.Find("A"), GameObject.Find("B")));
        brain.factList.Add(new iThinkFact("on", GameObject.Find("B"), GameObject.Find("C")));

        brain.goalState = new iThinkState("Goal", new List<iThinkFact>(brain.factList));
    }
    void GripperDifficult()
    {
        ///////////////////////////
        // Building current state//
        ///////////////////////////
        brain.factList = new List<iThinkFact>();
        brain.factList.Add(new iThinkFact("onTable", GameObject.Find("A")));
        brain.factList.Add(new iThinkFact("onTable", GameObject.Find("B")));
        brain.factList.Add(new iThinkFact("on", GameObject.Find("E"), GameObject.Find("A")));
        brain.factList.Add(new iThinkFact("on", GameObject.Find("D"), GameObject.Find("E")));
        brain.factList.Add(new iThinkFact("on", GameObject.Find("F"), GameObject.Find("B")));
        brain.factList.Add(new iThinkFact("on", GameObject.Find("C"), GameObject.Find("F")));
        brain.factList.Add(new iThinkFact("clear", GameObject.Find("C")));
        brain.factList.Add(new iThinkFact("clear", GameObject.Find("D")));
        brain.factList.Add(new iThinkFact("gripEmpty"));
        brain.startState = new iThinkState("Initial", new List<iThinkFact>(brain.factList));

        /////////////////
        // Create goal //
        /////////////////
        brain.factList.Clear();
        brain.factList.Add(new iThinkFact("on", GameObject.Find("A"), GameObject.Find("B")));
        brain.factList.Add(new iThinkFact("on", GameObject.Find("B"), GameObject.Find("C")));
        brain.factList.Add(new iThinkFact("on", GameObject.Find("D"), GameObject.Find("E")));
        brain.factList.Add(new iThinkFact("on", GameObject.Find("E"), GameObject.Find("F")));

        brain.goalState = new iThinkState("Goal", new List<iThinkFact>(brain.factList));
    }
    void Gripper7()
    {
        ///////////////////////////
        // Building current state//
        ///////////////////////////
        brain.factList = new List<iThinkFact>();
        brain.factList.Add(new iThinkFact("onTable", GameObject.Find("E")));
        brain.factList.Add(new iThinkFact("onTable", GameObject.Find("D")));
        brain.factList.Add(new iThinkFact("onTable", GameObject.Find("C")));
        brain.factList.Add(new iThinkFact("onTable", GameObject.Find("F")));
        brain.factList.Add(new iThinkFact("on", GameObject.Find("A"), GameObject.Find("E")));
        brain.factList.Add(new iThinkFact("on", GameObject.Find("B"), GameObject.Find("D")));
        brain.factList.Add(new iThinkFact("on", GameObject.Find("G"), GameObject.Find("F")));
        brain.factList.Add(new iThinkFact("clear", GameObject.Find("A")));
        brain.factList.Add(new iThinkFact("clear", GameObject.Find("B")));
        brain.factList.Add(new iThinkFact("clear", GameObject.Find("C")));
        brain.factList.Add(new iThinkFact("clear", GameObject.Find("G")));
        brain.factList.Add(new iThinkFact("gripEmpty"));
        brain.startState = new iThinkState("Initial", new List<iThinkFact>(brain.factList));

        /////////////////
        // Create goal //
        /////////////////
        brain.factList.Clear();
        brain.factList.Add(new iThinkFact("on", GameObject.Find("E"), GameObject.Find("A")));
        brain.factList.Add(new iThinkFact("on", GameObject.Find("D"), GameObject.Find("F")));
        brain.factList.Add(new iThinkFact("on", GameObject.Find("C"), GameObject.Find("B")));
        brain.factList.Add(new iThinkFact("on", GameObject.Find("F"), GameObject.Find("E")));
        brain.factList.Add(new iThinkFact("holding", GameObject.Find("G")));

        brain.goalState = new iThinkState("Goal", new List<iThinkFact>(brain.factList));
    }
}
