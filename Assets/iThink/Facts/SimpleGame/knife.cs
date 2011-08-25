using UnityEngine;
using System.Collections;

#pragma warning disable 0660, 0661

public class knife : iThinkFact
{
    GameObject Obj;

    public knife( GameObject obj )
        : base( "knife", true )
    {
        Obj = obj;
    }

    public override GameObject getObj1() { return Obj; }
}