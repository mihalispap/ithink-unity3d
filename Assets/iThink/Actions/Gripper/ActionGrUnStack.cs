using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ActionGrUnStack : iThinkAction
{
    GameObject Top, Below;
    public ActionGrUnStack( string name, GameObject top, GameObject below )
        : base( name )
    {
        Top = top;
        Below = below;

        initPreconditions();
        initEffects();
    }

    public void setArg1( GameObject top ) { Top = top; }
    public void setArg2( GameObject below ) { Below = below; }

    public GameObject getArg1() { return Top; }
    public GameObject getArg2() { return Below; }

    public override void initPreconditions()
    {
        base.initPreconditions();
        preConditions.Add( new iThinkFact( "on", Top, Below ) );
        preConditions.Add( new iThinkFact( "clear", Top ) );
        preConditions.Add( new iThinkFact( "gripEmpty") );
    }

    public override void initEffects()
    {
        base.initEffects();
        effects.Add( new iThinkFact( "clear", Below ) );
        effects.Add( new iThinkFact( "on", false, Top, Below ) );
        effects.Add( new iThinkFact( "clear", false, Top ) );
        effects.Add( new iThinkFact( "gripEmpty", false ) );
        effects.Add( new iThinkFact( "holding", Top ) );
    }

}
