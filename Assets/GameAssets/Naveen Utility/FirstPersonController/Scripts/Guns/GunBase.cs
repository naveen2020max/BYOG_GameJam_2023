using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour,IGun
{
    public Transform gunpoint;
    public LayerMask shootable;
    public BulletTrail BT;
    public float gunFirerate;
    private bool ready;

    public float FireRate { get; set; }

    private float gunrate;
    // Start is called before the first frame update
    void Start()
    {
        FireRate = gunFirerate;
    }

    // Update is called once per frame
    

    public void Shoot()
    {
        //shoot
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, shootable))
        {
            //Debug.Log("shoot");
            hit.collider.gameObject.GetComponent<IDamagable>().Damage(hit.point);

            Instantiate(BT, gunpoint.position, Quaternion.identity).SetLine(gunpoint.position, hit.point);

        }
        else
        {
            Instantiate(BT, gunpoint.position, Quaternion.identity).SetLine(gunpoint.position, Camera.main.transform.position + Camera.main.transform.forward * 50);
        }
    }
}
