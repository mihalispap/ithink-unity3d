using UnityEngine;
using System.Collections;

#pragma warning disable 0660, 0661

public class gun : iThinkFact
{
    GameObject Obj;

    public gun( GameObject obj )
        : base( "gun", true )
    {
        Obj = obj;
    }

    public override GameObject getObj1() { return Obj; }
}