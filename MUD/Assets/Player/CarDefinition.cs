using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarDefinition", menuName = "Cars/CarDefinition")]
public class CarDefinition : ScriptableObject
{
    public string description;
    public List<float> gearRatios;
    public float reverseRatio;
    public float differentialRatio;
    public float transmissionEfficiency;
    public float frontalArea;
    public float wheelRadius;
    public float mass;
    public float frictionCoefficient;
    public float rollingResistanceCoefficient;
    public float brakingcoefficient;
    public AnimationCurve torqueCurve;
}
