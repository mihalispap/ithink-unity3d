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
        iThinkPlan ReturnVal;

        _OpenStates.Clear();
        _VisitedStates.Clear();

        iThinkPlanStep step = new iThinkPlanStep( InitialState );
        _OpenStates.Add( step );
        _VisitedStates.Add( step.getState() );

        switch ( Method )
        {
            case 0:
                ReturnVal = dfs( GoalState, ActionSet, _OpenStates, _VisitedStates );
                break;
            case 1:
                ReturnVal = bestFs( GoalState, ActionSet, _OpenStates, _VisitedStates );
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

    //! DFS
    public iThinkPlan dfs( iThinkState GoalState, iThinkActionSet ActionSet, List<iThinkPlanStep> OpenStates, List<iThinkState> VisitedStates )
    {
        int it = 0;
        iThinkPlanStep curStep, nextStep;
        iThinkState CurrentState;

        while ( OpenStates.Count != 0 )
        {
            Debug.LogWarning( "Iteration #" + it );

            foreach ( iThinkPlanStep PlanStep in OpenStates )
            {
                if ( compareStates( PlanStep.getState(), GoalState ) )
                {
                    Debug.Log( "Found Plan (DFS)" );
                    Plan.setPlan( PlanStep );
                    return Plan;
                }
            }

            List<iThinkAction> applicableActions = new List<iThinkAction>();

            curStep = new iThinkPlanStep( OpenStates[0] );
            CurrentState = OpenStates[0].getState();
            OpenStates.RemoveAt( 0 );

            CurrentState.debugPrint();

            applicableActions = getApplicable( CurrentState, ActionSet.getActions() );

            foreach ( iThinkAction action in applicableActions )
            {
                if ( action.getName().Equals( "Stab" ) )
                    Debug.LogWarning( "APPLICABLE STAB ACTION!" );

                bool found = false;

                // TODO: Add "statnode" for statistics retrieval
                nextStep = progress( curStep, action );

                if ( compareStates( nextStep.getState(), GoalState ) )
                {
                    Debug.Log( "Found Plan (DFS)" );
                    Plan.setPlan( nextStep );
                    return Plan;
                }

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

            ++it;
        }
        Debug.Log( "Didn't find Plan (DFS)" );
        return null;
    }

    //! BestFs
    public iThinkPlan bestFs( iThinkState GoalState, iThinkActionSet ActionSet, List<iThinkPlanStep> OpenStates, List<iThinkState> VisitedStates )
    {
        int it = 0;
        iThinkPlanStep curStep, nextStep;
        iThinkState CurrentState;

        while ( OpenStates.Count != 0 )
        {
            Debug.LogWarning( "Iteration #" + it );

            List<iThinkAction> applicableActions = new List<iThinkAction>();
            List<iThinkPlanStep> stateList = new List<iThinkPlanStep>();

            curStep = new iThinkPlanStep( OpenStates[0] );
            CurrentState = OpenStates[0].getState();
            OpenStates.RemoveAt( 0 );

            if ( compareStates( CurrentState, GoalState ) )
            {
                Debug.Log( "Found Plan (BFS) *Before* foreach applicable" );
                Plan.setPlan( curStep );
                return Plan;
            }

            applicableActions = getApplicable( CurrentState, ActionSet.getActions() );

            foreach ( iThinkAction action in applicableActions )
            {
                if ( action.getName().Equals( "Stab" ) )
                    Debug.LogWarning( "APPLICABLE STAB ACTION!" );

                bool found = false;
                nextStep = progress( curStep, action );

                if ( compareStates( nextStep.getState(), GoalState ) )
                {
                    Debug.Log( "Found Plan (BFS) *After* foreach applicable" );
                    Plan.setPlan( nextStep );
                    return Plan;
                }

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
                    int Cost = hFunction( nextStep.getState(), GoalState );
                    nextStep.getState().setCost( Cost );
                    stateList.Add( nextStep );
                    VisitedStates.Add( nextStep.getState() );
                }

            }

            OpenStates.AddRange( stateList );
            OpenStates.Sort( delegate( iThinkPlanStep obj1, iThinkPlanStep obj2 )
                            {
                                if ( obj1.getState().getCost() == obj2.getState().getCost() )
                                    return 0;
                                else if ( obj1.getState().getCost() > obj2.getState().getCost() )
                                    return -1;
                                else
                                    return 1;
                            }
                           );
            ++it;
        }
        Debug.Log( "Didn't find plan (BFS)" );
        return null;
    }

    private int hFunction( iThinkState nextState, iThinkState GoalState )
    {
        int counter = 0;
        foreach ( iThinkFact fact in nextState.getFactList() )
        {
            foreach ( iThinkFact goalFact in GoalState.getFactList() )
            {
                if ( fact == goalFact )
                {
                    counter++;
                    break;
                }
            }
        }
        return counter;
    }

    //! Helper Functions
    public iThinkPlanStep progress( iThinkPlanStep Step, iThinkAction Action )
    {
        iThinkPlanStep NewStep = new iThinkPlanStep( Step );
        List<iThinkAction> curActions;

        curActions = NewStep.getActions();
        curActions.Add( Action );

        NewStep.setState( Action.applyEffects( Step.getState() ) );

        return NewStep;
    }

    public List<iThinkAction> getApplicable( iThinkState State, List<iThinkAction> Actions )
    {
        List<iThinkAction> ApplicableActions = new List<iThinkAction>();

        foreach ( iThinkAction action in Actions )
        {
            if ( action == null )
                break;

            if ( action.validate( State ) )
                ApplicableActions.Add( action );
        }

        return ApplicableActions;
    }

    // Checks if goalState is a subset of curState
    private bool compareStates( iThinkState curState, iThinkState goalState )
    {
        int counter = 0;
        foreach ( iThinkFact fact in goalState.getFactList() )
        {
            foreach ( iThinkFact check in curState.getFactList() )
            {
                if ( check == null )
                    return false;
                else if ( check == fact )
                    counter++;
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