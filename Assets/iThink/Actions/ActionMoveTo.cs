using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ActionMoveTo : iThinkAction
{
    GameObject From, To;
    public ActionMoveTo( string name, GameObject from, GameObject to )
        : base( name )
    {
        From = from;
        To = to;
    }

    public override void setArg1( GameObject from )
    {
        From = from;
    }

    public override void setArg2( GameObject to )
    {
        To = to;
    }

    public override GameObject getArg1()
    {
        return From;
    }

    public override GameObject getArg2()
    {
        return To;
    }

    public override void fixPreconditions( GameObject agent )
    {
        base.fixPreconditions();
        preConditions.Add( new isAt( (GameObject)agent, (GameObject)From ) );
    }

    public override void fixEffects( GameObject agent )
    {
        base.fixEffects();
        effects.Add( new isAt( (GameObject)agent, (GameObject)To, true ) );
        effects.Add( new isAt( (GameObject)agent, (GameObject)From, false ) );
    }

}
