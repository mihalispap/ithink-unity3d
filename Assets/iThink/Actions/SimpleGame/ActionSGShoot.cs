using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ActionSGShoot : iThinkAction
{
    GameObject Locnpc, Locplayer, Dir, Gun;
    public ActionSGShoot( string name, GameObject locnpc, GameObject locplayer, GameObject dir, GameObject gun )
        : base( name )
    {
        Locnpc = locnpc;
        Locplayer = locplayer;
        Dir = dir;
        Gun = gun;

        fixPreconditions();
        fixEffects();
    }

    public override void setArg1( GameObject locnpc ) { Locnpc = locnpc; }
    public override void setArg2( GameObject locplayer ) { Locplayer = locplayer; }
    public override void setArg3( GameObject dir ) { Dir = dir; }
    public override void setArg4( GameObject gun ) { Gun = gun; }

    public override GameObject getArg1() { return Locnpc; }
    public override GameObject getArg2() { return Locnpc; }
    public override GameObject getArg3() { return Locnpc; }
    public override GameObject getArg4() { return Locnpc; }

    public override void fixPreconditions()
    {
        base.fixPreconditions();
        preConditions.Add( new npcHolding( Gun ) );
        preConditions.Add( new gun( Gun ) );
        preConditions.Add( new adjacent( Locnpc, Locplayer, Dir ) );
        preConditions.Add( new npcAt( Locnpc ) );
        preConditions.Add( new playerAt( Locplayer ) );
        preConditions.Add( new npcFacing( Dir ) );
    }

    public override void fixEffects( GameObject agent )
    {
        base.fixEffects();
        effects.Add( new playerDown() );
    }

}
