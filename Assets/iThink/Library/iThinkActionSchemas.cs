///
/// <summary>
/// iThink GOAP library v0.0.8e
///     iThinkActionSet.cs
///
/// Description of file:
///     The ActionSetSchema provides functionality for automatic creation of actions according to
///     the knowledge of the agent.
///
/// </summary>
/// 

using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unify
{
    public GameObject obj1 { get; set; }
    public GameObject obj2 { get; set; }
    public GameObject obj3 { get; set; }
    public GameObject obj4 { get; set; }
    public GameObject important { get; set; }
};

public class iThinkActionSchemas
{
    GameObject agent;
    List<GameObject> someParts;
    List<GameObject> workingParts;
    List<unify> pairs;
    //List<unify> moveTrips;
    //List<unify> turnPairs;
    //List<unify> pickPairs;
    //List<unify> shootQuads;
    //List<unify> stabPairs;

    string[] actionElements;

    public iThinkActionSchemas() { }

    public List<GameObject> splitter( List<GameObject> GameParts, string action )
    {
        someParts = new List<GameObject>();
        actionElements = action.Split( '-' );

        someParts = GameParts.FindAll( delegate( GameObject obj1 )
        {
            if ( obj1.tag.ToString().Equals( actionElements[2] ) )
                return true;
            return false;
        } );

        workingParts = GameParts;
        return someParts;
    }

    public List<unify> unifier( List<GameObject> GameParts, GameObject agent )
    {
        pairs = new List<unify>();
        switch ( actionElements[0] )
        {
            case "ActionSGMove":
                break;
            case "ActionSGTurn":
                break;
            case "ActionSGPickUp":
                break;
            case "ActionSGStab":
                break;
            case "ActionSGShoot":
                break;
            default:
                Debug.LogError( "Bad action unification" );
                break;
        }
        return pairs;
    }

    public List<iThinkAction> actionCreator( List<unify> pairs, List<GameObject> GameParts, GameObject agent )
    {
        List<iThinkAction> actionList = new List<iThinkAction>();
        iThinkAction action;
        GameObject loc;
        foreach ( unify pair in pairs )
        {
            switch ( actionElements[0] )
            {
                case "ActionSGMove":
                    action = new ActionMoveTo( "Move", pair.obj1, pair.obj2 );
                    actionList.Add( action );
                    break;
                case "ActionSGTurn":
                    action = new ActionPickUp( "Turn", pair.obj1, pair.obj2 );
                    actionList.Add( action );
                    break;
                case "ActionSGPickUp":
                    action = new ActionPickUp( "PickUp", pair.obj1, pair.obj2 );
                    actionList.Add( action );
                    break;
                case "ActionSGStab":
                    action = new ActionPickUp( "Stab", pair.obj1, pair.obj2 );
                    actionList.Add( action );
                    break;
                case "ActionSGShoot":
                    action = new ActionPickUp( "Shoot", pair.obj1, pair.obj2 );
                    actionList.Add( action );
                    break;

                /* THIS IS FOR THE LOCS&KNIFE DEMOS
                case "ActionMoveTo":
                    action = new ActionMoveTo( "ActionMoveTo", pair.obj1, pair.obj2 );
                    action.fixPreconditions( agent );
                    action.fixEffects( agent );
                    actionList.Add( action );
                    break;
                case "ActionPickUp":
                    action = new ActionPickUp( "ActionPickUp", pair.obj1, pair.obj2 );
                    loc = findImportantElement( action );
                    action.fixPreconditions( action.getArg2() );
                    action.fixEffects( loc );
                    actionList.Add( action );
                    break;
                case "ActionDropDown":
                    action = new ActionDropDown( "ActionDropDown", pair.obj1, pair.obj2 );
                    loc = findImportantElement( action );
                    action.fixPreconditions( loc );
                    action.fixEffects( action.getArg2() );
                    actionList.Add( action );
                    break;
                */
            }
        }
        return actionList;
    }

    /* THIS IS FOR THE LOCS&KNIFE DEMOS
    GameObject findImportantElement( iThinkAction action )
    {
        GameObject loc = workingParts.Find( delegate( GameObject obj1 )
         {
             //Debug.Log( "obj1.name: " + obj1.name );
             //Debug.Log( "action.getArg2().name: " + action.getArg2().name );
             //Debug.Log( "-------------------");
             if ( obj1.name == action.getArg2().name )
                 return true;
             return false;
         } );
        //Debug.Log( loc );
        return loc;
    }

    List<unify> fixMovements( List<GameObject> Locations )
    {
        List<unify> pairs = new List<unify>();
        for ( int i = 0 ; i < Locations.Count ; i++ )
        {
            unify pair = new unify();
            if ( i == Locations.Count - 1 )
            {
                pair.obj1 = Locations[i];
                pair.obj2 = Locations[0];
            }
            else
            {
                pair.obj1 = Locations[i];
                pair.obj2 = Locations[i + 1];
            }
            pairs.Add( pair );
        }
        return pairs;
    }

    List<unify> fixActionPairs( List<GameObject> GameParts, GameObject agent )
    {
        List<unify> pairs = new List<unify>();
        foreach ( GameObject part in GameParts )
        {
            unify pair = new unify();
            pair.obj1 = agent;
            pair.obj2 = part;
            pairs.Add( pair );
        }
        return pairs;
    }
    */
}
