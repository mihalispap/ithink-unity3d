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
    protected List<iThinkAction> Plan;

    public iThinkPlan() { Plan = new List<iThinkAction>(); }

    public List<iThinkAction> getPlan() { return Plan; }

    public bool hasPlan() { return Plan.Count != 0; }

    public void setPlan( iThinkPlanStep _step ) { Plan = new List<iThinkAction>( _step.getActions() ); }

    public void printPlan()
    {
        foreach ( iThinkAction action in Plan )
            DebugConsole.Log( "Action : " + action.getName() + "-" + action.getArg1().name + "-" + action.getArg2().name, Color.white );
    }
}
