///
/// <summary>
///
/// iThink GOAP library v0.0.8b
///     iThinkState.cs
///
/// Description of file:
///     This class describes the list of facts (logical propositions)
///     that constitute the knowledge of agents about the world.
///     These states are used in the planning process to describe a node of the planning graph (search space).
///
/// </summary>
/// 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using iThink;

#pragma warning disable 0660, 0661

public class iThinkState
{
    protected string name;
    protected List<iThinkFact> facts;

    public iThinkState( iThinkState state )
    {
        facts = new List<iThinkFact>( state.facts );
        name = state.name;
    }

    public iThinkState( string Name, List<iThinkFact> factList )
    {
        facts = new List<iThinkFact>( factList );
        name = Name;
    }

    public string getName() { return name; }
    public List<iThinkFact> getFactList() { return facts; }

    public iThinkFact getFact( string name )
    {
        foreach ( iThinkFact fact in facts )
        {
            if ( fact.getName().Equals( name ) )
                return fact;
        }
        return null;
    }

    public void setFact( iThinkFact fact )
    {
        foreach ( iThinkFact _fact in this.facts )
        {
            if ( _fact == fact )
                return;
        }
        facts.Add( fact );
        //DebugConsole.Log( "Added fact - " + fact.getName() + " (" + fact.getObj1().name + "-" + fact.getObj2().name + ")", Color.gray );
    }

    public void delFact( iThinkFact fact )
    {
        foreach ( iThinkFact _fact in this.facts )
        {
            if ( _fact == fact )
            {
                facts.Remove( _fact );
                break;
            }
        }
        //DebugConsole.Log( "Removed fact - " + fact.getName() + " (" + fact.getObj1().name + "-" + fact.getObj2().name + ")", Color.gray );
    }

    public void debugPrint( string msg )
    {
        DebugConsole.Log( msg + "\n-= State =-", Color.cyan );
        foreach ( iThinkFact fact in facts )
        {
            if ( fact.getObj2() != null )
                DebugConsole.Log( "Fact: " + fact.getName() + "( " + fact.getObj1().name + ", " + fact.getObj2().name + " )" );
            else if ( fact.getObj1() != null )
                DebugConsole.Log( "Fact: " + fact.getName() + "( " + fact.getObj1().name + " )" );
            else
                DebugConsole.Log( "Fact: " + fact.getName() );
        }
    }

    public void debugPrint( string msg, Color colour )
    {
        DebugConsole.Log( msg + "\n-= State =-", colour );
        foreach ( iThinkFact fact in facts )
        {
            if ( fact.getObj2() != null )
                DebugConsole.Log( "Fact: " + fact.getName() + "( " + fact.getObj1().name + ", " + fact.getObj2().name + " )", colour );
            else if ( fact.getObj1() != null )
                DebugConsole.Log( "Fact: " + fact.getName() + "( " + fact.getObj1().name + " )", colour );
            else
                DebugConsole.Log( "Fact: " + fact.getName(), colour );
        }
    }

    public static bool operator ==( iThinkState state1, iThinkState state2 )
    {
        if ( System.Object.ReferenceEquals( state1, state2 ) )
        {
            return true;
        }

        if ( (object)state1 == null || (object)state2 == null )
        {
            return false;
        }

        foreach ( iThinkFact fact1 in state1.facts )
        {
            bool check = false;

            foreach ( iThinkFact fact2 in state2.facts )
            {
                if ( fact1 == fact2 )
                    check = true;
            }

            if ( check == false )
                return false;
        }

        return true;
    }

    public static bool operator !=( iThinkState state1, iThinkState state2 )
    {
        return !( state1 == state2 );
    }
}