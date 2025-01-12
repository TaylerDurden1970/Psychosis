using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image healthFiller;
    public GameObject WaveInfoPanel;

    public static UIController instance;

    public void Awake()
    {
        instance = this;
    }

    public void OverrideHealthFiller(int currentHP, int maximumHP)
    {
        healthFiller.fillAmount = (float)currentHP / maximumHP;
        Debug.Log((float)currentHP / maximumHP);
    }

    public void ShowWaveInfo(string Text)
    {
        WaveInfoPanel.GetComponent<Animator>().SetTrigger("ShowUI");
        WaveInfoPanel.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Text;
    }
}
