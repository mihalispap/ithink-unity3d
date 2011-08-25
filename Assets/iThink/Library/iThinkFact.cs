///
/// <summary>
///
/// iThink GOAP library v0.0.8
///     iThinkFact.cs
///
/// Description of file:
///     To iThinkFact perigrafei plhrws ena fact (to eidos ths gnwshs, ton typo tou, th timh tou klp) se koinh
///     For now, organwnei se koinh domh ena propvalue me to antistoixo proptype tou.
///
/// </summary>
/// 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using iThink;

#pragma warning disable 0660, 0661

public class iThinkFact
{
    protected string name;
    protected bool positive;

    public iThinkFact( string Name ) { name = Name; }
    public iThinkFact( string Name, bool pos ) { name = Name; positive = pos; }
    public string getName() { return name; }
    public bool getPositive() { return positive; }

    public virtual GameObject getObj1() { return null; }
    public virtual GameObject getObj2() { return null; }

    public void applyFact( iThinkState State )
    {
        if ( this.positive == false )
        {
            //DebugConsole.Log( "Negative fact - " + name + " (" + getObj1().name + "-" + getObj2().name + ")" );
            State.delFact( this );
        }
        else
        {
            //DebugConsole.Log( "Positive fact - " + name + " (" + getObj1().name + "-" + getObj2().name + ")" );
            State.setFact( this );
        }
    }

    public static bool operator ==( iThinkFact fact1, iThinkFact fact2 )
    {
        if ( System.Object.ReferenceEquals( fact1, fact2 ) )
        {
            return true;
        }

        if ( (object)fact1 == null || (object)fact2 == null )
        {
            return false;
        }

        return ( fact1.getName().Equals( fact2.getName() ) && fact1.getObj2() == fact2.getObj2() && fact1.getObj1() == fact2.getObj1() );
    }

    public static bool operator !=( iThinkFact fact1, iThinkFact fact2 )
    {
        return !( fact1 == fact2 );
    }
}