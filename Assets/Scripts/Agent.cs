using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

    //public GameObject wayPoint;
    public Vector3 position; //position vector, of our agent
    public Vector3 velocity; //velocity vector, of our agent
    public Vector3 acceleration; //acceleration vector, or our agent

    public World world; //Reference to Class World aka World.cs
    public AgentConfig boidconfig; //Reference to Class AgentConfig aka AgentConfig.cs

	// Use this for initialization
	void Start () {

        world = FindObjectOfType<World>(); //Find objects with these scripts attached.
        boidconfig = FindObjectOfType<AgentConfig>();
        position = transform.position; //initialize boid position and velocity
        velocity = new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
        //wayPoint = GameObject.CreatePrimitive(PrimitiveType.Capsule);
    }
	
	// Update is called once per frame
	void Update () { //Will update the movement of the boids.

        //Newtonian Physics.
        acceleration = combineAllBehaviors(); //Calculate Acceleration of our Boid
        acceleration = Vector3.ClampMagnitude(acceleration, boidconfig.maxAcceleration); //makes sure the boids don't accelerate to fast, by giving them a max acceleration they could possibly have.

        velocity = velocity + acceleration * Time.deltaTime; //Calculate velocity from acceleration of our Boid
        velocity = Vector3.ClampMagnitude(velocity, boidconfig.maxVelocity); //makes sure the boids don't go to fast, by giving them a max velocity they could possibly have

        position = position + velocity * Time.deltaTime; //Calculate position from velocity of our Boid.

        boidWorldBoundaries(ref position, -world.bound, world.bound); //Bound the world given the dimension of world.bound.

        transform.position = position; //Assign the new position to the cubeBoid, that way it starts moving. 
	}

    bool cubeBoidsEyeSight(Vector3 someOtherBoidAgent)
    {//Checks to see if the object our cubeBoid AI sees is within its' field of view.
        return Vector3.Angle(this.velocity, someOtherBoidAgent - this.position) <= boidconfig.VisionArea;
    }

    Vector3 cohesion()
    {//The tendency of a cubeBoid to go to the center of all its' nearby neighbors, done by summing vector of neighbors
        Vector3 resultVecCohesion = new Vector3();
        int countBoidAgents = 0;
        var boidFriends = world.getBoidFriends(this, boidconfig.RadiusCohesion); //See if there are any cubeBoids in the vicinity.
        
        if (boidFriends.Count == 0)
        { //If there are no boids in the vicinity of our trigger radius, return a zero vector.
            return resultVecCohesion;
        }

        foreach( var boidFriend in boidFriends) 
        {
            if (cubeBoidsEyeSight(boidFriend.position))
            {//If there are neighbors find resulting vector for Cohesion by adding their vector to the resulting vector for (this)
                resultVecCohesion = resultVecCohesion + boidFriend.position;
                countBoidAgents++;
            }
        }

        if(countBoidAgents == 0)
        {
            return resultVecCohesion;
        }
        resultVecCohesion = resultVecCohesion / countBoidAgents;

        resultVecCohesion = resultVecCohesion - this.position; //Go towards center of neighboring boids, just as was said in book.

        resultVecCohesion = Vector3.Normalize(resultVecCohesion); //Normalize vector so vector is unit. 

        return resultVecCohesion;
    }

    Vector3 separation()
    { //the tendency of a cubeBoid to seperate from the flock, sum of vectors again of neighbors again.
        Vector3 resultVecSeparation = new Vector3();

        var boidFriends = world.getBoidFriends(this, boidconfig.RadiusSeperation); //See if there any cubeBoids in the vicinity.

        if(boidFriends.Count == 0)
        {
            return resultVecSeparation; //If no neighbors within the radius of seperation, cubeBoid doesn't have separation desire.
        }

        foreach(var boidFriend in boidFriends)
        {
            if (cubeBoidsEyeSight(boidFriend.position))
            {
                //Compute the vector that gives a force from each of (this) boid's friend.
                Vector3 forceOnThisBoid = this.position - boidFriend.position; //vector from (this) cubeBoid to other neighboring cubeBoids

                if (forceOnThisBoid.magnitude > 0)
                { //Take the vector that points from (this) cubeBoid and add it to the vector that has a "force" on (this) cubeBoid.
                    resultVecSeparation = resultVecSeparation + forceOnThisBoid.normalized / forceOnThisBoid.magnitude;
                }
            }
        }

        return resultVecSeparation.normalized;

    }

    Vector3 alignment()
    {// Tendency of a cubeBoid to align with it's neighbors.
        Vector3 resultVecAlignment = new Vector3();

        var boidFriends = world.getBoidFriends(this, boidconfig.RadiusAlignment); //See if there any cubeBoids in the vicinity.

        if (boidFriends.Count == 0) //If no neighbors within the radius of alignment, cubeBoid doesn't have alignment desire.
        {
            return resultVecAlignment;
        }

        //(this) boid will mimic the speed and direction of it's boid friends.
        foreach (var boidFriend in boidFriends)
        {
            if (cubeBoidsEyeSight(boidFriend.position))
            {
                resultVecAlignment = resultVecAlignment + boidFriend.velocity;
            }
        }
        
        return resultVecAlignment.normalized;
    }

    virtual protected Vector3 combineAllBehaviors() //make virtual in order to override a function in c sharp.
    {
        //Now we need to combine all the resulting vectors from each function to calculate the final vector the boid should have after adding all the other tendencies

        Vector3 finalVec = boidconfig.WeightC * cohesion() + boidconfig.WeightS * separation() + boidconfig.WeightA * alignment() + boidconfig.WeightFollow * followLeaders() + boidconfig.WeightSmoothing * birdFlightSmoothing();
        return finalVec;
    }

    void boidWorldBoundaries(ref Vector3 boundaryVector, float min, float max)
    { //Establishes a boundary so boids don't just go all over the place, but instead stay in a bounded box.
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
    Vector3 wayPointTarget;

    protected Vector3 birdFlightSmoothing()
    { //See Youtube: "Unity 5 Tutorial: Path Follow System Using Waypoints in C#. Thanks xOctoManx!
   
        //Make the boid follow small target in front of him. 
        wayPointTarget = wayPointTarget + new Vector3((Random.Range(0f, 1f) - Random.Range(0f, 1f)) * boidconfig.Smoothing * Time.deltaTime, 0, (Random.Range(0f, 1f) - Random.Range(0f, 1f)) * boidconfig.Smoothing * Time.deltaTime);

        //normalize the vector for unit circle. 
        wayPointTarget = wayPointTarget.normalized;

        //increase length to be the same as the radius of the wander circle 
        wayPointTarget = wayPointTarget * boidconfig.SmoothingRadius;

        //position the target in front of the cubeBoid
        Vector3 targetInLocalSpace = wayPointTarget + new Vector3(0, 0, boidconfig.SmoothingDistance);

        Vector3 targetInWorldSpace = transform.TransformPoint(targetInLocalSpace);

        //Steer toward that target.
        targetInWorldSpace = targetInWorldSpace - this.position;

        //wayPoint.transform.position = targetInWorldSpace;

        return targetInWorldSpace.normalized;
        //return wayPoint.transform.position;
    }

    Vector3 followLeaders()
    { //Flee from the red enemies.

        Vector3 resultVecFollow = new Vector3();

        var leaders = world.getLeaders(this, boidconfig.RadiusFollow); //get all the enemies from the world

        if(leaders.Count == 0)
        {
            return resultVecFollow;
        }

        //iterate over the leaders
        foreach(var leader in leaders)
        {
            resultVecFollow = resultVecFollow + follow(leader.position);
        }

        return resultVecFollow.normalized;
    }

    Vector3 follow(Vector3 target)
    {   //run with the target.
        Vector3 velocityToFollow = (target-position).normalized * boidconfig.maxVelocity; //get direction to leader by subtracting (this)cubeboid's position from the target aka leaders positon.

        //steer our velocity 
        return velocityToFollow - velocity;
    }
}

