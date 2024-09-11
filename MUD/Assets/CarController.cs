using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public CarDefinition data;
    private Rigidbody rb;

    private Vector3 velocity = Vector3.zero;
    private int rpm;

    public void Start(){
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate(){
        if (Input.GetKey(KeyCode.Space)){
            rpm = Mathf.Max(rpm - 15, 0);
            Vector3 acceleration = (GetDrag() + GetRollingResistance() + GetBrakingForce()) / data.mass;
            velocity += acceleration * Time.fixedDeltaTime;

            if (velocity.magnitude < 0.01) velocity = Vector3.zero;

            rb.position += velocity;
        }else{
            if (Input.GetKey(KeyCode.UpArrow)){
                rpm = Mathf.Min(rpm + 10, 6000);
            }else{
                rpm = Mathf.Max(rpm - 15, 0);
            }

            Vector3 acceleration = GetNetForces() / data.mass;
            velocity += acceleration * Time.fixedDeltaTime;
            rb.position += velocity;
        }

    }

    private Vector3 GetNetForces(){
        return GetTraction() + GetDrag() + GetRollingResistance();
    }

    private Vector3 GetBrakingForce(){
        return -transform.forward * data.brakingcoefficient;
    }

    private Vector3 GetDrag(){
        return -0.5f * data.frictionCoefficient * data.frontalArea * Dynamics.AIR_DENSITY * velocity * velocity.magnitude;
    }

    private Vector3 GetTraction(){
        return transform.forward * GetTorque() * data.gearRatios[0] * data.differentialRatio * data.transmissionEfficiency / data.wheelRadius;
    }

    private float GetTorque(){
        return data.torqueCurve.Evaluate(rpm);
    }

    private Vector3 GetRollingResistance(){
        return -data.rollingResistanceCoefficient * velocity;
    }
}
