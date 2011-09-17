using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGameAgent : MonoBehaviour
{
    iThinkBrain brain;

    public void Awake()
    {
        brain = new iThinkBrain();

        // Sensing GameObjects
        List<String> tags = new List<String>();
        tags.Add( "direction" ); tags.Add( "location" ); tags.Add( "player" ); tags.Add( "npc" ); tags.Add( "gun" ); tags.Add( "knife" );
        brain.sensorySystem.OmniscientUpdate( this.gameObject, brain.gameParts, tags );

        Debug.LogWarning( "gameParts count: " + brain.gameParts.Count );

        // Building knowledge
        SimplegameTest();
        brain.curState = brain.startState;

        // Building Actions from knowledge
        brain.actionSet = new iThinkActionSet( brain.getKnownFacts() );
        brain.actionSet.Init( this.gameObject, brain.getKnownObjects() );

        Debug.LogWarning( "action count: " + brain.actionSet.getActions().Count );

        // Init search        
        brain.planner.forwardSearch( brain.startState, brain.goalState, brain.actionSet, 1 );
        brain.planner.getPlan().printPlan();
    }

    void SimplegameTest()
    {
        ///////////////////////////
        // Building current state//
        ///////////////////////////
        brain.factList = new List<iThinkFact>();
        brain.factList.Add( new iThinkFact( "npcAt", GameObject.Find( "LOC1" ) ) );
        brain.factList.Add( new iThinkFact( "npcFacing", GameObject.Find( "UP" ) ) );
        brain.factList.Add( new iThinkFact( "npcEmptyHands" ) );
        brain.factList.Add( new iThinkFact( "playerAt", GameObject.Find( "LOC8" ) ) );
        brain.factList.Add( new iThinkFact( "knife", GameObject.Find( "KNIFE" ) ) );
        brain.factList.Add( new iThinkFact( "gun", GameObject.Find( "GUN" ) ) );
        brain.factList.Add( new iThinkFact( "objectAt", GameObject.Find( "KNIFE" ), GameObject.Find( "LOC3" ) ) );
        brain.factList.Add( new iThinkFact( "objectAt", GameObject.Find( "GUN" ), GameObject.Find( "LOC6" ) ) );

        initAdjacents( brain.factList );
        brain.startState = new iThinkState( "Initial", new List<iThinkFact>( brain.factList ) );

        /////////////////
        // Create goal //
        /////////////////
        brain.factList.Clear();
        //brain.factList.Add( new iThinkFact( "npcHolding", GameObject.Find( "GUN" ) ) );
        brain.factList.Add( new iThinkFact( "playerDown" ) );

        brain.goalState = new iThinkState( "Goal", new List<iThinkFact>( brain.factList ) );
    }

    public void initAdjacents( List<iThinkFact> factList )
    {
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC1" ), GameObject.Find( "LOC4" ), GameObject.Find( "RIGHT" ) ) );
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC1" ), GameObject.Find( "LOC2" ), GameObject.Find( "UP" ) ) );
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC2" ), GameObject.Find( "LOC5" ), GameObject.Find( "RIGHT" ) ) );
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC2" ), GameObject.Find( "LOC1" ), GameObject.Find( "DOWN" ) ) );
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC2" ), GameObject.Find( "LOC3" ), GameObject.Find( "UP" ) ) );
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC3" ), GameObject.Find( "LOC6" ), GameObject.Find( "RIGHT" ) ) );
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC3" ), GameObject.Find( "LOC2" ), GameObject.Find( "DOWN" ) ) );

        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC4" ), GameObject.Find( "LOC7" ), GameObject.Find( "RIGHT" ) ) );
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC4" ), GameObject.Find( "LOC5" ), GameObject.Find( "UP" ) ) );
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC4" ), GameObject.Find( "LOC1" ), GameObject.Find( "LEFT" ) ) );
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC5" ), GameObject.Find( "LOC8" ), GameObject.Find( "RIGHT" ) ) );
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC5" ), GameObject.Find( "LOC6" ), GameObject.Find( "UP" ) ) );
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC5" ), GameObject.Find( "LOC2" ), GameObject.Find( "LEFT" ) ) );
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC5" ), GameObject.Find( "LOC4" ), GameObject.Find( "DOWN" ) ) );
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC6" ), GameObject.Find( "LOC9" ), GameObject.Find( "RIGHT" ) ) );
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC6" ), GameObject.Find( "LOC5" ), GameObject.Find( "DOWN" ) ) );
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC6" ), GameObject.Find( "LOC3" ), GameObject.Find( "LEFT" ) ) );

        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC7" ), GameObject.Find( "LOC4" ), GameObject.Find( "LEFT" ) ) );
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC7" ), GameObject.Find( "LOC8" ), GameObject.Find( "UP" ) ) );
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC8" ), GameObject.Find( "LOC5" ), GameObject.Find( "LEFT" ) ) );
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC8" ), GameObject.Find( "LOC7" ), GameObject.Find( "DOWN" ) ) );
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC8" ), GameObject.Find( "LOC9" ), GameObject.Find( "UP" ) ) );
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC9" ), GameObject.Find( "LOC6" ), GameObject.Find( "LEFT" ) ) );
        factList.Add( new iThinkFact( "adjacent", GameObject.Find( "LOC9" ), GameObject.Find( "LOC8" ), GameObject.Find( "DOWN" ) ) );
    }
}
