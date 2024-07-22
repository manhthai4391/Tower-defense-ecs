using TMPro;
using UnityEngine;

public class PlayerCoinText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    private void Start()
    {
        OnPlayerCoinChange();
    }

    public void OnPlayerCoinChange()
    {
        text.text = GameManager.Instance.Coins.ToString();
    }
}
