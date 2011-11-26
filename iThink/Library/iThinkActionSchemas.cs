using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class iThinkActionSchemas
 *  @brief Parses action schemas and generates actions.
 *  
 *  Each agent describes its available actions in the iThinkBrain::schemaList variable.
 *  Whenever it needs to plan, it generates its available actions via the iThinkManager,
 *  a process which is internally managed and completed by iThinkActionSchemas.
 */

public class iThinkActionSchemas
{
    /// Action generator
    /// @todo Implement lookup table or a design pattern for easier/automatic function generation
    public virtual void actionGenerator( List<GameObject[]> combinations )
    {
        iThinkAction action;
        foreach ( GameObject[] matrix in combinations )
        {
            switch ( schemaElements[0] )
            {
                /* SIMPLEFPS_LITE */
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

                /* BLOCKSWORLD */
                case "ActionBwPickUp":
                    action = new ActionBwPickUp( "PickUp", matrix[0] );
                    tempActionList.Add( action );
                    break;
                case "ActionBwUnStack":
                    action = new ActionBwUnStack( "UnStack", matrix[0], matrix[1] );
                    tempActionList.Add( action );
                    break;
                case "ActionBwPutDown":
                    action = new ActionBwPutDown( "PutDown", matrix[0] );
                    tempActionList.Add( action );
                    break;
                case "ActionBwStack":
                    action = new ActionBwStack( "Stack", matrix[0], matrix[1] );
                    tempActionList.Add( action );
                    break;
            }
        }
    }

    int globalCounter = 0;
    public Dictionary<int, List<GameObject>> objectCollector { get; set; }
    List<iThinkAction> tempActionList;
    List<GameObject[]> combinations;
    string[] schemaElements;
    string[] actionElements;

    public iThinkActionSchemas()
    {
        objectCollector = new Dictionary<int, List<GameObject>>();
        tempActionList = new List<iThinkAction>();
    }

    /** Parses the action schema string, searches for needed data, and creates a list applicable actions
     * @return A list of available/applicable actions.
     */
    public List<iThinkAction> generateActions( List<GameObject> knownObjects, List<iThinkFact> FactList, string schema, int method )
    {
        initData();
        schemaParser( schema );
        searchComponents( knownObjects, FactList );
        Unificator( method );
        actionGenerator( combinations );
        return tempActionList;
    }

    /// Splits the action schema string
    private void schemaParser( string schema )
    {
        schemaElements = schema.Split( '-' );
    }

    /// Searches for the needed information, specified in the action schema string.
    private void searchComponents( List<GameObject> knownObjects, List<iThinkFact> FactList )
    {
        for ( int i = 2 ; i < schemaElements.Length ; i++ )
        {
            actionElements = schemaElements[i].Split( '~' );
            switch ( actionElements[0] )
            {
                case "Tag":
                    searchknownObjects( knownObjects );
                    break;
                case "Fact":
                    searchFacts( FactList );
                    break;
                default:
                    break;
            }
        }
    }

    /// Searches for GameObjects of specified tag.
    private void searchknownObjects( List<GameObject> knownObjects )
    {
        List<GameObject> foundObjects = new List<GameObject>();
        foundObjects = knownObjects.FindAll( delegate( GameObject checkedObject )
        {
            if ( checkedObject.tag.ToString().Equals( actionElements[1] ) )
                return true;
            return false;
        } );

        objectCollector.Add( globalCounter, foundObjects );
        globalCounter++;
    }

    /// Searches for facts of specified name.
    private void searchFacts( List<iThinkFact> FactList )
    {
        List<iThinkFact> foundFacts = new List<iThinkFact>();
        List<GameObject> foundObjects = new List<GameObject>();
        foundFacts = FactList.FindAll( delegate( iThinkFact checkedObject )
        {
            if ( checkedObject.getName().Equals( actionElements[1] ) )
                return true;
            return false;
        } );

        foreach ( iThinkFact fact in foundFacts )
        {
            foundObjects.Add( fact.getObj( int.Parse( actionElements[2] ) - 1 ) );
        }

        objectCollector.Add( globalCounter, foundObjects );
        globalCounter++;
    }

    /// Executes unification by cartesian product (iterative or recursive).
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
        if ( objectCollector.Count > 0 )
        {
            var enumStack = new Stack<List<GameObject>.Enumerator>();
            var itemStack = new Stack<GameObject>();
            int index = objectCollector.Count - 1;
            var enumerator = objectCollector[index].GetEnumerator();
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
                        enumerator = objectCollector[--index].GetEnumerator();
                    }
                }
                else
                {
                    if ( ++index == objectCollector.Count )
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
        if ( objectCollector.Count > 0 )
        {
            CartesianRecursion( accum, new Stack<GameObject>(), objectCollector.Count - 1 );
        }
        return accum;
    }

    private void CartesianRecursion( List<GameObject[]> accum, Stack<GameObject> stack, int index )
    {
        foreach ( GameObject item in objectCollector[index] )
        {
            stack.Push( item );
            if ( index == 0 )
                accum.Add( stack.ToArray() );
            else
            {
                CartesianRecursion( accum, stack, index - 1 );
            }
            stack.Pop();
        }
    }

    /// Clears the object's data values.
    private void initData()
    {
        objectCollector.Clear();
        tempActionList.Clear();
        globalCounter = 0;
    }
}
