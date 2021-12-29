using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicSeek : MonoBehaviour
{
    public CharacterController controller;
    public GameObject target;
    public float maxSpeed  = 6.0f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KinematicSteeringOutput output = GetSteering();
        controller.Move(output.direction.normalized * maxSpeed * Time.deltaTime);
    }

    private KinematicSteeringOutput GetSteering()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Vector3 moveDirection = Vector3.zero;

        KinematicSteeringOutput output = new KinematicSteeringOutput();

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
            moveDirection = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
        }

        output.direction = moveDirection;
        output.rotation = 0.0f;

        return output;
    }
}
