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

    public List<iThinkFact> getEffects() { return effects; }
    
    public virtual void initPreconditions() { preConditions = new List<iThinkFact>(); }
    
    public virtual void initEffects() { effects = new List<iThinkFact>(); }

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
            // TODO: Get facts of wanted type/name only
            foreach ( iThinkFact checkFact in curState.getFactList() )
            {
                if ( fact == checkFact )
                    counter++;
            }
        }
        if ( counter == preConditions.Count )
            return true;
        return false;
    }

}
