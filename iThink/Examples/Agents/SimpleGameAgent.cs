using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGameAgent : MonoBehaviour
{
    iThinkBrain brain;

    public string[] schemaList = {
                                     "ActionSGMove-3-Tag~location-Tag~location-Tag~direction",
                                     "ActionSGTurn-2-Tag~direction-Tag~direction",
                                     "ActionSGShoot-4-Tag~location-Tag~location-Tag~direction-Tag~gun",
                                     "ActionSGStab-2-Tag~location-Tag~knife",
                                     "ActionSGPickUp-2-Tag~knife-Tag~location",
                                     "ActionSGPickUp-2-Tag~gun-Tag~location"
                                 };

    public void Awake()
    {
        brain = new iThinkBrain();

        // Sensing GameObjects
        List<String> tags = new List<String>();
        tags.Add( "direction" ); tags.Add( "location" ); tags.Add( "player" ); tags.Add( "npc" ); tags.Add( "gun" ); tags.Add( "knife" );
        brain.sensorySystem.OmniscientUpdate( this.gameObject, tags );

        Debug.LogWarning( "knownObjects count: " + brain.sensorySystem.getKnownObjects().Count );

        // Building knowledge
        SimplegameTest();
        brain.curState = brain.startState;

        // Building Actions from knowledge
        brain.ActionManager = new iThinkActionManager();
        brain.ActionManager.initActionList( this.gameObject, schemaList, brain.getKnownObjects(), brain.getKnownFacts() );

        Debug.LogWarning( "action count: " + brain.ActionManager.getActions().Count );

        // Init search        
        brain.planner.forwardSearch( brain.startState, brain.goalState, brain.ActionManager, 1 );
        brain.planner.getPlan().debugPrintPlan();
    }

    void SimplegameTest()
    {
        List<iThinkFact> factList;

        ///////////////////////////
        // Building current state//
        ///////////////////////////
        factList = new List<iThinkFact>();
        factList.Add( new iThinkFact( "npcAt", GameObject.Find( "LOC1" ) ) );
        factList.Add( new iThinkFact( "npcFacing", GameObject.Find( "UP" ) ) );
        factList.Add( new iThinkFact( "npcEmptyHands" ) );
        factList.Add( new iThinkFact( "playerAt", GameObject.Find( "LOC8" ) ) );
        factList.Add( new iThinkFact( "knife", GameObject.Find( "KNIFE" ) ) );
        factList.Add( new iThinkFact( "gun", GameObject.Find( "GUN" ) ) );
        factList.Add( new iThinkFact( "objectAt", GameObject.Find( "KNIFE" ), GameObject.Find( "LOC3" ) ) );
        factList.Add( new iThinkFact( "objectAt", GameObject.Find( "GUN" ), GameObject.Find( "LOC6" ) ) );

        initAdjacents( factList );
        brain.startState = new iThinkState( "Initial", new List<iThinkFact>( factList ) );

        /////////////////
        // Create goal //
        /////////////////
        factList.Clear();
        //brain.factList.Add( new iThinkFact( "npcHolding", GameObject.Find( "GUN" ) ) );
        factList.Add( new iThinkFact( "playerDown" ) );

        brain.goalState = new iThinkState( "Goal", new List<iThinkFact>( factList ) );
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
