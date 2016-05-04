using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public float moveForce = 365f;
	public float moveSpeed = 25f;
    public Vector2 jumpForce = new Vector2(0, 200);
	private Rigidbody2D rb2d;
    public float vPos;

    public bool hitByObject = false;

    public GameObject projectile;

    public int healthPoints = 5;

    public float buzzerTimer;

    private float x;

    // Use this for initialization
    void Start()
    {
        buzzerTimer = 0.1f;
		rb2d = GetComponent<Rigidbody2D> ();
        rb2d.position = new Vector2(2, 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

        // Move the player by calculating with moveSpeed
		rb2d.velocity = new Vector2 (h * moveSpeed, v * moveSpeed);

        if (hitByObject)
        {
            Buzz();
        }
    }

    public void Jump()
    {
        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(jumpForce);
    }

    public void Shoot()
    {
        GameObject g = (GameObject)Instantiate(projectile, new Vector3(transform.position.x+1.4f, transform.position.y, transform.position.z), Quaternion.identity);

        g.transform.parent = GameObject.Find("ProjectileContainer").transform;
    }

    public int DecreaseHealthPoints()
    {
        healthPoints--;
        return healthPoints;
    }

    void Buzz()
    {
        buzzerTimer -= Time.deltaTime;
        Debug.Log(buzzerTimer);

        if(buzzerTimer <= 0)
        {
            hitByObject = false;
            buzzerTimer = 0.1f;
        }
    }

    public void SetVerticalPos(float input)
    { 
        vPos = (input / 113.7f) * -1;
        transform.position = new Vector2(0, vPos);
    }

    public float GetVerticalPos()
    {
        return vPos;
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("obstacle"))
        {
            DecreaseHealthPoints();
            hitByObject = true;
            Buzz();
            Destroy(other.gameObject);
        }
    }

    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    Die();
    //}

    //void Die()
    //{
    //    Application.LoadLevel(Application.loadedLevel);
    //}
}
