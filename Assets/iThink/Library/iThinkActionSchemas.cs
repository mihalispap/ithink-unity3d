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

public class datakeeper
{
    public Dictionary<int, List<GameObject>> collection { get; set; }
};

public class iThinkActionSchemas
{
    string[] schemaElements;
    string[] actionElements;
    int globalCounter = 0;
    datakeeper objs;
    List<iThinkAction> tempActionList;
    List<GameObject[]> combinations;

    public iThinkActionSchemas()
    {
        objs = new datakeeper();
        objs.collection = new Dictionary<int, List<GameObject>>();
        tempActionList = new List<iThinkAction>();
    }

    public List<iThinkAction> generateActions( List<GameObject> GameParts, List<iThinkFact> FactList, string schema, int method )
    {
        clearData();
        schemaParser( schema );
        searchComponents( GameParts, FactList );
        Unificator( method );
        actionGenerator( combinations );
        return tempActionList;
    }

    private void schemaParser( string schema )
    {
        schemaElements = schema.Split( '-' );
    }

    private void searchComponents( List<GameObject> GameParts, List<iThinkFact> FactList )
    {
        for ( int i = 2 ; i < schemaElements.Length ; i++ )
        {
            actionElements = schemaElements[i].Split( '~' );
            switch ( actionElements[0] )
            {
                case "Tag":
                    searchGameParts( GameParts );
                    break;
                case "Fact":
                    searchFacts( FactList );
                    break;
                default:
                    break;
            }
        }
    }

    //Search for game Parts
    private void searchGameParts( List<GameObject> GameParts )
    {
        List<GameObject> someParts = new List<GameObject>();
        someParts = GameParts.FindAll( delegate( GameObject obj1 )
        {
            if ( obj1.tag.ToString().Equals( actionElements[1] ) )
                return true;
            return false;
        } );

        objs.collection.Add( globalCounter, someParts );
        globalCounter++;
    }

    private void searchFacts( List<iThinkFact> FactList )
    {
        List<iThinkFact> someFacts = new List<iThinkFact>();
        List<GameObject> someParts = new List<GameObject>();
        someFacts = FactList.FindAll( delegate( iThinkFact obj1 )
        {
            if ( obj1.getName().Equals( actionElements[1] ) )
                return true;
            return false;
        } );

        foreach ( iThinkFact fact in someFacts )
        {
            someParts.Add( fact.getObj( int.Parse( actionElements[2] ) -1 ) );
        }

        objs.collection.Add( globalCounter, someParts );
        globalCounter++;
    }

    //Cartesian products for unification
    private void Unificator( int method )
    {
        switch ( method )
        {
            case 1:
                combinations = CartesianProductIterative();
                break;
            case 2:
                combinations = CartesianProductRecursive();
                break;
        }
    }

    private List<GameObject[]> CartesianProductIterative()
    {
        var accum = new List<GameObject[]>();
        if ( objs.collection.Count > 0 )
        {
            var enumStack = new Stack<List<GameObject>.Enumerator>();
            var itemStack = new Stack<GameObject>();
            int index = objs.collection.Count - 1;
            var enumerator = objs.collection[index].GetEnumerator();
            while ( true )
                if ( enumerator.MoveNext() )
                {
                    itemStack.Push( enumerator.Current );
                    if ( index == 0 )
                    {
                        accum.Add( itemStack.ToArray() );
                        itemStack.Pop();
                    }
                    else
                    {
                        enumStack.Push( enumerator );
                        enumerator = objs.collection[--index].GetEnumerator();
                    }
                }
                else
                {
                    if ( ++index == objs.collection.Count )
                        break;
                    itemStack.Pop();
                    enumerator = enumStack.Pop();
                }
        }
        return accum;
    }

    private List<GameObject[]> CartesianProductRecursive()
    {
        List<GameObject[]> accum = new List<GameObject[]>();
        if ( objs.collection.Count > 0 )
        {
            CartesianRecurse( accum, new Stack<GameObject>(), objs.collection.Count - 1 );
        }
        return accum;
    }

    private void CartesianRecurse( List<GameObject[]> accum, Stack<GameObject> stack, int index )
    {
        foreach ( GameObject item in objs.collection[index] )
        {
            stack.Push( item );
            if ( index == 0 )
                accum.Add( stack.ToArray() );
            else
            {
                CartesianRecurse( accum, stack, index - 1 );
            }
            stack.Pop();
        }
    }

    //Action generator
    //! TODO:USER
    public virtual void actionGenerator( List<GameObject[]> combinations )
    {
        iThinkAction action;
        foreach ( GameObject[] matrix in combinations )
        {
            /* SIMPLEGAME */
            /**/
            switch ( schemaElements[0] )
            {
                case "ActionSGMove":
                    action = new ActionSGMove( "Move", matrix[0], matrix[1], matrix[2] );
                    tempActionList.Add( action );
                    break;
                case "ActionSGTurn":
                    action = new ActionSGTurn( "Turn", matrix[0], matrix[1] );
                    tempActionList.Add( action );
                    break;
                case "ActionSGStab":
                    action = new ActionSGStab( "Stab", matrix[0], matrix[1] );
                    tempActionList.Add( action );
                    break;
                case "ActionSGShoot":
                    action = new ActionSGShoot( "Shoot", matrix[0], matrix[1], matrix[2], matrix[3] );
                    tempActionList.Add( action );
                    break;
                case "ActionSGPickUp":
                    action = new ActionSGPickUp( "PickUp", matrix[0], matrix[1] );
                    tempActionList.Add( action );
                    break;
            }
            /**/

            /* GRIPPER
            /*
            switch ( schemaElements[0] )
            {
                case "ActionGrPickUp":
                    action = new ActionGrPickUp( "PickUp", matrix[0] );
                    tempActionList.Add( action );
                    break;
                case "ActionGrUnStack":
                    action = new ActionGrUnStack( "UnStack", matrix[0], matrix[1] );
                    tempActionList.Add( action );
                    break;
                case "ActionGrPutDown":
                    action = new ActionGrPutDown( "PutDown", matrix[0] );
                    tempActionList.Add( action );
                    break;
                case "ActionGrStack":
                    action = new ActionGrStack( "Stack", matrix[0], matrix[1] );
                    tempActionList.Add( action );
                    break;
            }
            */
        }
    }

    //Clear data
    private void clearData()
    {
        objs.collection.Clear();
        tempActionList.Clear();
        globalCounter = 0;
    }
}
