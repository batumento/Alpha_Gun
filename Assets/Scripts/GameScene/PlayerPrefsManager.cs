using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPrefsManager : MonoBehaviour
{
    [Header("Money")]
    public int moneyValues;
    public TextMeshProUGUI beforeMoney;
    public TextMeshProUGUI afterMoney;

    [Header("Level")]
    public int levelUp;
    public int totalExp;
    public LevelBar experience;

    private void Awake()
    {
        experience = transform.Find("LevelPanel").GetComponent<LevelBar>();
    }
    void Start()
    {
        totalExp = PlayerPrefs.GetInt(nameof(totalExp));
        levelUp = PlayerPrefs.GetInt(nameof(levelUp));
        moneyValues = PlayerPrefs.GetInt(nameof(moneyValues));
        beforeMoney.text = PlayerPrefs.GetInt(nameof(moneyValues)).ToString();
        afterMoney.text = PlayerPrefs.GetInt(nameof (moneyValues)).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoneyValueIncrease()
    {
        moneyValues++;
        PlayerPrefs.SetInt(nameof(moneyValues), moneyValues);
        afterMoney.text = moneyValues.ToString();
    }
    public void TotalExp(int nextExp)
    {
        totalExp += nextExp;
        PlayerPrefs.SetInt(nameof(totalExp), totalExp);
    }
    public void LevelValueSet()
    {
        levelUp++;
        PlayerPrefs.SetInt(nameof(levelUp), levelUp);
    }
}
