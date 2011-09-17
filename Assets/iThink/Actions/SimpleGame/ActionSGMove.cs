using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ActionSGMove : iThinkAction
{
    GameObject From, To, Dir;
    public ActionSGMove( string name, GameObject from, GameObject to, GameObject dir )
        : base( name )
    {
        From = from;
        To = to;
        Dir = dir;

        initPreconditions();
        initEffects();
    }

    public void setArg1( GameObject from ) { From = from; }
    public void setArg2( GameObject to ) { To = to; }
    public void setArg3( GameObject dir ) { Dir = dir; }

    public GameObject getArg1() { return From; }
    public GameObject getArg2() { return To; }
    public GameObject getArg3() { return Dir; }

    public override void initPreconditions()
    {
        base.initPreconditions();
        preConditions.Add( new iThinkFact( "npcAt", From ) );
        preConditions.Add( new iThinkFact( "npcFacing", Dir ) );
        preConditions.Add( new iThinkFact( "adjacent", From, To, Dir ) );
    }

    public override void initEffects()
    {
        base.initEffects();
        effects.Add( new iThinkFact("npcAt", To ) );
        effects.Add( new iThinkFact("npcAt", false, From ) );
    }

}
