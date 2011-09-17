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

        initPreconditions();
        initEffects();
    }

    public void setArg1( GameObject loc ) { Loc = loc; }
    public void setArg2( GameObject knife ) { Knife = knife; }

    public GameObject getArg1() { return Loc; }
    public GameObject getArg2() { return Knife; }

    public override void initPreconditions()
    {
        base.initPreconditions();
        preConditions.Add( new iThinkFact("npcHolding", Knife ) );
        preConditions.Add( new iThinkFact("knife", Knife ) );
        preConditions.Add( new iThinkFact("npcAt", Loc ) );
        preConditions.Add( new iThinkFact("playerAt", Loc ) );
    }

    public override void initEffects()
    {
        base.initEffects();
        effects.Add( new iThinkFact("playerDown") );
    }

}
