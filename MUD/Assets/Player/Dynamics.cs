using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dynamics
{
    public const float AIR_DENSITY = 1.29f;
    
    public static Vector3 CalculateAerodynamicDrag(float c_drag, Vector3 velocity){
        return -c_drag * velocity * velocity.magnitude;
    }

    public static Vector3 CalculateRollingResistance(float c_rolling_resistance, Vector3 velocity){
        return -c_rolling_resistance * velocity;
    }

    public static Vector3 CalculateAirResistance(float frictionCoefficient, float frontalArea, Vector3 velocity){
        return 0.5f * frictionCoefficient * frontalArea * AIR_DENSITY * velocity * velocity.magnitude;
    }

    public static float CalculateHorsePower(float torque, float rpm){
        return torque * rpm / 5252;
    }

    public static Vector3 CalculateLongitudinalForceFromEngine(Vector3 direction, float torque, float gearRatio, float differentialRatio, float transmissionEfficiency, float wheelRadius){
        return direction * torque * gearRatio * differentialRatio * transmissionEfficiency / wheelRadius;
    }
}
