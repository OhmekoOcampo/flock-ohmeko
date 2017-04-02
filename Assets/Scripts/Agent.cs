using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

    public Vector3 position; //position
    public Vector3 velocity; //velocity vector
    public Vector3 acceleration; //acceleration vector

    public World world;
    public AgentConfig

	// Use this for initialization
	void Start () {

        world = FindObjectOfType<World>();
        position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        //Newtonian Physics. Integration.
        acceleration = combine();
        velocity = velocity + acceleration * Time.deltaTime;
        position = position + velocity * Time.deltaTime;

        transform.position = position;
	}

    Vector3 cohesion()
    {
        return Vector3.zero;
    }

    Vector3 separation()
    {
        return Vector3.zero;
    }

    Vector3 alignment()
    {
        return Vector3.zero;
    }

    Vector3 combine()
    {
        return Vector3.zero;
    }

}
