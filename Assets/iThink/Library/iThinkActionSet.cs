///
/// <summary>
/// iThink GOAP library v0.0.8e
///     iThinkActionSet.cs
///
/// Description of file:
///     The ActionSet provides a list of actions that are available to use by the agent via iThinkBrain.
///
/// </summary>
/// 

using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Prepei na allaxoun kai ta actions!
public class iThinkActionSet
{
    protected List<iThinkAction> actionSetList;

    public List<unify> pairs;
    public iThinkActionSchemas schemasAlgo;

    /*public string[] actionsSchemas = {  "ActionMoveTo-2-location",
                                        "ActionPickUp-1-iThink",
                                        "ActionDropDown-1-iThink"
                                     };*/

    public string[] actionsSchemas = {  "ActionSGMove-3-location",
                                        "ActionSGTurn-2-iThink",
                                        "ActionSGPickUp-2-iThink",
                                        "ActionSGShoot-4-iThink",
                                        "ActionSGStab-2-iThink"
                                     };

    public List<iThinkAction> getActions() { return actionSetList; }
    /**/
    public void Init( GameObject agent, List<GameObject> gameParts )
    {
        List<iThinkAction> tempActionList = new List<iThinkAction>();
        actionSetList = new List<iThinkAction>();
        schemasAlgo = new iThinkActionSchemas();
        foreach ( string action in actionsSchemas )
        {
            //Debug.LogWarning( "Checking action: " + action );
            List<GameObject> someGameParts = schemasAlgo.splitter( gameParts, action );
            switch ( someGameParts.Count )
            {
                case 0:
                    break;
                default:
                    pairs = schemasAlgo.unifier( someGameParts, agent );
                    tempActionList = schemasAlgo.actionCreator( pairs, gameParts, agent );
                    break;
            }
            actionSetList.AddRange( tempActionList );
            tempActionList.Clear();
        }

        foreach ( iThinkAction action in actionSetList )
        {
            DebugConsole.Log( "<__________________ACTIONS_____________________>" );
            DebugConsole.Log( action.getName() + "( " + action.getArg1().name + ", " + action.getArg2().name + " )" );
            DebugConsole.Log( action.getEffects()[0].getName() + "<-->" + action.getEffects()[0].getObj2().name );
        }
    }

    /**/
    /*
        public void Init( GameObject agent, List<GameObject> gameParts )
        {
            actionSetList = new List<iThinkAction>();

            iThinkAction action;

            action = new ActionMoveTo( "ActionMoveTo", GameObject.Find( "LOC1" ), GameObject.Find( "LOC2" ) );
            action.fixEffects( agent );
            action.fixPreconditions( agent );
            actionSetList.Add( action );

            action = new ActionMoveTo( "ActionMoveTo", GameObject.Find( "LOC2" ), GameObject.Find( "LOC3" ) );
            action.fixEffects( agent );
            action.fixPreconditions( agent );
            actionSetList.Add( action );

            action = new ActionMoveTo( "ActionMoveTo", GameObject.Find( "LOC2" ), GameObject.Find( "LOC1" ) );
            action.fixEffects( agent );
            action.fixPreconditions( agent );
            actionSetList.Add( action );

            action = new ActionMoveTo( "ActionMoveTo", GameObject.Find( "LOC3" ), GameObject.Find( "LOC1" ) );
            action.fixEffects( agent );
            action.fixPreconditions( agent );
            actionSetList.Add( action );

            action = new ActionMoveTo( "ActionMoveTo", GameObject.Find( "LOC3" ), GameObject.Find( "LOC4" ) );
            action.fixEffects( agent );
            action.fixPreconditions( agent );
            actionSetList.Add( action );

            action = new ActionMoveTo( "ActionMoveTo", GameObject.Find( "LOC4" ), GameObject.Find( "LOC5" ) );
            action.fixEffects( agent );
            action.fixPreconditions( agent );
            actionSetList.Add( action );

            action = new ActionPickUp( "ActionPickUp", GameObject.Find( "NPC" ), GameObject.Find( "KNIFE" ) );
            action.fixEffects( GameObject.Find( "KNIFE" ) );
            action.fixPreconditions( GameObject.Find( "LOC4" ) );
            actionSetList.Add( action );
        }
    */
}