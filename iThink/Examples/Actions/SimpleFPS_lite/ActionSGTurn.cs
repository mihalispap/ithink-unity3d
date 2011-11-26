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

    public override void initPreconditions()
    {
        preconditions.Add( new iThinkFact("npcFacing", Fdir ) );
    }

    public override void initEffects()
    {
        effects.Add( new iThinkFact("npcFacing", Tdir ) );
        effects.Add( new iThinkFact("npcFacing", false, Fdir ) );
    }

}
