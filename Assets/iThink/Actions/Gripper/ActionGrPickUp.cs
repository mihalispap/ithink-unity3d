using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ActionGrPickUp : iThinkAction
{
    GameObject Obj;
    public ActionGrPickUp( string name, GameObject obj )
        : base( name )
    {
        Obj = obj;

        initPreconditions();
        initEffects();
    }

    public void setArg1( GameObject obj ) { Obj = obj; }

    public GameObject getArg1() { return Obj; }

    public override void initPreconditions()
    {
        base.initPreconditions();
        preConditions.Add( new iThinkFact( "clear", Obj ) );
        preConditions.Add( new iThinkFact( "onTable", Obj ) );
        preConditions.Add( new iThinkFact( "gripEmpty" ) );
    }

    public override void initEffects()
    {
        base.initEffects();
        effects.Add( new iThinkFact( "clear", false, Obj ) );
        effects.Add( new iThinkFact( "onTable", false, Obj ) );
        effects.Add( new iThinkFact( "gripEmpty", false ) );
        effects.Add( new iThinkFact( "holding", Obj ) );
    }

}
