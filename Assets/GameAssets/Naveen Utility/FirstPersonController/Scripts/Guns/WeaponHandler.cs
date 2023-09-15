using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public List<GunBase> Weapons;


    private float curGunRate;
    private IGun currWeapon;
    private bool ready;


    private void Start()
    {
        currWeapon = Weapons[0];
    }
    void Update()
    {
        if (currWeapon == null)
            return;
        if (!ready)
        {
            if (curGunRate < 0)
            {
                //ready to shoot
                ready = true;
                curGunRate = currWeapon.FireRate;
            }
            else
            {
                curGunRate -= Time.deltaTime;
            }
        }

        if (Input.GetMouseButton(0) && ready)
        {
            currWeapon.Shoot();
            Debug.Log("shoot");
            ready = false;
        }
    }
}
