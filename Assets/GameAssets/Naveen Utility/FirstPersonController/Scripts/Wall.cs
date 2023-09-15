using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IDamagable
{
    public GameObject shootEFT;
    public void Damage(Vector3 pos)
    {
        Instantiate(shootEFT, pos, Quaternion.identity);
    }
}
