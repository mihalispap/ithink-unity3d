using UnityEngine;
using System.Collections;

#pragma warning disable 0660, 0661

public class playerAt : iThinkFact
{
    GameObject Loc;

    public playerAt( GameObject loc )
        : base( "playerAt", true )
    {
        Loc = loc;
    }

    public override GameObject getObj1() { return Loc; }
}