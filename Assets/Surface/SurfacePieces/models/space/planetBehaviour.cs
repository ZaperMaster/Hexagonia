using UnityEngine;
using System.Collections;

public class planetBehaviour : astronomicalObject {

    bool starFound = false;
    float timer = 0;
    float starSearchInterval = 0.5f;

    // Use this for initialization
    void Start()
    {
        reDim();
    }

    void FixedUpdate () {
        if (myStar == null) { starFound = false; distance = float.MaxValue; }

        if (!starFound) starFound = lookForAStar("Star");
        else
        {
            transform.position = new Vector3(transform.position.x, myStar.position.y, transform.position.x);
            transform.position = nextPositionInMyOrbit(myStar.position, transform.position, orbitationVelocity);
            // this part is used to change orbit if the planet is near a star that is not its star
            if (timer <= 0) { lookForAStar("Star"); timer += starSearchInterval; }
            else timer -= Time.deltaTime;
        }
	}

    //public GameObject explosion;
    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag.Equals("Sun") || collision.gameObject.tag.Equals("Planet"))
    //    {            
    //        Instantiate(explosion, collision.contacts[0].point, transform.rotation);
    //        Destroy(this.gameObject);
    //    }
    //}
    
}
