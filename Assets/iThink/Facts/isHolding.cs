using UnityEngine;
using System.Collections;

#pragma warning disable 0660, 0661

public class isHolding : iThinkFact
{
    GameObject what;

    public isHolding( GameObject What )
        : base( "isHolding" )
    {
        what = What;
    }

    public isHolding( GameObject What, bool positive )
        : base( "isHolding", positive )
    {
        what = What;
    }

    public override GameObject getObj1()
    {
        return what;
    }
}