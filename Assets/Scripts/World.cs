using System.Collections;
using System.Collections.Generic; //So we can use Generics.
using UnityEngine;

public class World : MonoBehaviour
{

    public Transform cubeBoid; //give the transform of a boid which is position, rotation, and scale
    public int numCubeBoid; //the number of agents the world will spawn.

    public List<Agent> agentBoids; //list for any agent cube boid, sphere boids, etc...
    public List<Leader> leaders; //list for number of leader sphereboids

    public float bound; //bound for the world
    public float spawnRadiusBoid; //Radius of spawning.

    // Use this for initialization
    void Start()
    {
        agentBoids = new List<Agent>(); //Create a list for these cubeBoids agents
        spawnCubeBoid(cubeBoid, numCubeBoid); //Call Instantiation Function, so cubeboids can populate scene

        agentBoids.AddRange(FindObjectsOfType<Agent>()); //Find all the objects that have Agent.cs attached to it, and since we instantiated them at start all those cubeBoids will be placed in agents array.
        leaders.AddRange(FindObjectsOfType<Leader>()); //Find all the objects that have Leader.cs attached to it, so we can keep track of the number of leaders.

    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawnCubeBoid(Transform prefab, int n)
    {//take the cube object called agent, and randomly place copies of it around the scene. 
        for(int i = 0; i < n; i++)
        { 
            var obj = Instantiate(prefab, new Vector3(Random.Range(-spawnRadiusBoid, spawnRadiusBoid), 0, Random.Range(-spawnRadiusBoid, spawnRadiusBoid)),Quaternion.identity);
            //instantiate a bunch of cubeboid bird objects on the x and z axis
        }
    }

    public List<Agent> getBoidFriends(Agent agent, float radius)
    { //Take note of all the cubeBoids around a particular cubeBoid
        List<Agent> collectBoidNeighbors = new List<Agent>();
        
        foreach(var otherBoid in agentBoids)
        {
            if(otherBoid == agent) //No one is a neighbor of itself.
            {
                continue;
            }
            if(Vector3.Distance(agent.position,otherBoid.position) <= radius) 
            {//If the cubeBoid falls within the radius then accept it into list, so we can access it's transform for Agent.cs
                collectBoidNeighbors.Add(otherBoid);
            }
        }
        return collectBoidNeighbors; //Return the array for processing.
    }


    public List<Leader> getLeaders(Agent agent, float radius)
    { //Take note of all the leader sphereBoids around a particular cubeBoid
        List<Leader> collectLeaders = new List<Leader>();

        foreach (var leader in leaders)
        {
            if (Vector3.Distance(agent.position, leader.position) <= radius)
            { //Check to see if there is leader to follow
                collectLeaders.Add(leader);
            }
        }
        return collectLeaders; //Return the array for processing
    }

}