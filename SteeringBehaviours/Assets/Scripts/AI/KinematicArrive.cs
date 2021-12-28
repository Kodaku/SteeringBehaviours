using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicArrive : MonoBehaviour
{
    public CharacterController controller;
    public GameObject target;
    public float maxSpeed;
    public float satisfactionRadius;
    public float turnSmoothTime;
    private float timeToTarget;
    private float currentSpeed;
    private float turnSmoothVelocity;
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        KinematicSteeringOutput output = GetSteering();

        controller.Move(output.direction.normalized * currentSpeed * Time.deltaTime);
    }

    private KinematicSteeringOutput GetSteering()
    {
        KinematicSteeringOutput output = new KinematicSteeringOutput();

        Vector3 direction = target.transform.position - transform.position;
        Vector3 moveDirection = Vector3.zero;

        if(direction.magnitude < satisfactionRadius)
        {
            output.direction = moveDirection;
            output.rotation = 0.0f;
            return output;
        }

        // direction /= timeToTarget;
        currentSpeed /= timeToTarget;

        if(currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
        else
        {
            currentSpeed = direction.magnitude;
        }

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
