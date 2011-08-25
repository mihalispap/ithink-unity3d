using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ActionSGPickUp : iThinkAction
{
    GameObject Obj, Loc;
    public ActionSGPickUp( string name, GameObject obj, GameObject loc )
        : base( name )
    {
        Obj = obj;
        Loc = loc;

        fixPreconditions();
        fixEffects();
    }

    public override void setArg1( GameObject obj ) { Obj = obj; }
    public override void setArg2( GameObject loc ) { Loc = loc; }

    public override GameObject getArg1() { return Obj; }
    public override GameObject getArg2() { return Loc; }

    public override void fixPreconditions()
    {
        base.fixPreconditions();
        preConditions.Add( new npcEmptyHands() );
        preConditions.Add( new npcAt( Loc ) );
        preConditions.Add( new objectAt( Obj, Loc ) );
    }

    public override void fixEffects()
    {
        base.fixEffects();
        effects.Remove( new npcEmptyHands() );
        effects.Remove( new objectAt( Obj, Loc ) );
        effects.Add( new npcHolding( Obj ) );
    }

}
