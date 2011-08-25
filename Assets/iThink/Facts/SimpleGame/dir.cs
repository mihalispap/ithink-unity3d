using UnityEngine;
using System.Collections;

#pragma warning disable 0660, 0661

public class dir : iThinkFact
{
    GameObject Obj;

    public dir( GameObject obj )
        : base( "dir", true )
    {
        Obj = obj;
    }

    public override GameObject getObj1() { return Obj; }
}