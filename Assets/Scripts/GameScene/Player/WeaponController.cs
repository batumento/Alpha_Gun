using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GunsType[] _weapons;
    FireController fireController;
    public int[] ammo;
    public int[] clip;
    public int[] clipSize;
    private void Awake()
    {
        fireController = GameObject.Find("CanvasUI").GetComponent<FireController>();
    }
    private void Start()
    {
        for (int i = 0; i < _weapons.Length; i++)
        {
            Intialize(i);

        }
    }

    public void FireBullet(int ID)
    {
        if (ammo[ID] <= 0) return;

            ammo[ID] -= 1;
            fireController.UpdateWeaponButtons();
    }
    public void Reload(int ID)
    {
        ammo[ID] += clip[ID];
        clip[ID] = Mathf.Min(clipSize[ID], ammo[ID]);
        ammo[ID] -= clip[ID];
    }
    public void Intialize(int ID)
    {
        ammo[ID] = _weapons[ID].stash;
        clip[ID] = _weapons[ID].clip;
        clipSize[ID] = _weapons[ID].clipSize;
    }
}
