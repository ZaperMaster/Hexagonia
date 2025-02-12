using UnityEngine;
using System.Collections;

public class sateliteBehaviour : astronomicalObject {

    bool planetFound = false;
    float timer = 0;
    float starSearchInterval = 0.5f;

    // Use this for initialization
    void Start()
    {
        reDim();
    }

    void FixedUpdate()
    {
        if (myStar == null) { planetFound = false; distance = float.MaxValue; }

        if (!planetFound) planetFound = lookForAStar("Planet");
        else
        {
            transform.position = new Vector3(transform.position.x, myStar.position.y, transform.position.x);
            transform.position = nextPositionInMyOrbit(myStar.position, transform.position, orbitationVelocity);
            // this part is used to change orbit if the planet is near a star that is not its star
            if (timer <= 0) { lookForAStar("Planet"); timer += starSearchInterval; }
            else timer -= Time.deltaTime;
        }
    }

    //public GameObject explosion;
    //void OnCollisionEnter(Collision collision)
    //{
    //        Instantiate(explosion, collision.contacts[0].point, transform.rotation);
    //        Destroy(this.gameObject);
    //}
    

}
