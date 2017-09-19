using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    private Rigidbody2D spikes;
    // Use this for initialization
    void Start()
    {
        this.spikes = this.gameObject.GetComponent<Rigidbody2D>();
        this.spikes.velocity = Vector2.up * 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            Application.LoadLevel("Main");
        }
        //Physics2D.IgnoreCollision(this.spikesCollider, collision.collider);
        //if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Floor")
        //{
        //    Physics2D.IgnoreCollision(this.spikesCollider, collision.collider);
        //}
    }
}
