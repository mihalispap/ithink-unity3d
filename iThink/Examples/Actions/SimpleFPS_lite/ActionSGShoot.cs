using UnityEngine;

class ActionSGShoot : iThinkAction
{
    GameObject Locnpc, Locplayer, Dir, Gun;
    public ActionSGShoot( string name, GameObject locnpc, GameObject locplayer, GameObject dir, GameObject gun )
        : base( name )
    {
        Locnpc = locnpc;
        Locplayer = locplayer;
        Dir = dir;
        Gun = gun;

        initPreconditions();
        initEffects();
    }

    public override void initPreconditions()
    {
        preconditions.Add( new iThinkFact("npcHolding", Gun ) );
        preconditions.Add( new iThinkFact("gun", Gun ) );
        preconditions.Add( new iThinkFact("adjacent", Locnpc, Locplayer, Dir ) );
        preconditions.Add( new iThinkFact("npcAt", Locnpc ) );
        preconditions.Add( new iThinkFact("playerAt", Locplayer ) );
        preconditions.Add( new iThinkFact("npcFacing", Dir ) );
    }

    public override void initEffects()
    {
        effects.Add( new iThinkFact("playerDown") );
    }

}
