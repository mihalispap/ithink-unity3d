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
    protected List<GameObject> objects;
    protected string name;
    protected bool positive;

    public iThinkFact( string Name, params GameObject[] objs )
    {
        name = Name;
        positive = true;
        objects = new List<GameObject>();

        for ( int i = 0 ; i < objs.Length ; i++ )
            addObj( objs[i] );
    }

    public iThinkFact( string Name, bool pos, params GameObject[] objs )
    {
        name = Name;
        positive = pos;
        objects = new List<GameObject>();

        for ( int i = 0 ; i < objs.Length ; i++ )
            addObj( objs[i] );
    }

    public string getName() { return name; }
    public bool getPositive() { return positive; }

    public void addObj( GameObject obj ) { objects.Add( obj ); }
    public GameObject getObj( int index ) { return objects[index]; }
    public int getObjCount() { return objects.Count; }

    public void applyFact( iThinkState State )
    {
        if ( this.positive == false )
            State.delFact( this );
        else
            State.setFact( this );
    }

    public static bool operator ==( iThinkFact fact1, iThinkFact fact2 )
    {
        if ( System.Object.ReferenceEquals( fact1, fact2 ) )
            return true;

        if ( (object)fact1 == null || (object)fact2 == null )
            return false;

        if ( !fact1.getName().Equals( fact2.getName() ) )
            return false;

        if ( fact1.objects.Count != fact2.objects.Count )
            return false;

        for ( int i = 0 ; i < fact1.objects.Count ; i++ )
        {
            if ( fact1.getObj( i ) != fact2.getObj( i ) )
                return false;
        }

        return true;
    }

    public static bool operator !=( iThinkFact fact1, iThinkFact fact2 )
    {
        return !( fact1 == fact2 );
    }
}