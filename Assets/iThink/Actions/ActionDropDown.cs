using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDropDown : iThinkAction
{
    GameObject who, what;
    public ActionDropDown( string name, GameObject Who, GameObject What )
        : base( name )
    {
        who = Who;
        what = What;
    }

    public override GameObject getArg1()
    {
        return who;
    }

    public override GameObject getArg2()
    {
        return what;
    }

    public override void fixPreconditions( GameObject obj )
    {
        base.fixPreconditions();
        preConditions.Add( new isHolding( obj ) );

    }

    public override void fixEffects( GameObject location )
    {
        base.fixEffects();
        effects.Add( new isHolding( what, true ) );
        effects.Add( new isAt( what, location, false ) );
        effects.Add( new hasFreeHands( false ) );

    }
}
