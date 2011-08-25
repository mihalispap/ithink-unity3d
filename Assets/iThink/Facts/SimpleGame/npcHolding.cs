using UnityEngine;
using System.Collections;

#pragma warning disable 0660, 0661

public class npcHolding : iThinkFact
{
    GameObject Obj;

    public npcHolding( GameObject obj )
        : base( "npcHolding", true )
    {
        Obj = obj;
    }

    public override GameObject getObj1() { return Obj; }
}