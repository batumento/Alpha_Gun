using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEngine.Animations.Rigging;
public class FireController : MonoBehaviour
{
    public PlayerPrefsManager playerPrefs;
    public WeaponController weaponController;
    public GunsType[] weapons;

    [SerializeField] public GameObject currentWeapon;
    
    public int currentIndex;

    [Header("WeaponStuff")]

    [SerializeField] Transform weaponParent;
    public Transform ammoSpawn;
    //[SerializeField] private TwoBoneIKConstraint rightIK;
    //[SerializeField] private TwoBoneIKConstraint leftIK;
    

    int isUsed;
    [Header("UI")]
    [SerializeField] private Transform weaponPanel;

    private void Awake()
    {
        playerPrefs = GetComponent<PlayerPrefsManager>();
        weaponController = GameObject.Find("Player").GetComponent<WeaponController>();
        weaponParent = weaponController.transform.Find("WeaponParent").transform;
        //rightIK = weaponController.transform.Find("mixamorig:Hips/RigLayer/RightHandIK").GetComponent<TwoBoneIKConstraint>();
        //leftIK = weaponController.transform.Find("mixamorig:Hips/RigLayer/LeftHandIK").GetComponent<TwoBoneIKConstraint>();
    }
    private void Start()
    {
        UpdateWeaponButtons();
        if (currentWeapon == null)
        {
            EquipButton(0);
        }
    }

    public void WeaponButton(int a)
    {
            weaponPanel.GetChild(a).GetChild(3).gameObject.SetActive(false);
            Image tempImage = weaponPanel.GetChild(a).GetChild(0).GetComponent<Image>();
            TMP_Text tempText = weaponPanel.GetChild(a).GetChild(1).GetChild(0).GetComponent<TMP_Text>();
            TMP_Text ammoText = weaponPanel.GetChild(a).GetChild(2).GetComponent<TMP_Text>();

            WeaponIntialize(a);
            tempImage.sprite = weapons[a].weaponIcon;
            tempText.text = weapons[a].weaponName;
            ammoText.text = weaponController.ammo[a].ToString();
    }
    public void UpdateWeaponButtons()
    {

        for(int i =0;i <weapons.Length; i++)
        {
            if (i == 1 && playerPrefs.levelUp < 10)
            {
                break;
            }
            if (i == 2 && playerPrefs.levelUp < 15)
            {
                break;
            }
            if (i == 3 && playerPrefs.levelUp < 20)
            {
                break;
            }

            weaponPanel.GetChild(i).GetChild(3).gameObject.SetActive(false);
            Image tempImage = weaponPanel.GetChild(i).GetChild(0).GetComponent<Image>();
            TMP_Text tempText = weaponPanel.GetChild(i).GetChild(1).GetChild(0).GetComponent<TMP_Text>();
            TMP_Text ammoText = weaponPanel.GetChild(i).GetChild(2).GetComponent<TMP_Text>();

            WeaponIntialize(i);
            tempImage.sprite = weapons[i].weaponIcon;
            tempText.text = weapons[i].weaponName;
            ammoText.text = weaponController.ammo[i].ToString();
        }
    }

    IEnumerator Equip(GunsType _weapon)
    {
        if (currentWeapon != null) Destroy(currentWeapon);
        GameObject _wPrefab = Instantiate(_weapon.prefab, weaponParent);
        currentWeapon = _wPrefab;
        ammoSpawn = _wPrefab.transform.GetChild(1).transform;

        //rightIK.data.target = _wPrefab.transform.GetChild(0).Find("GripR").transform;
        //leftIK.data.target = _wPrefab.transform.GetChild(0).Find("GripL").transform;

        yield return new WaitForEndOfFrame();
        
    }
    public void EquipButton(int weaponID)
    {
        if (weaponID == 0)
        {
            currentIndex = weaponID;
            StartCoroutine(Equip(weapons[weaponID]));
        }
        if (weaponID == 1)
        {
            if (playerPrefs.levelUp >= 10)
            {
                currentIndex = weaponID;
                StartCoroutine(Equip(weapons[weaponID]));
            }
        }
        if (weaponID == 2)
        {
            if (playerPrefs.levelUp >= 15)
            {
                currentIndex = weaponID;
                StartCoroutine(Equip(weapons[weaponID]));
            }
        }
        if (weaponID == 3)
        {
            if (playerPrefs.levelUp >= 20)
            {
                currentIndex = weaponID;
                StartCoroutine(Equip(weapons[weaponID]));
            }
        }
    }

    void WeaponIntialize(int ID)
    {
        if (isUsed < weapons.Length)
        {
            weaponController.Intialize(ID);
            isUsed++;
        }
    }
}
