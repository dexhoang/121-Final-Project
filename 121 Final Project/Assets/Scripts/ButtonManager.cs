using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance;

    [SerializeField] private GameObject _buttonPanel;
    [SerializeField] private TextMeshProUGUI waterText;
    [SerializeField] private TextMeshProUGUI sunText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowButtonsAtTile(Vector3 tilePosition, float water, float sun)
    {
        _buttonPanel.SetActive(true);

        waterText.text = "Water Level: " + water.ToString("F1");
        sunText.text = "Sun Level: " + sun.ToString("F1");
    }

    public void HideButtons()
    {
        _buttonPanel.SetActive(false);
    }

    public bool PanelActive()
    {
        return _buttonPanel.activeSelf;
    }
}

