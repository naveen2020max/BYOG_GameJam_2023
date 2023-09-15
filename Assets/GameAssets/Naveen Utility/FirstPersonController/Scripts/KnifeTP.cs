using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeTP : MonoBehaviour
{
    public GameObject BladePrefab;
    public Transform BladeSpawnPos;

    public GameObject Indicator;

    private bool BladeAvalable;
    private GameObject CurrentKnife;

    private void Start()
    {
        SpawnBlade();
    }
    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && BladeAvalable)
        {
            Indicator.transform.position = PlaceToShoot(new Vector2(Screen.width / 2, Screen.height / 2));

        }

    }
    private void ShootBlade(Vector3 _destination)
    {
       
    }

    private Vector3 PlaceToShoot(Vector3 _pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(_pos);
        if(Physics.Raycast(ray,out RaycastHit hit, 100))
        {
            return hit.point;
        }
        else
        {
            return transform.position + GetComponentInChildren<Camera>().transform.forward * 100;
        }
    }

    private void SpawnBlade()
    {
        GameObject blade = Instantiate(BladePrefab, BladeSpawnPos.position, BladeSpawnPos.rotation);
        BladeAvalable = true;
        CurrentKnife = blade;
    }
}
