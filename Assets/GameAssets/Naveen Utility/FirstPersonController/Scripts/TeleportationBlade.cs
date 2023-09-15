using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class TeleportationBlade : MonoBehaviour
{
    public LayerMask EnemyLayer;
    public float BladeLockTime;
    public float TeleBoostForce;

    private bool BladeAvalable;
    private bool BladeLock;
    private float LockTimer;
    private Vector3 TeleBoost;
    private GameObject EnemyGO;
    private PlayerController pc;

    private void Start()
    {
        pc = GetComponent<PlayerController>();
        BladeAvalable = true;
    }

    private void Update()
    {

        if (BladeLock)
        {
            if(LockTimer < 0)
            {
                ReleaseEnemy();
            }
            else
            {
                LockTimer -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Teleport();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q)&& !BladeLock)
        {
            ShootBlade();
        }
    }
    private void ShootBlade()
    {
        if (BladeAvalable)
        {
            LanchBlade();
        }
    }

    private void LanchBlade()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray,out RaycastHit hit, 300, EnemyLayer))
        {
            BladeAvalable = false;
            BladeLock = true;
            LockTimer = BladeLockTime;
            LockEnemy(hit.collider.gameObject);
        }
    }

    private void LockEnemy(GameObject _go)
    {
        EnemyGO = _go;
        EnemyGO.GetComponent<MeshRenderer>().material.color = Color.cyan;
    }

    private void ReleaseEnemy()
    {
        EnemyGO.GetComponent<MeshRenderer>().material.color = Color.white;
        EnemyGO = null;
        BladeAvalable = true;
        BladeLock = false;
    }

    private void Teleport()
    {
        Vector3 dir = EnemyGO.transform.position - transform.position;
        dir.Normalize();
        pc.ToggleCharacterController(false);
        Vector3 temp = transform.position;
        transform.position = EnemyGO.transform.position;
        EnemyGO.transform.position = temp;
        pc.ToggleCharacterController(true);
        pc.JumpPadFunction(TeleBoostForce, dir, 0.4f);
        ReleaseEnemy();
    }
}
