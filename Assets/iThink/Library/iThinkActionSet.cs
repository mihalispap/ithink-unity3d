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
    protected List<iThinkFact> factList;
    protected List<iThinkAction> actionList;
    public iThinkActionSchemas schemaManager;

    //! TODO:USER

    /* SIMPLEGAME */
    /**/
    public string[] actionSchemas = {  
                                        "ActionSGMove-3-Tag~location-Tag~location-Tag~direction",
                                        "ActionSGTurn-2-Tag~direction-Tag~direction",
                                        "ActionSGShoot-4-Tag~location-Tag~location-Tag~direction-Tag~gun",
                                        "ActionSGStab-2-Tag~location-Tag~knife",
                                        "ActionSGPickUp-2-Tag~knife-Tag~location",
                                        "ActionSGPickUp-2-Tag~gun-Tag~location"
                                    };
    /**/

    /* GRIPPER */
    /*
    public string[] actionSchemas = {  
                                        "ActionGrPickUp-1-Tag~block",
                                        "ActionGrUnStack-2-Tag~block-Tag~block",
                                        "ActionGrPutDown-1-Tag~block",
                                        "ActionGrStack-2-Tag~block-Tag~block",
                                    };
    */

    public List<iThinkAction> getActions() { return actionList; }

    public iThinkActionSet( List<iThinkFact> facts )
    {
        factList = new List<iThinkFact>( facts );
        actionList = new List<iThinkAction>();
        schemaManager = new iThinkActionSchemas();
    }

    public void Init( GameObject agent, List<GameObject> gameParts )
    {
        List<iThinkAction> tempActionList = new List<iThinkAction>();
        foreach ( string schema in actionSchemas )
        {
            tempActionList.Clear();
            tempActionList = schemaManager.generateActions( gameParts, factList, schema, 2 );
            actionList.AddRange( tempActionList );
        }
    }

}