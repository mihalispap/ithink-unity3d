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

        initPreconditions();
        initEffects();
    }

    public void setArg1( GameObject locnpc ) { Locnpc = locnpc; }
    public void setArg2( GameObject locplayer ) { Locplayer = locplayer; }
    public void setArg3( GameObject dir ) { Dir = dir; }
    public void setArg4( GameObject gun ) { Gun = gun; }

    public GameObject getArg1() { return Locnpc; }
    public GameObject getArg2() { return Locnpc; }
    public GameObject getArg3() { return Locnpc; }
    public GameObject getArg4() { return Locnpc; }

    public override void initPreconditions()
    {
        base.initPreconditions();
        preConditions.Add( new iThinkFact("npcHolding", Gun ) );
        preConditions.Add( new iThinkFact("gun", Gun ) );
        preConditions.Add( new iThinkFact("adjacent", Locnpc, Locplayer, Dir ) );
        preConditions.Add( new iThinkFact("npcAt", Locnpc ) );
        preConditions.Add( new iThinkFact("playerAt", Locplayer ) );
        preConditions.Add( new iThinkFact("npcFacing", Dir ) );
    }

    public override void initEffects()
    {
        base.initEffects();
        effects.Add( new iThinkFact("playerDown") );
    }

}
