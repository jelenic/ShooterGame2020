using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class buttonListBtn : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI btnText;

    private void Start()
    {
        btnText = GetComponent<TextMeshProUGUI>();
    }

    public void setText(string text)
    {
        btnText.text = text;
    }
}
