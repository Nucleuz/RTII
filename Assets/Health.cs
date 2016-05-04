using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    Text txt;
    public Player player;

	// Use this for initialization
	void Start () {
        txt = GetComponent<Text>();
        
	
	}
	
	// Update is called once per frame
	void Update () {

        txt.text = "Health: " + player.healthPoints.ToString();
	}
}
