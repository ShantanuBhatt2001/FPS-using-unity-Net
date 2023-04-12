
using System.Collections.Specialized;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Rigidbody rb;
    private float CamrotationX = 0f;
    private float CurrentCamRot = 0f;
    public Camera Cam;
    public float CamRotClamp = 85f;
    public Vector3 ThrusterForce = Vector3.zero;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Rotate(Vector3 inrotation)
    {
        rotation = inrotation;
    }
    public void Move(Vector3 invelocity)
    {
        velocity = invelocity;
    }
    public void RotateCam(float inCamRotationX)
    {
        CamrotationX = inCamRotationX;
    }



    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();

    }
    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.AddForce(velocity * Time.fixedDeltaTime,ForceMode.VelocityChange);
        }
        if(ThrusterForce!=Vector3.zero)
        {
            rb.AddForce(ThrusterForce * Time.deltaTime, ForceMode.Acceleration);
        }
    }
    void PerformRotation()
    {
        if (rotation != Vector3.zero)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        }
        if (Cam != null)
        {
            CurrentCamRot += CamrotationX;
            CurrentCamRot = Mathf.Clamp(CurrentCamRot,-CamRotClamp, CamRotClamp);
            Cam.transform.localEulerAngles = new Vector3(CurrentCamRot, 0f, 0f);
        }

    }
    public void ApplyThruster(Vector3 inThrusterForce)
    {
        ThrusterForce = inThrusterForce;
    }

}
