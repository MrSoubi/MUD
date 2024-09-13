using UnityEngine;

public class CarControl : MonoBehaviour
{
    public float motorTorque = 2000;
    public float brakeTorque = 2000;
    public float maxSpeed = 20;
    public float steeringRange = 30;
    public float steeringRangeAtMaxSpeed = 10;
    public float centreOfGravityOffset = -1f;

    public AnimationCurve engineTorque;

    public float jumpForce = 1;

    WheelControl[] wheels;
    Rigidbody rigidBody;


    [HideInInspector] public string throttleInputName;
    [HideInInspector] public string brakeInputName;
    [HideInInspector] public string steeringInputName;
    [HideInInspector] public string jumpInputName;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        // Adjust center of mass vertically, to help prevent the car from rolling
        rigidBody.centerOfMass += Vector3.up * centreOfGravityOffset;

        // Find all child GameObjects that have the WheelControl script attached
        wheels = GetComponentsInChildren<WheelControl>();
    }

    float vInput = 0;
    float hInput = 0;
    bool desiredJump = false;

    // Update is called once per frame
    void Update()
    {
        vInput = Input.GetAxis(throttleInputName) - Input.GetAxis(brakeInputName);
        hInput = Input.GetAxis(steeringInputName);
        desiredJump = Input.GetButtonDown(jumpInputName);

        if (IsGrounded())
        {
            // Calculate current speed in relation to the forward direction of the car
            // (this returns a negative number when traveling backwards)
            float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.velocity);

            // Calculate how close the car is to top speed
            // as a number from zero to one
            float speedFactor = Mathf.InverseLerp(0, maxSpeed, forwardSpeed);

            // Use that to calculate how much torque is available 
            // (zero torque at top speed)
            float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);

            // �and to calculate how much to steer 
            // (the car steers more gently at top speed)
            float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);

            // Check whether the user input is in the same direction 
            // as the car's velocity
            bool isAccelerating = Mathf.Sign(vInput) == Mathf.Sign(forwardSpeed);

            foreach (var wheel in wheels)
            {
                // Apply steering to Wheel colliders that have "Steerable" enabled
                if (wheel.steerable)
                {
                    wheel.WheelCollider.steerAngle = hInput * currentSteerRange;
                }

                if (isAccelerating)
                {
                    // Apply torque to Wheel colliders that have "Motorized" enabled
                    if (wheel.motorized)
                    {
                        wheel.WheelCollider.motorTorque = vInput * currentMotorTorque;
                    }

                    wheel.WheelCollider.brakeTorque = 0;
                }
            }

            if (desiredJump)
            {
                rigidBody.AddForce(Vector3.up * jumpForce * rigidBody.mass, ForceMode.Impulse);
            }
        }
    }

    private bool IsGrounded()
    {
        bool isGrounded = false;

        foreach (var wheel in wheels)
        {
            isGrounded |= wheel.WheelCollider.isGrounded;
        }

        return isGrounded;
    }
}