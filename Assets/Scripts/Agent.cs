using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

    public Vector3 position; //position
    public Vector3 velocity; //velocity vector
    public Vector3 acceleration; //acceleration vector

    public World world;
    public AgentConfig conf;

	// Use this for initialization
	void Start () {

        world = FindObjectOfType<World>();
        conf = FindObjectOfType<AgentConfig>();
        position = transform.position;
	}
	
	// Update is called once per frame
	void Update () { //Will update the movement of the boids.
        
        //Newtonian Physics. Integration.
        acceleration = combine();
        acceleration = Vector3.ClampMagnitude(acceleration, conf.maxA); //makes sure the boids don't accelerate to fast

        velocity = velocity + acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, conf.maxV); //makes sure the boids don't go to fast

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
