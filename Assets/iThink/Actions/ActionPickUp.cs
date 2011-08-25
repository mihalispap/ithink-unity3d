using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPickUp : iThinkAction
{
    GameObject Who, What;
    public ActionPickUp( string name, GameObject who, GameObject what )
        : base( name )
    {
        Who = who;
        What = what;
    }

    public override void setArg1( GameObject who )
    {
        Who = who;
    }

    public override void setArg2( GameObject what )
    {
        What = what;
    }


    public override GameObject getArg1()
    {
        return Who;
    }

    public override GameObject getArg2()
    {
        return What;
    }

    public override void fixPreconditions( GameObject location )
    {
        base.fixPreconditions();
        //preConditions.Add( new isAt( Who, location ) );
        preConditions.Add( new hasFreeHands() );
    }

    public override void fixEffects( GameObject obj )
    {
        base.fixEffects();
        effects.Add( new isHolding( obj, true ) );
        effects.Add( new hasFreeHands( false ) );
    }
}
