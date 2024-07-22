
using UnityEngine;

public class TowerBuildLocation : MonoBehaviour
{
    public void ShowBuildTowerMenu()
    {
        BuyTowerMenu.Instance.ShowMenuAtLocation(this);
    }
}
