using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float projectileSpeed = 0.1f;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position + new Vector3(projectileSpeed, 0, 0);

        if (transform.position.x > 20)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("obstacle"))
        {
            Debug.Log("YOU HIT AN OBJECT!");
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
