using UnityEngine;

class ActionSGStab : iThinkAction
{
    GameObject Loc, Knife;
    public ActionSGStab( string name, GameObject loc, GameObject knife )
        : base( name )
    {
        Loc = loc;
        Knife = knife;

        initPreconditions();
        initEffects();
    }

    public override void initPreconditions()
    {
        preconditions.Add( new iThinkFact("npcHolding", Knife ) );
        preconditions.Add( new iThinkFact("knife", Knife ) );
        preconditions.Add( new iThinkFact("npcAt", Loc ) );
        preconditions.Add( new iThinkFact("playerAt", Loc ) );
    }

    public override void initEffects()
    {
        effects.Add( new iThinkFact("playerDown") );
    }

}
