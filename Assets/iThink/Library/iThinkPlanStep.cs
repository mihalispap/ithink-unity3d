///
/// <summary>
///
/// iThink GOAP library v0.0.8a
///     iThinkPlanStep.cs
///
/// Description of file:
///     A PlanStep describes the sequence of "Actions" needed to proceed to a "State"
///     When the State equals a goalstate, the Actions lists equals the Plan.
///
/// </summary>
/// 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iThinkPlanStep
{
    protected iThinkState State;
    protected List<iThinkAction> Actions;

    public iThinkPlanStep()
    {
        State = null;
        Actions = new List<iThinkAction>();
    }

    public iThinkPlanStep( iThinkState _State )
    {
        State = new iThinkState( _State );
        Actions = new List<iThinkAction>();
    }

    public iThinkPlanStep( iThinkState _State, List<iThinkAction> _Actions )
    {
        State = new iThinkState( _State );
        Actions = new List<iThinkAction>( _Actions );
    }

    public iThinkPlanStep( iThinkPlanStep Step )
    {
        State = new iThinkState( Step.State );
        Actions = new List<iThinkAction>( Step.Actions );
    }

    public iThinkState getState() { return State; }
    public void setState( iThinkState NewState ) { State = NewState; }
    public List<iThinkAction> getActions() { return Actions; }

    public void appendAction( iThinkAction action )
    {
        Actions.Add( action );
    }
}