using UnityEngine;
using System.Collections;

#pragma warning disable 0660, 0661

public class isAt : iThinkFact
{
    GameObject owner;
    GameObject coords;

    public isAt( GameObject Own, GameObject Coords )
        : base( "isAt" )
    {
        owner = Own;
        coords = Coords;
    }

    public isAt( GameObject Own, GameObject Coords, bool positive )
        : base( "isAt", positive )
    {
        owner = Own;
        coords = Coords;
    }

    public override GameObject getObj1() { return owner; }
    public override GameObject getObj2() { return coords; }
}