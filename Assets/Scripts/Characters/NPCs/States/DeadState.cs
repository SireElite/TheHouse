public class DeadState : State
{
    private NPC _npc;

    public DeadState(NPC npc)
    {
        _npc = npc;
    }

    public override void Enter()
    {
        _npc.BoxCollider.enabled = false;
        _npc.Controller.enabled = false;
        _npc.GFX.SetActive(false);
        _npc.NameUI.SetActive(false);
        _npc.IsAlive = false;
        _npc.DropMoney();
        _npc.StartCoroutine(_npc.Revive());
    }
}
