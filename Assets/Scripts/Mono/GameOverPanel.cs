using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI resultText;

    public void PlayerWin()
    {
        gameObject.SetActive(true);
        resultText.text = "Victory!";
    }

    public void PlayerLose()
    {
        gameObject.SetActive(true);
        resultText.text = "Game Over!";
    }
}
