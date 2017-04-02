using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{

    public Transform cubeBoid;
    public int numCubeBoid;

    public List<Agent> agents;

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
            var obj = Instantiate(prefab, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)),Quaternion.identity);
            //instantiate a bunch of cubeboid bird objects
        }
    }

    public List<Agent> getBoidFriends(Agent agent, float radius)
    {
        return null;
    }
}