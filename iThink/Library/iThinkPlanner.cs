using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! @class iThinkPlanner
 *  @brief iThinkPlanner performs the planning process.

 *  By supplying an initial state, a goal state, and an iThinkActionManager object, iThinkPlanner
 *  performs planning and searches for the first valid plan.
 */

public class iThinkPlanner
{
    protected iThinkPlan Plan;
    protected List<iThinkPlan> _OpenStates;
    protected List<iThinkState> _VisitedStates;

    public iThinkPlanner()
    {
        Plan = new iThinkPlan();
        _OpenStates = new List<iThinkPlan>();
        _VisitedStates = new List<iThinkState>();
    }

    public iThinkPlan getPlan() { return Plan; }

    /// Just an interface to call forwardSearch with BestFS search method
    public bool forwardSearch( iThinkState InitialState, iThinkState GoalState, iThinkActionManager ActionManager )
    {
        return forwardSearch( InitialState, GoalState, ActionManager, 1 );
    }

    /// The function performing planning using Forward Search and the specified \a method
    public bool forwardSearch( iThinkState InitialState, iThinkState GoalState, iThinkActionManager ActionManager, int Method )
    {
        iThinkPlan ReturnVal;

        _OpenStates.Clear();
        _VisitedStates.Clear();

        iThinkPlan step = new iThinkPlan( InitialState );
        _OpenStates.Add( step );
        _VisitedStates.Add( step.getState() );

        switch ( Method )
        {
            case 0:
                ReturnVal = depthFS( GoalState, ActionManager, _OpenStates, _VisitedStates );
                break;
            case 1:
                ReturnVal = bestFS( GoalState, ActionManager, _OpenStates, _VisitedStates );
                break;
            case 2:
                ReturnVal = breadthFS( GoalState, ActionManager, _OpenStates, _VisitedStates );
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

    /// Depth-First Search
    public iThinkPlan depthFS( iThinkState GoalState, iThinkActionManager ActionManager, List<iThinkPlan> OpenStates, List<iThinkState> VisitedStates )
    {
        int it = 0;
        iThinkPlan curStep, nextStep;
        iThinkState CurrentState;

        while ( OpenStates.Count != 0 )
        {
            //Debug.LogWarning( "Iteration #" + it );
            List<iThinkAction> applicableActions = new List<iThinkAction>();

            curStep = new iThinkPlan( OpenStates[0] );
            CurrentState = OpenStates[0].getState();
            OpenStates.RemoveAt( 0 );

            CurrentState.debugPrint();

            applicableActions = getApplicable( CurrentState, ActionManager.getActions() );

            foreach ( iThinkAction action in applicableActions )
            {
                bool found = false;

                // todo: Add "statnode" for statistics retrieval
                nextStep = progress( curStep, action );

                if ( compareStates( nextStep.getState(), GoalState ) )
                {
                    Debug.Log( "Found Plan (DepthFS)" );
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
                    OpenStates.Insert( 0, nextStep );
                    VisitedStates.Add( nextStep.getState() );
                }
            }

            ++it;
        }
        Debug.Log( "Didn't find Plan (DepthFS)" );
        return null;
    }

    /// Breadth-First Search
    public iThinkPlan breadthFS( iThinkState GoalState, iThinkActionManager ActionManager, List<iThinkPlan> OpenStates, List<iThinkState> VisitedStates )
    {
        int it = 0;
        iThinkPlan curStep, nextStep;
        iThinkState CurrentState;

        while ( OpenStates.Count != 0 )
        {
            //Debug.LogWarning( "Iteration #" + it );
            List<iThinkAction> applicableActions = new List<iThinkAction>();

            curStep = new iThinkPlan( OpenStates[0] );
            CurrentState = OpenStates[0].getState();
            OpenStates.RemoveAt( 0 );

            CurrentState.debugPrint();

            applicableActions = getApplicable( CurrentState, ActionManager.getActions() );

            foreach ( iThinkAction action in applicableActions )
            {
                bool found = false;

                // todo: Add "statnode" for statistics retrieval
                nextStep = progress( curStep, action );

                if ( compareStates( nextStep.getState(), GoalState ) )
                {
                    Debug.Log( "Found Plan (BreadthFS)" );
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
        Debug.Log( "Didn't find Plan (BreadthFS)" );
        return null;
    }

    /// Best-First Search, which uses a Manhattan-distance heuristic (number of not-yet satisfied goalstate facts)
    public iThinkPlan bestFS( iThinkState GoalState, iThinkActionManager ActionManager, List<iThinkPlan> OpenStates, List<iThinkState> VisitedStates )
    {
        int it = 0;
        iThinkPlan curStep, nextStep;
        iThinkState CurrentState;

        while ( OpenStates.Count != 0 )
        {
            //Debug.LogWarning( "Iteration #" + it );
            List<iThinkAction> applicableActions = new List<iThinkAction>();
            List<iThinkPlan> stateList = new List<iThinkPlan>();

            curStep = new iThinkPlan( OpenStates[0] );
            CurrentState = OpenStates[0].getState();
            OpenStates.RemoveAt( 0 );

            applicableActions = getApplicable( CurrentState, ActionManager.getActions() );

            foreach ( iThinkAction action in applicableActions )
            {
                bool found = false;
                // todo: Add "statnode" for statistics retrieval
                nextStep = progress( curStep, action );

                if ( compareStates( nextStep.getState(), GoalState ) )
                {
                    Debug.Log( "Found Plan (BestFS)" );
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
            OpenStates.Sort( delegate( iThinkPlan obj1, iThinkPlan obj2 )
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
        Debug.Log( "Didn't find plan (BestFS)" );
        return null;
    }
    /// Manhattan-distance heuristic computation for BestFS()
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

    /// Generates the next plan step after the application of \a Action's effects.
    public iThinkPlan progress( iThinkPlan Step, iThinkAction Action )
    {
        iThinkPlan NewStep = new iThinkPlan( Step );
        List<iThinkAction> curActions;

        curActions = NewStep.getPlanActions();
        curActions.Add( Action );

        NewStep.setState( Action.applyEffects( Step.getState() ) );

        return NewStep;
    }

    /// Finds all actions applicable to the current \a State from the collection of available \a Actions
    public List<iThinkAction> getApplicable( iThinkState State, List<iThinkAction> Actions )
    {
        List<iThinkAction> ApplicableActions = new List<iThinkAction>();

        foreach ( iThinkAction action in Actions )
        {
            if ( action == null )
                break;

            if ( action.isApplicable( State ) )
                ApplicableActions.Add( action );
        }

        return ApplicableActions;
    }

    /// Checks if goalState is a subset of curState
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
}