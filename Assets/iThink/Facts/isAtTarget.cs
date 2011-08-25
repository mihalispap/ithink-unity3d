using UnityEngine;
using System.Collections;

#pragma warning disable 0660, 0661

public class isAtTarget : iThinkFact
{
    GameObject owner;
    bool atTarget;

    public isAtTarget( GameObject obj, bool flag )
        : base( "atTarget" )
    {
        owner = obj;
        atTarget = flag;
    }

    public GameObject getOwner() { return owner; }

    public bool getAtTarget() { return atTarget; }

    public static bool operator ==( isAtTarget fact1, isAtTarget fact2 )
    {
        if ( System.Object.ReferenceEquals( fact1, fact2 ) )
            return true;

        if ( fact1 == fact2 )
            return fact1.getAtTarget() == fact2.getAtTarget();
        return false;
    }

    public static bool operator !=( isAtTarget fact1, isAtTarget fact2 ) { return !( fact1 == fact2 ); }
}