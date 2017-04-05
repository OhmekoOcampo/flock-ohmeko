using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : Agent {

    override protected Vector3 combineAllBehaviors()
    { //Reuse the combine() method but this time for leaders sphereBoids.

        return boidconfig.WeightSmoothing * birdFlightSmoothing();

    }
	
}
