using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShootableBOX : MonoBehaviour,IDamagable
{
    public GameObject shootEFT;

    public void Damage(Vector3 pos)
    {
        GetComponent<Rigidbody>().AddForce(-(pos - transform.position) * 100);
        Instantiate(shootEFT, pos, Quaternion.identity);
    }
}
