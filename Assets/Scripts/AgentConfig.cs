using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentConfig : MonoBehaviour
{
    public float RadiusCohesion; //Radius for Cohesion behavior to take affect
    public float RadiusSeperation; //Radius for Seperation behavior to take affect
    public float RadiusAlignment; //Radius for Alignment behavior to take affect
    public float RadiusFollow; //Radius for Following behavior to take affect

    public float WeightC; //How much emphasis should be placed on Cohesion tendency
    public float WeightS; //How much emphasis should be placed on Seperation tendency
    public float WeightA; //How much emphasis should be placed on Alignment tendency
    public float WeightFollow; //How much emphasis should be placed on Following a Leader

    public float VisionArea = 90; //90 degree bird viewing angle
    public float maxVelocity; //maximum velocity
    public float maxAcceleration; //maximum acceleration


    //Smoothing Variables, smooths out the bird flight path for more natural flocking.
    public float Smoothing;
    public float SmoothingRadius;
    public float SmoothingDistance;
    public float WeightSmoothing;


}
