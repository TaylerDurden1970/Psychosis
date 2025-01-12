public class GameState
{
    protected GameManager gameManager;

    public GameState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void UpdateState() { }
}
