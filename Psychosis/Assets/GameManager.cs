using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameState currentState;

    public SafeZoneGate safeZoneGate;
    public WaveManager WaveManager;

    public bool isInSafeZone = false;

    private void Start()
    {
        SetState(new WaveRunningState(this));
    }

    private void Update()
    {
        currentState?.UpdateState();
    }

    public void SetState(GameState newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public void StartWave()
    {
        WaveManager.StartWave();
        UIController.instance.ShowWaveInfo("Wave starts!");
    }

    public bool AreAllEnemiesDefeated()
    {
        return WaveManager.AreAllEnemiesDefeated();
    }

    public void OnPlayerExitSafeZone()
    {
        // Когда игрок покидает безопасную зону, закрываем ворота
        safeZoneGate.CloseGate();
        isInSafeZone = false;
        
        // Начинаем новую волну
        StartWave();
    }

    public void OnWaveCompleted()
    {
        // Открыть ворота безопасной зоны
        safeZoneGate.OpenGate();

        // Остановить спавн врагов
        WaveManager.StopSpawning();
        UIController.instance.ShowWaveInfo("Wave compledet!");
        Debug.Log("Wave completed! Safe zone is now open.");
    }
}

public class WaveCompletedState : GameState
{
    public WaveCompletedState(GameManager gameManager) : base(gameManager) { }

    public override void EnterState()
    {
        Debug.Log("WaveCompleted");
        gameManager.safeZoneGate.OpenGate();
    }
}

public class SafeZoneState : GameState
{
    public SafeZoneState(GameManager gameManager) : base(gameManager) { }

    public override void EnterState()
    {
        Debug.Log("SafeZone:");       
    }

    public override void UpdateState()
    {
        if (!gameManager.isInSafeZone)
        {
            gameManager.safeZoneGate.CloseGate();
            gameManager.SetState(new WaveRunningState(gameManager));
        }
    }
}

public class WaveRunningState : GameState
{
    public WaveRunningState(GameManager gameManager) : base(gameManager) { }

    public override void EnterState()
    {
        Debug.Log("WaveRunning:");
        gameManager.StartWave();
    }

    public override void UpdateState()
    {
        if (gameManager.AreAllEnemiesDefeated())
        {
            gameManager.SetState(new WaveCompletedState(gameManager)); // ??????? ? ?????????? ?????????
        }
    }
}

