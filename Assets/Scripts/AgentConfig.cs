﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentConfig : MonoBehaviour
{
    public float RadiusCohesion;
    public float RadiusSeperation;
    public float RadiusAlignment;

    public float WeightC;
    public float WeightS;
    public float WeightA;

    public float maxVelocity; //maximum velocity
    public float maxAcceleration; //maximum acceleration
}
