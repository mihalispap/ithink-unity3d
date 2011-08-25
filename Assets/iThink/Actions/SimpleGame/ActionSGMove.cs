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

        fixPreconditions();
        fixEffects();
    }

    public override void setArg1( GameObject from ) { From = from; }
    public override void setArg2( GameObject to ) { To = to; }
    public override void setArg3( GameObject dir ) { Dir = dir; }

    public override GameObject getArg1() { return From; }
    public override GameObject getArg2() { return To; }
    public override GameObject getArg3() { return Dir; }

    public override void fixPreconditions()
    {
        base.fixPreconditions();
        preConditions.Add( new npcAt( From ) );
        preConditions.Add( new npcFacing( Dir ) );
        preConditions.Add( new adjacent( From, To, Dir ) );
    }

    public override void fixEffects()
    {
        base.fixEffects();
        effects.Add( new npcAt( To ) );
        effects.Remove( new npcAt( From ) );
    }

}
