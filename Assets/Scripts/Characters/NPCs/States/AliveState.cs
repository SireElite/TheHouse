public class AliveState : State
{
    private NPC _npc;

    public AliveState(NPC npc)
    {
        _npc = npc;
    }

    public override void Enter()
    {
        _npc.CurrentHealth = _npc.NPC_SO.MaxHealth;
        _npc.transform.position = _npc.Controller.SpawnPoint.position;
        _npc.BoxCollider.enabled = true;
        _npc.Controller.enabled = true;
        _npc.GFX.SetActive(true);
        _npc.NameUI.SetActive(true);
        _npc.IsAlive = true;
    }
}
