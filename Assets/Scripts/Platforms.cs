using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour {
    // Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Floor")
        {
            Rigidbody2D platform = this.gameObject.GetComponent<Rigidbody2D>();
            //platform.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public void setPosition()
    {
        float vertExtent = Camera.main.orthographicSize + Camera.main.transform.position.y + 2;
        float horzExtent = (Camera.main.orthographicSize * Screen.width / Screen.height) - 1;
        float num = Random.Range((-1) * horzExtent, horzExtent);
        this.gameObject.transform.position = new Vector3(num, vertExtent, -2);
    }
}
