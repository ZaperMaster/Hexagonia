using UnityEngine;
using System.Collections;

public class starBehaviour : astronomicalObject {

    void Start()
    {
        reDim(Random.Range(2, 5));
    }

    //public GameObject explosion;
    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag.Equals("Sun") && collision.transform.lossyScale.x > transform.lossyScale.x)
    //    { }
    //    else
    //    {
    //        Instantiate(explosion, collision.contacts[0].point, transform.rotation);
    //        Destroy(collision.gameObject);
    //    }
    //}

}
