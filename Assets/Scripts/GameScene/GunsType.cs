using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu (fileName = "New Weapon", menuName = "Scriptable Objects/Create Weapon")]
public class GunsType : ScriptableObject
{
    [Header("Infos")]
    public string weaponName;
    public Sprite weaponIcon;
    [Header("Datas")]
    public GameObject prefab;
    public GameObject mermiPrefab;
    public float fireRate;
    public float reloadTime;
    public int minDamage;
    public int maxDamage;
    public int ammo;
    public int clipSize;
    public int burst; //0 pistol 1 rifle 3 meele
    public AudioClip soundEffect;
    public AudioClip reloadSound;
    public float pitchRandomization;
    public float soundVolume;

    public int stash; //currentAmmo
    public int clip; //currentClip

    public void Intialize()
    {
        stash = ammo;
        clip = clipSize;
    }

    public bool FireBullet()
    {
        if (clip >= 1)
        {
            clip -= 1;
            Debug.Log("Ateş Edildi");
            return true;
        }
        else return false;

    }

    public void Reload()
    {
        stash += clip;
        clip = Mathf.Min(clipSize, stash);
        stash -= clip; 

    }

    public int GetStash()
    {
        return stash;
    }

    public int GetClip()
    {
        return clip;
    }

}
