

using System;


using UnityEngine;
[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour
{

    public float speed = 0.001f;
    private PlayerMotor motor;
    private ConfigurableJoint joint;
    public float lookSpeed = 3f;
    public float ThrusterForce = 1000f;
  
    
    
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
       
    }

    // Update is called once per frame
    void Update()
    {
        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");
        
        
        Vector3 moveHorizontal = transform.right * xMov;
        Vector3 moveVertical = transform.forward * zMov;
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed ;
        motor.Move(velocity);
        
        
        float yRot = Input.GetAxis("Mouse X");
        Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSpeed;
        motor.Rotate(rotation);
       
        
        float xRot = Input.GetAxis("Mouse Y");
        float Camrotation = -xRot * lookSpeed;
        motor.RotateCam(Camrotation);

        Vector3 thrusterforce = Vector3.zero;
        if(Input.GetButton("Jump"))
        {
            thrusterforce = Vector3.up * ThrusterForce;
            
        }
        else if(Input.GetButton("Down"))
        {
            thrusterforce = -Vector3.up * ThrusterForce ;
        }
        motor.ApplyThruster(thrusterforce);


    }
    

}
