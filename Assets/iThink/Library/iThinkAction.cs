/// 
/// <summary>
/// iThink GOAP library v0.0.8b
///     iThinkAction.cs
///
/// Description of file:
///     Describes a base (abstract) Action and provides useful methods concerning
///     its status, initialization and functionality
///
/// </summary>
/// 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using iThink;

public class iThinkAction
{
    protected string name;
    protected List<iThinkFact> preConditions;
    protected List<iThinkFact> effects;

    public iThinkAction( string name )
    {
        this.name = name;
    }

    public string getName() { return name; }

    public List<iThinkFact> getEffects()
    {
        return effects;
    }

    public void setPreconditions()
    {
        preConditions = new List<iThinkFact>();
    }

    public void setEffects()
    {
        effects = new List<iThinkFact>();
    }

    public iThinkState applyEffects( iThinkState State )
    {
        iThinkState NewState = new iThinkState( State );

        foreach ( iThinkFact effect in this.effects )
        {
            effect.applyFact( NewState );
        }

        return NewState;
    }

    public bool validate( iThinkState curState )
    {
        int counter = 0;
        foreach ( iThinkFact fact in preConditions )
        {
            if ( fact.getObj2() != null )
                DebugConsole.Log( "[validate (" + name + ")] Validating fact: " + fact.getName() + "( " + fact.getObj1().name + ", " + fact.getObj2().name + " )", Color.yellow );
            else if ( fact.getObj1() != null )
                DebugConsole.Log( "[validate (" + name + ")] Validating fact: " + fact.getName() + "( " + fact.getObj1().name + " )", Color.yellow );
            else
                DebugConsole.Log( "[validate (" + name + ")] Validating fact: " + fact.getName(), Color.yellow );
            
            // TODO: Get facts of wanted type/name only
            foreach ( iThinkFact checkFact in curState.getFactList() )
            {
                if ( checkFact.getObj2() != null )
                    DebugConsole.Log( "[validate (" + name + ")] Currently checking fact:: " + checkFact.getName() + "( " + checkFact.getObj1().name + ", " + checkFact.getObj2().name + " )", Color.yellow );
                else if ( checkFact.getObj1() != null )
                    DebugConsole.Log( "[validate (" + name + ")] Currently checking fact:: " + checkFact.getName() + "( " + checkFact.getObj1().name + " )", Color.yellow );
                else
                    DebugConsole.Log( "[validate (" + name + ")] Currently checking fact:: " + checkFact.getName(), Color.yellow );
                if ( fact == checkFact )
                {
                    DebugConsole.Log( "[validate (" + name + ")] Fact           found!", Color.green );
                    counter++;
                    break;
                }
                //else
                DebugConsole.Log( "[validate (" + name + ")] Fact   *NOT*   found!", Color.red );
            }
        }
        if ( counter == preConditions.Count )
            return true;
        return false;
    }

    public virtual void fixPreconditions()
    {
        preConditions = new List<iThinkFact>();
    }

    public virtual void fixEffects()
    {
        effects = new List<iThinkFact>();
    }

    public virtual void fixPreconditions( GameObject obj1 )
    {
        preConditions = new List<iThinkFact>();
    }

    public virtual void fixEffects( GameObject obj1 )
    {
        effects = new List<iThinkFact>();
    }

    public virtual void fixPreconditions( GameObject obj1, GameObject obj2 )
    {
        preConditions = new List<iThinkFact>();
    }

    public virtual void fixEffects( GameObject obj1, GameObject obj2 )
    {
        effects = new List<iThinkFact>();
    }

    public virtual void fixPreconditions( GameObject obj1, GameObject obj2, GameObject obj3 )
    {
        preConditions = new List<iThinkFact>();
    }

    public virtual void fixEffects( GameObject obj1, GameObject obj2, GameObject obj3 )
    {
        effects = new List<iThinkFact>();
    }

    public virtual void fixPreconditions( GameObject obj1, GameObject obj2, GameObject obj3, GameObject obj4 )
    {
        preConditions = new List<iThinkFact>();
    }

    public virtual void fixEffects( GameObject obj1, GameObject obj2, GameObject obj3, GameObject obj4 )
    {
        effects = new List<iThinkFact>();
    }

    public virtual GameObject getArg1() { return null; }
    public virtual GameObject getArg2() { return null; }
    public virtual GameObject getArg3() { return null; }
    public virtual GameObject getArg4() { return null; }
    public virtual void setArg1( GameObject from ) { }
    public virtual void setArg2( GameObject from ) { }
    public virtual void setArg3( GameObject from ) { }
    public virtual void setArg4( GameObject from ) { }
}
