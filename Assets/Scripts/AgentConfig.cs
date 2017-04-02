using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentConfig : MonoBehaviour
{
    public float RadiusCohesion;
    public float RadiusSeperation;
    public float RadiusAlignment;

    public float Kc;
    public float Ks;
    public float Ka;

    public float maxV; //maximum velocity
    public float maxA; //maximum acceleration
}
