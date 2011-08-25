/// <summary>
///
/// iThink GOAP library v0.0.8c
///     iThinkPlanner.cs
///
/// Description of file:
///
/// </summary>

using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iThinkPlanner
{
    protected iThinkPlan Plan;
    protected List<iThinkPlanStep> _OpenStates;
    protected List<iThinkState> _VisitedStates;

    public iThinkPlanner()
    {
        Plan = new iThinkPlan();
        _OpenStates = new List<iThinkPlanStep>();
        _VisitedStates = new List<iThinkState>();
    }

    public iThinkPlan getPlan() { return Plan; }

    public bool forwardSearch( iThinkState InitialState, iThinkState GoalState, iThinkActionSet ActionSet )
    {
        return forwardSearch( InitialState, GoalState, ActionSet, 0 );
    }

    public bool forwardSearch( iThinkState InitialState, iThinkState GoalState, iThinkActionSet ActionSet, int Method )
    {
        int it = 1;
        iThinkPlan ReturnVal;

        _OpenStates.Clear();
        _VisitedStates.Clear();

        iThinkPlanStep step = new iThinkPlanStep( InitialState );
        _OpenStates.Add( step );
        _VisitedStates.Add( step.getState() );

        //foreach ( iThinkAction action in ActionSet.getActions() )
        //    DebugConsole.Log( "Possible action: " + action.getName() );

        switch ( Method )
        {
            case 0:
                ReturnVal = dfs( GoalState, ActionSet, _OpenStates, _VisitedStates, it );
                break;
            default:
                ReturnVal = new iThinkPlan();
                break;
        }

        if ( ReturnVal == null )
            return false;
        else if ( ReturnVal.hasPlan() )
            return true;
        return false;
    }

    public iThinkPlan dfs( iThinkState GoalState, iThinkActionSet ActionSet, List<iThinkPlanStep> OpenStates, List<iThinkState> VisitedStates, int it )
    {
        iThinkPlanStep curStep, nextStep;
        iThinkState CurrentState;

        DebugConsole.Log( "*DFS* Iteration: " + it, Color.magenta );
        DebugConsole.Log( "*DFS* OpenStates count: (" + OpenStates.Count + ")", Color.magenta );

        foreach ( iThinkPlanStep PlanStep in OpenStates )
        {
            if ( compareStates( PlanStep.getState(), GoalState ) )
            {
                DebugConsole.Log( "*ITHINK* I've found a Goal state!", Color.green );
                Plan.setPlan( PlanStep );
                return Plan;
            }
        }

        if ( OpenStates.Count == 0 )
        {
            DebugConsole.Log( "*ITHINK* OpenStates is empty and I couldn't find a plan.", Color.red );
            return null;
        }

        List<iThinkAction> applicableActions = new List<iThinkAction>();

        curStep = new iThinkPlanStep( OpenStates[0] );
        CurrentState = OpenStates[0].getState();
        OpenStates.RemoveAt( 0 );

        applicableActions = getApplicable( CurrentState, ActionSet.getActions() );

        DebugConsole.Log( "Applicable action count: " + applicableActions.Count, Color.yellow );
        //foreach ( iThinkAction action in applicableActions )
        //    DebugConsole.Log( "Applicable action: " + action.getName(), Color.yellow );

        CurrentState.debugPrint( "[CurrentState]", Color.green );

        foreach ( iThinkAction action in applicableActions )
        {
            bool found = false;

            // TODO: Add "statnode" for statistics retrieval
            nextStep = progress( curStep, action );

            nextStep.getState().debugPrint( "[NextStep after " + action.getName() + "( " + action.getArg1().name + ", " + action.getArg2().name + " )]" );

            foreach ( iThinkState state in VisitedStates )
            {
                if ( state == nextStep.getState() )
                {
                    found = true;
                    break;
                }
            }

            if ( found == false )
            {
                OpenStates.Add( nextStep );
                VisitedStates.Add( nextStep.getState() );
            }
        }

        return dfs( GoalState, ActionSet, OpenStates, VisitedStates, ++it );
    }

    public iThinkPlanStep progress( iThinkPlanStep Step, iThinkAction Action )
    {
        iThinkPlanStep NewStep = new iThinkPlanStep( Step );
        List<iThinkAction> curActions;

        curActions = NewStep.getActions();

        foreach ( iThinkAction _act in curActions )
            DebugConsole.Log( "TempPlan step: " + _act.getName() + "( " + _act.getArg1().name + ", " + _act.getArg2().name + " )", Color.grey );

        curActions.Add( Action );

        NewStep.setState( Action.applyEffects( Step.getState() ) );

        return NewStep;
    }

    public List<iThinkAction> getApplicable( iThinkState State, List<iThinkAction> Actions )
    {
        List<iThinkAction> ApplicableActions = new List<iThinkAction>();

        foreach ( iThinkAction action in Actions )
        {
            if ( action.validate( State ) )
            {
                DebugConsole.Log( "(" + action.getName() + "-" + action.getArg1().name + "-" + action.getArg2().name + ") *VALIDATED*", Color.green );
                ApplicableActions.Add( action );
            }
            else
                DebugConsole.Log( "[validate (" + action.getName() + "-" + action.getArg1().name + "-" + action.getArg2().name + ")] *NOT* Validated", Color.red );
        }

        return ApplicableActions;
    }

    // Checks if goalState is a subset of curState
    private bool compareStates( iThinkState curState, iThinkState goalState )
    {
        int counter = 0;
        foreach ( iThinkFact fact in goalState.getFactList() )
        {
            /*Debug.Log( "-=-=-=-=-=-=-" );

            foreach ( iThinkFact _fact in curState.getFactList() )
            {
                if ( _fact.getObj2() != null )
                    Debug.LogWarning( "Fact: " + _fact.getName() + "( " + _fact.getObj1().name + ", " + _fact.getObj2().name + " )" );
                else if ( _fact.getObj1() != null )
                    Debug.LogWarning( "Fact: " + _fact.getName() + "( " + _fact.getObj1().name + " )" );
                else
                    Debug.LogWarning( "Fact: " + _fact.getName() );
            }*/
            //Debug.Log( "--->" );
            foreach ( iThinkFact check in curState.getFactList() )
            {
                //Debug.Log( "Checking Goal for fact: " + fact.getName() + "-" + fact.getObj1() + "-" + fact.getObj2() );
                //Debug.Log( "and check is: " + check.getName() + "-" + check.getObj1() + "-" + check.getObj2() );

                if ( check == null )
                    return false;
                else
                {
                    if ( check == fact )
                        counter++;
                }
            }
        }
        if ( counter == goalState.getFactList().Count )
            return true;
        return false;
    }

    // Applies the effects of action on curState and returns a new State. curState doesn't change
    private iThinkState updateState( iThinkState curState, iThinkAction action )
    {
        foreach ( iThinkFact effect in action.getEffects() )
        {
            if ( effect.getPositive() == false )
            {
                foreach ( iThinkFact fact in curState.getFactList() )
                {
                    if ( fact == effect )
                    {
                        curState.getFactList().Remove( fact );
                        break;
                    }
                }
            }
            else
                curState.getFactList().Add( effect );
        }
        return curState;
    }
}