using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyTowerMenu : MonoBehaviour
{
    public static BuyTowerMenu Instance;

    public bool IsShowing { get; private set; } = true;

    public int TowerPrice;

    [SerializeField]
    private Button button;

    [SerializeField]
    private TowerSpawner towerSpawner;

    [SerializeField]
    private TextMeshProUGUI towerPriceText;

    private Camera _mainCamera;
    private TowerBuildLocation _currentBuildLocation; 

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

        _mainCamera = Camera.main;
        HideButton();
        towerPriceText.text = TowerPrice.ToString() + "$";    
    }

    private void Update() 
    {
        if(IsShowing)
        {
            if(GameManager.Instance.Coins < TowerPrice)
            {
                button.interactable = false;
            }
            else 
            {
                button.interactable = true;
            }
        }
    }

    public void ShowMenuAtLocation(TowerBuildLocation buildLocation)
    {
        IsShowing = true;
        button.gameObject.SetActive(true);
        transform.position = _mainCamera.WorldToScreenPoint(buildLocation.transform.position);
        _currentBuildLocation = buildLocation;
    }

    public void BuyTower()
    {
        if(GameManager.Instance.Coins >= TowerPrice)
        {
            GameManager.Instance.AddOrRemoveCoins(-TowerPrice);
            towerSpawner.SpawnTowerEntity(_currentBuildLocation.transform.position);
            Destroy(_currentBuildLocation.gameObject);
            button.interactable = false;
            HideButton();
        }
    }

    public void HideButton()
    {
        IsShowing = false;
        button.gameObject.SetActive(false);
    }
}
