using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask towerBuildLocationLayer;
    private Camera _mainCamera;

    // Start is called before the first frame update
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, towerBuildLocationLayer);
            
            //Select Tower Build Location
            if(hit.transform != null)
            {
                hit.transform.GetComponent<TowerBuildLocation>().ShowBuildTowerMenu();
            }
            //Deselect it
            else 
            {
                BuyTowerMenu.Instance.HideButton();
            }
        }
    }
}
