using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {
    //public GameObject Platform;
    public GameObject smallPlatform;
    public GameObject spikes;
    public GameObject start;
    private bool startFlag = false;

    private float timeLeft = 2;
	// Use this for initialization
	void Start () {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update() {
        if (startFlag == false)
        { 
            foreach (Touch touch in Input.touches)
            {
                Ray raycast = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(raycast, out hit))
                {
                    if (hit.collider.CompareTag("Start"))
                    {
                        Time.timeScale = 1;
                        startFlag = true;
                        this.start.SetActive(false);
                    }
                }
            }
        }
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            if (Random.Range(1, 3) > 1)
            {
                GameObject platform_clone = (GameObject)Instantiate(smallPlatform);
                platform_clone.GetComponent<Platforms>().setPosition();
                Physics2D.IgnoreCollision(this.spikes.GetComponent<Collider2D>(), platform_clone.GetComponent<Collider2D>());
            }
            else
            {
                GameObject small_platform_clone = (GameObject)Instantiate(smallPlatform);
                small_platform_clone.GetComponent<Platforms>().setPosition();
                Physics2D.IgnoreCollision(this.spikes.GetComponent<Collider2D>(), small_platform_clone   .GetComponent<Collider2D>());
            }
            timeLeft = 1;
        }

    }
}
