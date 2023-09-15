using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour,IDamagable
{
    public GameObject shootEFT;
    public void Damage(Vector3 pos)
    {
        Instantiate(shootEFT, pos, Quaternion.identity);
    }
}
