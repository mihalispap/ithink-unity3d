using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ActionGrPutDown : iThinkAction
{
    GameObject Obj;
    public ActionGrPutDown( string name, GameObject obj)
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
        preConditions.Add( new iThinkFact( "holding", Obj ) );
    }

    public override void initEffects()
    {
        base.initEffects();
        effects.Add( new iThinkFact( "clear", Obj ) );
        effects.Add( new iThinkFact( "onTable", Obj ) );
        effects.Add( new iThinkFact( "gripEmpty") );
        effects.Add( new iThinkFact( "holding", false, Obj ) );
    }

}
