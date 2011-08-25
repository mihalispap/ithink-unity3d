using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ActionSGTurn : iThinkAction
{
    GameObject From, To;
    public ActionSGTurn( string name, GameObject from, GameObject to )
        : base( name )
    {
        From = from;
        To = to;

        fixPreconditions();
        fixEffects();
    }

    public override void setArg1( GameObject from ) { From = from; }
    public override void setArg2( GameObject to ) { To = to; }

    public override GameObject getArg1() { return From; }
    public override GameObject getArg2() { return To; }

    public override void fixPreconditions( GameObject fdir, GameObject tdir )
    {
        base.fixPreconditions();
        preConditions.Add( new npcFacing( fdir ) );
        //preConditions.Add( new dir( fdir ) );
        preConditions.Add( new dir( tdir ) );
    }

    public override void fixEffects( GameObject fdir, GameObject tdir )
    {
        base.fixEffects();
        effects.Add( new npcFacing( tdir ) );
        effects.Remove( new npcFacing( fdir ) );
    }

}
