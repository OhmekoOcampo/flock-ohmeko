using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{

    public Transform cubeBoid;
    public int numCubeBoid;

    public List<Agent> agents; //list for any agent cube boid, sphere boids, etc...

    public float bound; //bound for the world

    public float spawnRadiusAgent;

    // Use this for initialization
    void Start()
    {
        agents = new List<Agent>();
        spawnCubeBoid(cubeBoid, numCubeBoid);

        agents.AddRange(FindObjectsOfType<Agent>());

    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawnCubeBoid(Transform prefab, int n)
    {
        for(int i = 0; i < n; i++)
        {
            var obj = Instantiate(prefab, new Vector3(Random.Range(-spawnRadiusAgent, spawnRadiusAgent), 0, Random.Range(-spawnRadiusAgent, spawnRadiusAgent)),Quaternion.identity);
            //instantiate a bunch of cubeboid bird objects
        }
    }

    public List<Agent> getBoidFriends(Agent agent, float radius)
    {
        List<Agent> r = new List<Agent>();
        
        foreach(var otherAgent in agents)
        {
            if(otherAgent == agent) //No one is a neighbor of itself.
            {
                continue;
            }
            if(Vector3.Distance(agent.position,otherAgent.position) <= radius)
            {
                r.Add(otherAgent);
            }
        }
        return r;
    }
}