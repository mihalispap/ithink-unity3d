using UnityEngine;
using System.Collections;

#pragma warning disable 0660, 0661

public class objectAt : iThinkFact
{
    GameObject Obj;
    GameObject Loc;

    public objectAt( GameObject obj, GameObject loc )
        : base( "objectAt", true )
    {
        Obj = obj;
        Loc = loc;
    }

    public override GameObject getObj1() { return Obj; }
    public override GameObject getObj2() { return Loc; }
}