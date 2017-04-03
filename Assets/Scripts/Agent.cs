using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

    public Vector3 position; //position vector
    public Vector3 velocity; //velocity vector
    public Vector3 acceleration; //acceleration vector

    public World world;
    public AgentConfig conf;

	// Use this for initialization
	void Start () {

        world = FindObjectOfType<World>();
        conf = FindObjectOfType<AgentConfig>();
        position = transform.position;
        velocity = new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));

	}
	
	// Update is called once per frame
	void Update () { //Will update the movement of the boids.

        //Newtonian Physics. Integration.
        acceleration = combine();
        acceleration = Vector3.ClampMagnitude(acceleration, conf.maxAcceleration); //makes sure the boids don't accelerate to fast

        velocity = velocity + acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, conf.maxVelocity); //makes sure the boids don't go to fast

        position = position + velocity * Time.deltaTime;

        boidWorldBoundaries(ref position, -world.bound, world.bound);

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
        { //If there are neighbors find resulting vector for Cohesion
            resultVecCohesion = resultVecCohesion + agent.position;
        }
        resultVecCohesion = resultVecCohesion / boidFriends.Count;

        resultVecCohesion = resultVecCohesion - this.position; //Calculate new vector for (this) boid

        resultVecCohesion = Vector3.Normalize(resultVecCohesion); //After normalizing the resulting cohesion vector.

        return resultVecCohesion;
    }

    Vector3 separation()
    {
        Vector3 resultVecSeparation = new Vector3();

        var boidFriends = world.getBoidFriends(this, conf.RadiusSeperation);

        if(boidFriends.Count == 0)
        {
            return resultVecSeparation;
        }

        foreach(var agent in boidFriends)
        {
            //Compute the vector that gives a force from each of (this) boid's friend.
            Vector3 forceOnThisBoid = this.position - agent.position;
            
            if(forceOnThisBoid.magnitude > 0)
            {
                resultVecSeparation = resultVecSeparation + forceOnThisBoid.normalized / forceOnThisBoid.magnitude;
            }
        }

        return resultVecSeparation.normalized;

    }

    Vector3 alignment()
    {
        Vector3 resultVecAlignment = new Vector3();

        var boidFriends = world.getBoidFriends(this, conf.RadiusAlignment);

        if(boidFriends.Count == 0)
        {
            return resultVecAlignment;
        }

        //(this) boid will mimic the speed and direction of it's boid friends.
        foreach(var agent in boidFriends)
        {
            resultVecAlignment = resultVecAlignment + agent.velocity;
        }
        return resultVecAlignment.normalized;
    }

    Vector3 combine()
    {
        //Now we need to combine all the resulting vectors from each function.
        //The finalVec = WeightC*cohesion() + WeightS*separation() + WeightA*alignment()

        Vector3 finalVec = conf.WeightC * cohesion() + conf.WeightS * separation() + conf.WeightA * alignment();
        return finalVec;
    }

    void boidWorldBoundaries(ref Vector3 boundaryVector, float min, float max)
    {
        if(boundaryVector.x > max) //bounds x, so that boids wrap around.
        {
            boundaryVector.x = min;
        }else if(boundaryVector.x < min)
        {
            boundaryVector.x = max;
        }

        if (boundaryVector.y > max) //bounds y, so that boids wrap around.
        {
            boundaryVector.y = min;
        }
        else if (boundaryVector.y < min)
        {
            boundaryVector.y = max;
        }

        if (boundaryVector.z > max) //bounds z, so that boids wrap around.
        {
            boundaryVector.z = min;
        }
        else if (boundaryVector.z < min)
        {
            boundaryVector.z = max;
        }
    }
}
