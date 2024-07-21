public class DialogueState : State
{
    public override void Enter()
    {
        Player.Instance.BlockLogic();
    }

    public override void Exit()
    {
        Player.Instance.UnblockLogic();
    }
}
