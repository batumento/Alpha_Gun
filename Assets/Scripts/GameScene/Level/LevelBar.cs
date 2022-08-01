using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelBar : MonoBehaviour
{
    [SerializeField] private FireController fireController;
    [SerializeField] private PlayerPrefsManager playerPrefs;
    [Header("UI System")]
    public RectTransform levelBar;
    [SerializeField] private TextMeshProUGUI level_txt;

    [Header("Level System")]
    public float totalExp;
    public int nextXp;
    public float currentExp;
    public int levelUp;
    
    private void Awake()
    {
        fireController = transform.parent.GetComponent<FireController>();
        levelBar.localScale = new Vector3(0, 1, 1);
        level_txt.text = PlayerPrefs.GetInt(nameof(levelUp)).ToString();
    }
    private void Start()
    {
        if (totalExp > playerPrefs.totalExp)
        {
            totalExp = 100;
        }
        else
        {
            totalExp = playerPrefs.totalExp;
        }
    }
    public void AddExperience(float amount)
    {
        currentExp += amount;
        if (currentExp>=totalExp)
        {
            playerPrefs.LevelValueSet();
            currentExp -= totalExp;
            totalExp += nextXp;
            playerPrefs.TotalExp(nextXp);
            level_txt.text = PlayerPrefs.GetInt(nameof(levelUp)).ToString();
            if (PlayerPrefs.GetInt(nameof(levelUp)) > 10)
            {
                fireController.WeaponButton(1);
            }
            if (PlayerPrefs.GetInt(nameof(levelUp)) > 15)
            {
                fireController.WeaponButton(2);
            }
            if (PlayerPrefs.GetInt(nameof(levelUp)) > 20)
            {
                fireController.WeaponButton(3);
            }
        }
    }
    public float GetExperienceNormalized()
    {
        return currentExp / totalExp;
    }
}
