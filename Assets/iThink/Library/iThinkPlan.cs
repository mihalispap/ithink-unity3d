///
/// <summary>
///
/// iThink GOAP library v0.0.8c
///     iThinkPlan.cs
///
/// Description of file:
///     TODO
///
/// </summary>
/// 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iThinkPlan
{
    protected List<iThinkAction> actions;

    public iThinkPlan() { actions = new List<iThinkAction>(); }

    public List<iThinkAction> getActions() { return actions; }
    public int getActionCount()
    {
        if (actions == null)
            return 0;
        return actions.Count;
    }

    public bool hasPlan() { return actions.Count != 0; }

    public void setPlan(iThinkPlanStep _step) { actions = new List<iThinkAction>(_step.getActions()); }

    public void printPlan()
    {
        String msg = "[PrintPlan()]";
        foreach ( var action in actions )
        { msg += " -> " + action.getName(); }
        Debug.Log( msg );
    }
}
