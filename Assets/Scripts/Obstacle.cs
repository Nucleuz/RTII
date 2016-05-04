using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    public Player player;
    public bool hitAnObject = false;

    public Vector2 velocity = new Vector2(-4, 0);
	public float range;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
		range = Random.Range(-5f,5f);
        GetComponent<Rigidbody2D>().velocity = velocity;
        transform.position = new Vector3(transform.position.x+10, transform.position.y + range, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
		if (transform.position.x < -20) {
			Destroy (this.gameObject);
		}
    }

	

    
}
