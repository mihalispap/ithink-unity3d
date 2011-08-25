using UnityEngine;
using System.Collections;

#pragma warning disable 0660, 0661

public class hasFreeHands : iThinkFact
{

    public hasFreeHands()
        : base( "hasFreeHands" )
    {
    }

    public hasFreeHands( bool positive )
        : base( "hasFreeHands", positive )
    {
    }
}
