///
/// <summary>
///
/// iThink GOAP library v0.0.8a
///     iThinkBrain.cs
///
/// Description of file:
///     The iThink brain will contain all the planning-related systems and information.
///     - It's primary function is to produce a plan for the parent GameObject
///       according to its local knowledge (world state), available actions and goal state.
///
/// </summary>
/// 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iThinkBrain : MonoBehaviour
{
    protected List<GameObject> gameParts;
    protected List<iThinkFact> factList;

    protected iThinkSensorySystem sensorySystem = new iThinkSensorySystem();
    protected iThinkState curState;
    protected iThinkState goalState;
    protected iThinkActionSet actionSet;

    protected iThinkPlanner planner;

    public void Awake()
    {
        DebugConsole.IsOpen = true;
        planner = new iThinkPlanner();

        // Building knowledge about the world
        gameParts = new List<GameObject>();

        List<String> tags = new List<String>(); tags.Add( "iThink" ); tags.Add( "location" );
        sensorySystem.OmniscientUpdate( this.gameObject, gameParts, tags );

        // Building current state
        factList = new List<iThinkFact>();
        factList.Add( new isAt( this.gameObject, GameObject.Find( "LOC1" ) ) );
        factList.Add( new isAt( GameObject.Find( "KNIFE" ), GameObject.Find( "LOC5" ) ) );
        factList.Add( new hasFreeHands( true ) );
        curState = new iThinkState( "Initial", new List<iThinkFact>( factList ) );

        // Building Actions
        actionSet = new iThinkActionSet();
        actionSet.Init( this.gameObject, gameParts );

        // Create goal
        factList.Clear();
        factList.Add( new isAt( this.gameObject, GameObject.Find( "LOC5" ) ) );
        factList.Add( new isHolding( GameObject.Find( "KNIFE" ) ) );
        goalState = new iThinkState( "Goal", new List<iThinkFact>( factList ) );

        // Search
        planner.forwardSearch( curState, goalState, actionSet, 0 );

        // Show solution
        /*
        if ( planner.Solution == null )
        {
            DebugConsole.Log( "No solution :(" );
        }
        else
        {
            DebugConsole.Log( "Yaaaaaaaaaay, solution! ^_^" );
        }
        */

        planner.getPlan().printPlan();
    }
}
