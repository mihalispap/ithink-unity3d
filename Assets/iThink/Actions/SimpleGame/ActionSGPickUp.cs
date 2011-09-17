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

        initPreconditions();
        initEffects();
    }

    public void setArg1( GameObject obj ) { Obj = obj; }
    public void setArg2( GameObject loc ) { Loc = loc; }

    public GameObject getArg1() { return Obj; }
    public GameObject getArg2() { return Loc; }

    public override void initPreconditions()
    {
        base.initPreconditions();
        preConditions.Add( new iThinkFact("npcEmptyHands") );
        preConditions.Add( new iThinkFact("npcAt", Loc ) );
        preConditions.Add( new iThinkFact("objectAt", Obj, Loc ) );
    }

    public override void initEffects()
    {
        base.initEffects();
        effects.Add( new iThinkFact("npcEmptyHands", false ) );
        effects.Add( new iThinkFact("objectAt", false, Obj, Loc ) );
        effects.Add( new iThinkFact("npcHolding", Obj ) );
    }

}
