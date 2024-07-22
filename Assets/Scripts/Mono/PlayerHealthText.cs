using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    private void Start()
    {
        OnHealthChange();
    }

    public void OnHealthChange()
    {
        text.text = GameManager.Instance.Health.ToString();
    }
}
