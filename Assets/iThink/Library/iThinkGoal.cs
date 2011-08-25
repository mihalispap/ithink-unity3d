///
/// <summary>
/// iThink GOAP library v0.0.5
///
/// Description of file:
///     TODO: Probably obsolete
///     
/// </summary>
/// 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class iThinkGoal
{
    protected string name;
    iThinkState goalState;

    public iThinkGoal(GameObject obj, string name)
    {
        this.name = name;
    }

    public void updateGoal(iThinkState state1)
    {
        goalState = state1;
    }

    public iThinkState getGoalState()
    {
        return goalState;
    }
}
