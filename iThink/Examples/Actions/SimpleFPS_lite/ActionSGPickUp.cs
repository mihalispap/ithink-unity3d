using UnityEngine;

class ActionSGPickUp : iThinkAction
{
    GameObject Obj, Loc;
    public ActionSGPickUp( string name, GameObject obj, GameObject loc )
        : base( name )
    {
        Obj = obj;
        Loc = loc;

        initPreconditions();
        initEffects();
    }

    public override void initPreconditions()
    {
        preconditions.Add( new iThinkFact("npcEmptyHands") );
        preconditions.Add( new iThinkFact("npcAt", Loc ) );
        preconditions.Add( new iThinkFact("objectAt", Obj, Loc ) );
    }

    public override void initEffects()
    {
        effects.Add( new iThinkFact("npcEmptyHands", false ) );
        effects.Add( new iThinkFact("objectAt", false, Obj, Loc ) );
        effects.Add( new iThinkFact("npcHolding", Obj ) );
    }

}
