using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    #region Fields
    private float StickToGround = 4;
    public TextMeshProUGUI SpeedText;
    [Header("Movement")]
    public float speed = 10;
    public float IncreaseRate = 10f;
    public float GravityMulti = 2;
    [Header("Jump")]
    public float JumpSpeed = 5f;
    public float AirJumpSpeed = 3f;
    public int AirJumpCountMax = 1;
    [Header("Dash")]
    public float DashSpeed = 25f;
    public int DashCount = 3;
    public float dashTime = 0.4f;
    public ParticleSystem ForwardDashParticle;
    [Header("Ledge Detect")]
    public float LedgeDetectDistance;
    public Transform HeadRay;
    public Transform ChestRay;
    public LayerMask LedgeLayer;
    public float LedgejumpHeight, LedgejumpForward;
    public float LedgeDuration;

    private Vector3 moveDir,dashDir;
    private Vector3 MoveInput;
    private Vector3 gravity;
    private Vector2 InputDir,previousInput;
    private CharacterController controller;
    private bool jump,airJump;
    private bool previousGround;
    private float curSpeed;
    private bool startRun;
    private int airJumpCount;
    public float ExternalForceTime { get; set; } = 0;
    //dash
    private bool dashing;
    private int dashAttempts;
    private float dashStartTime;
    //Ledge
    private bool isLedgeClimbing;
    #endregion

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        SpeedText.text = controller.velocity.magnitude.ToString();

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPosition();
        }

        //if (!jump)
        //{
        //    jump = Input.GetKeyDown(KeyCode.Space);
        //}

        JumpControl();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashing = true;
        }

        InputDir.x = Input.GetAxis("Horizontal");
        InputDir.y = Input.GetAxis("Vertical");

        HandleDash();

        if(InputDir.magnitude != 0 && !startRun)
        {
            startRun = true;
            curSpeed = 0;

        }else if(InputDir.magnitude == 0 && startRun)
        {
            startRun = false;
        }

        if (!previousGround && controller.isGrounded)
        {
            moveDir.y = 0;
            
        }

        previousGround = controller.isGrounded;
        
    }

    void FixedUpdate()
    {
        if (controller.isGrounded )
        {
            if (dashAttempts >= DashCount)
            {
                dashAttempts = 0; 
            }
            if (airJumpCount > 0)
            {
                ResetAirJump(); 
            }
        }

        LedgeDetect();

        MoveInput = transform.right * InputDir.x + transform.forward * InputDir.y;

        if (startRun)
        {
            curSpeed += IncreaseRate * Time.fixedDeltaTime;
            curSpeed = Mathf.Clamp(curSpeed, 0, speed);
        }

        //if (dash)
        //{
        //    curSpeed = DashSpeed;
        //    dash = false;
        //}
        
        if (ExternalForceTime <= 0)
        {
            //Debug.Log("nana");
            if (MoveInput != Vector3.zero )
            {
                moveDir.x = MoveInput.x * curSpeed;
                moveDir.z = MoveInput.z * curSpeed; 
            }

            if(previousInput != Vector2.zero && MoveInput == Vector3.zero)
            {
                moveDir.x = 0;
                moveDir.z = 0;
            }

            if (controller.isGrounded)
            {
                moveDir.y = -StickToGround;
                // jump
                if (jump)
                {
                    JumpForce(JumpSpeed);
                    jump = false;
                }

            }
            else if (airJump)
            {
                JumpForce(AirJumpSpeed);
                airJump = false;
            } 
            else
            {
                if (!dashing)
                {
                    moveDir += Physics.gravity * GravityMulti * Time.fixedDeltaTime;
                }
                
            }
        }
        else
        {
            ExternalForceTime -= Time.fixedDeltaTime;

        }

        //if (dashing)
        //{
        //    moveDir = dashDir;
        //}

        controller.Move(moveDir * Time.fixedDeltaTime);
        previousInput = InputDir;
    }

    private void JumpForce(float spd)
    {
        moveDir.y = spd;
    }

    private void JumpControl()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (controller.isGrounded)
            {
                jump = true;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (airJumpCount < AirJumpCountMax)
                    {
                        airJump = true;
                        airJumpCount++;
                    }
                }
            }
        }
        
    }

    public void JumpPadFunction(float force,Vector3 dir,float externaltime)
    {
        ExternalForceTime = externaltime;
        //JumpForce(force);
        moveDir = dir * force;
        //controller.Move(dir.normalized * force * Time.fixedDeltaTime);

    }

    public void ResetAirJump()
    {
        airJumpCount = 0;
    }

    private void HandleDash()
    {
        bool isTryingToDash = Input.GetKeyDown(KeyCode.E);

        if(isTryingToDash)
        {
            if(dashAttempts <= DashCount)
            {
                OnStartDash();
            }
        }

        if (dashing)
        {
            if(Time.time - dashStartTime <= dashTime)
            {
                if (InputDir.Equals(Vector2.zero))
                {
                    controller.Move(transform.forward * DashSpeed * Time.deltaTime);
                    //dashDir = transform.forward * DashSpeed;
                }
                else
                {
                    controller.Move(MoveInput * DashSpeed * Time.deltaTime);
                    //dashDir = moveDir.normalized * DashSpeed;
                }
                Debug.Log("dash0");

            }
            else
            {
                OnEndDash();
            }
        }
    }

    private void OnStartDash()
    {
        dashing = true;
        dashStartTime = Time.time;
        dashDir = Vector3.zero;
        dashAttempts++;
        ForwardDashParticle.Play();
    }

    private void OnEndDash()
    {
        dashing = false;
        dashDir = Vector3.zero;
        dashStartTime = 0;
    }

    private void LedgeDetect()
    {
        //Ray hray = new Ray(HeadRay.position, HeadRay.forward);
        //Ray cray = new Ray(ChestRay.position, ChestRay.forward);
        bool hray = Physics.Raycast(new Ray(HeadRay.position, HeadRay.forward), LedgeDetectDistance, LedgeLayer);
        bool cray = Physics.Raycast(new Ray(ChestRay.position, ChestRay.forward),out RaycastHit info, LedgeDetectDistance, LedgeLayer);

        //Debug.DrawRay(HeadRay.position, HeadRay.forward, hray ? Color.green : Color.red, LedgeDetectDistance/2);
        //Debug.DrawRay(ChestRay.position, ChestRay.forward, cray ? Color.green : Color.red, LedgeDetectDistance/4);

        if (!hray && cray && !isLedgeClimbing)
        {
            isLedgeClimbing = true;
            LedgeClimbing(info);
        }

    }

    private void LedgeClimbing(RaycastHit hit)
    {
        Debug.Log("Ledge climbing");
        Vector3 ToPlace = hit.point + Vector3.up * LedgejumpHeight + transform.forward * LedgejumpForward;
        moveDir.y = 0;
        ToggleCharacterController(false);
        //transform.position = ToPlace;
        StartCoroutine(LedgeClimbAnim(ToPlace));
        Debug.Log("player position :" + transform.position + "  Ledge position :" + ToPlace);
        isLedgeClimbing = false;
        //ToggleCharacterController(true);
    }

    public void ToggleCharacterController(bool b)
    {
        GetComponent<CharacterController>().enabled = b;
    }

    private IEnumerator LedgeClimbAnim(Vector3 Target)
    {
        float time = 0;
        Vector3 start = transform.position;
        while (time < LedgeDuration)
        {
            transform.position = Vector3.Lerp(start, Target, time / LedgeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        ToggleCharacterController(true);
    }

    private void ResetPosition()
    {
        ToggleCharacterController(false);
        transform.position = InfoDatabase.instance.CheckpointInfo.LoadData().CheckpointPosition;
        ToggleCharacterController(true);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("JumpPad"))
        {
            JumpPadFunction(hit.gameObject.GetComponent<JumpPad>().force,hit.gameObject.transform.TransformDirection(Vector3.up),
                hit.gameObject.GetComponent<JumpPad>().forceTime);
            Debug.Log(hit.gameObject.transform.TransformDirection(Vector3.up));
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "CheckPoint")
        {
            Debug.Log("checkpoint collided by trigger");
            CheckPoint cp = other.GetComponent<CheckPoint>();
            if (!cp.IsReached)
            {
                InfoDatabase.instance.CheckpointInfo.SaveData(new CheckPointData { CheckpointPosition = cp.CheckpointReached() });
            }
        }
    }



}
