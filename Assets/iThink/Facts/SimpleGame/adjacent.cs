using UnityEngine;
using System.Collections;

#pragma warning disable 0660, 0661

public class adjacent : iThinkFact
{
    /* TODO: PAAAAAAAAAAAAAAAAAAAAAAAAAAARA POLLH DOULEIA :( */
    GameObject owner;
    GameObject coords;

    public adjacent( GameObject Own, GameObject Coords )
        : base( "adjacent" )
    {
        owner = Own;
        coords = Coords;
    }

    public adjacent( GameObject Own, GameObject Coords, bool positive )
        : base( "adjacent", positive )
    {
        owner = Own;
        coords = Coords;
    }

    public override GameObject getObj1() { return owner; }
    public override GameObject getObj2() { return coords; }
}