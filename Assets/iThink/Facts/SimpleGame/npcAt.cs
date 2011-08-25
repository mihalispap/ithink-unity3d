using UnityEngine;
using System.Collections;

#pragma warning disable 0660, 0661

public class npcAt : iThinkFact
{
    GameObject Loc;

    public npcAt( GameObject loc )
        : base( "npcAt", true )
    {
        Loc = loc;
    }

    public override GameObject getObj1() { return Loc; }
}