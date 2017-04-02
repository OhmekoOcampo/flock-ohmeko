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
        acceleration = cohesion();
        acceleration = Vector3.ClampMagnitude(acceleration, conf.maxAcceleration); //makes sure the boids don't accelerate to fast

        velocity = velocity + acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, conf.maxVelocity); //makes sure the boids don't go to fast

        position = position + velocity * Time.deltaTime;

        transform.position = position;
	}

    Vector3 cohesion()
    {
        Vector3 resultVecCohesion = new Vector3();

        var boidFriends = world.getBoidFriends(this, conf.RadiusCohesion);

        if (boidFriends.Count == 0)
        { //Check to see if there any boids near a particular (this) boid.
            return resultVecCohesion;
        }

        foreach( var agent in boidFriends)
        {
            resultVecCohesion = resultVecCohesion + agent.position;
        }
        resultVecCohesion = resultVecCohesion / boidFriends.Count;

        resultVecCohesion = resultVecCohesion - this.position;

        Vector3.Normalize(resultVecCohesion);

        return resultVecCohesion;
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
