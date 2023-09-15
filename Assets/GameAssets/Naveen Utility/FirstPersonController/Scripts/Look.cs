using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public float LookSensitivity = 100f;

    public Transform PlayerTransform;

    private float Xrot = 45f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * LookSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * LookSensitivity * Time.deltaTime;

        Xrot -= mouseY;
        Xrot = Mathf.Clamp(Xrot, -90, 90);

        transform.localRotation = Quaternion.Euler(Xrot, 0, 0);
        PlayerTransform.Rotate(Vector3.up * mouseX);
    }
}
