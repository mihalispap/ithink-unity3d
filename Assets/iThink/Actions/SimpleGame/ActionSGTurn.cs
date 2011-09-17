using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ActionSGTurn : iThinkAction
{
    GameObject Fdir, Tdir;
    public ActionSGTurn( string name, GameObject fdir, GameObject tdir )
        : base( name )
    {
        Fdir = fdir;
        Tdir = tdir;

        initPreconditions();
        initEffects();
    }

    public void setArg1( GameObject fdir ) { Fdir = fdir; }
    public void setArg2( GameObject tdir ) { Tdir = tdir; }

    public GameObject getArg1() { return Fdir; }
    public GameObject getArg2() { return Tdir; }

    public override void initPreconditions()
    {
        base.initPreconditions();
        preConditions.Add( new iThinkFact("npcFacing", Fdir ) );
    }

    public override void initEffects()
    {
        base.initEffects();
        effects.Add( new iThinkFact("npcFacing", Tdir ) );
        effects.Add( new iThinkFact("npcFacing", false, Fdir ) );
    }

}
