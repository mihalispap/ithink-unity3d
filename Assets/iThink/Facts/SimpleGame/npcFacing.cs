using UnityEngine;
using System.Collections;

#pragma warning disable 0660, 0661

public class npcFacing : iThinkFact
{
    // TODO: Convert the temporary DIR gameobjects to strings or something
    GameObject Dir;

    public npcFacing( GameObject dir )
        : base( "npcFacing", true )
    {
        Dir = dir;
    }

    public override GameObject getObj1() { return Dir; }
}