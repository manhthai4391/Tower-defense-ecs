using UnityEngine;
using UnityEngine.Events;
using Unity.Entities;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int Health { get; private set; }

    public int Coins { get; private set; }

    public UnityEvent OnHealthChange;
    public UnityEvent OnCoinsChange;

    public UnityEvent OnGameOver;
    public UnityEvent OnPlayerWin;

    public int InitialHealth;
    public int InitialCoins;
    public int TotalTanks;
    public int CoinRewardOnTankDestroyed;

    public bool IsGameOver { get; private set; } = false;
    private int _tankDestroyed;

    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
            return;
        }

        Coins = InitialCoins;
        Health = InitialHealth;

        TankCollisionSystem tankCollision = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<TankCollisionSystem>();
        tankCollision.OnTankReachEndPoint.AddListener(TakeDamage);
        tankCollision.OnTankDestroy.AddListener(TankDestroyed);   
    }

    public void AddOrRemoveCoins(int amount)
    {        
        Coins += amount;
        OnCoinsChange.Invoke();
    }

    public void AddOrRemoveHealth(int amount)
    {
        if(IsGameOver)
            return;
        
        Health += amount;
        OnHealthChange?.Invoke();
        if(Health <= 0)
        {
            IsGameOver = true;
            OnGameOver.Invoke();
        }
    }

    private void TakeDamage()
    {
        AddOrRemoveHealth(-1);
        if(Health > 0)
        {
            CheckPlayerWin();
        }
    }

    private void TankDestroyed()
    {
        AddOrRemoveCoins(CoinRewardOnTankDestroyed);
        CheckPlayerWin();
    }

    private void CheckPlayerWin()
    {
        _tankDestroyed++;
        if(_tankDestroyed == TotalTanks)
        {
            OnPlayerWin?.Invoke();
            IsGameOver = true;
        }
    }
}
