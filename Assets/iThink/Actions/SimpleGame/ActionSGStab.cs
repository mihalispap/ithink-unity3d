using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ActionSGStab : iThinkAction
{
    GameObject Loc, Knife;
    public ActionSGStab( string name, GameObject loc, GameObject knife )
        : base( name )
    {
        Loc = loc;
        Knife = knife;

        fixPreconditions();
        fixEffects();
    }

    public override void setArg1( GameObject loc ) { Loc = loc; }
    public override void setArg2( GameObject knife ) { Knife = knife; }

    public override GameObject getArg1() { return Loc; }
    public override GameObject getArg2() { return Knife; }

    public override void fixPreconditions()
    {
        base.fixPreconditions();
        preConditions.Add( new npcHolding( Knife ) );
        preConditions.Add( new knife( Knife ) );
        preConditions.Add( new npcAt( Loc ) );
        preConditions.Add( new playerAt( Loc ) );
    }

    public override void fixEffects()
    {
        base.fixEffects();
        effects.Add( new playerDown() );
    }

}
