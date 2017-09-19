using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Rigidbody2D Player_RB;
    public GameObject leftBtn;
    public GameObject rightBtn;
    public GameObject jumpBtn;
    enum Jump {Up=1, Right=2, Left=3, None=0}
    Jump jump = Jump.Up;
    float minX;
    float maxX;
    float midY;
    Vector3 cameraOriginalPosiiton;

	// Use this for initialization
	void Start () {
        Player_RB = this.gameObject.GetComponent<Rigidbody2D>();
        float vertExtent = Camera.main.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;
        cameraOriginalPosiiton = Camera.main.transform.position;
        maxX = horzExtent;
        minX = (-1) * horzExtent;
        midY = Camera.main.orthographicSize / 2;
    }
	
	// Update is called once per frame
	void Update () {
        this.CheckPosition();
        this.MoveCamera();
        this.TouchMove();
        //this.WASDMove();

    }

    void WASDMove()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Player_RB.AddForce(15 * Vector2.left);
        }

        if (Input.GetKey(KeyCode.W))
        {
            switch (jump)
            {
                case Jump.Up:
                    Player_RB.velocity = Vector2.zero;
                    Player_RB.velocity = (5 * Vector2.up);
                    break;
                case Jump.Left:
                    Player_RB.velocity = Vector2.zero;
                    Player_RB.velocity = ((30 * Vector2.left) + (5 * Vector2.up));
                    //Player_RB.AddForce(100 * Vector2.left);
                    //Player_RB.AddForce(200 * Vector2.up);
                    break;
                case Jump.Right:
                    Player_RB.velocity = Vector2.zero;
                    Player_RB.velocity = ((30 * Vector2.right) + (5 * Vector2.up));
                    //Player_RB.AddForce(100 * Vector2.right);
                    //Player_RB.AddForce(200 * Vector2.up);
                    break;
                default:
                    break;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            Player_RB.AddForce(15 * Vector2.right);
        }
    }

    void TouchMove()
    {
        foreach (Touch touch in Input.touches)
        {
            Ray raycast = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            if (Physics.Raycast(raycast, out hit))
            {
                if (hit.collider.CompareTag("Left"))
                {
                    Player_RB.AddForce(15 * Vector2.left);
                }
                else if (hit.collider.CompareTag("Right"))
                {
                    Player_RB.AddForce(15 * Vector2.right);
                }
                else if (hit.collider.CompareTag("Jump"))
                {
                    switch (jump)
                    {
                        case Jump.Up:
                            Player_RB.velocity = Vector2.zero;
                            Player_RB.velocity = (5 * Vector2.up) + Player_RB.velocity;
                            break;
                        case Jump.Left:
                            Player_RB.velocity = Vector2.zero;
                            Player_RB.velocity = ((3 * Vector2.left) + (5 * Vector2.up));
                            //Player_RB.AddForce(100 * Vector2.left);
                            //Player_RB.AddForce(200 * Vector2.up);
                            break;
                        case Jump.Right:
                            Player_RB.velocity = Vector2.zero;
                            Player_RB.velocity = ((3 * Vector2.right) + (5 * Vector2.up));
                            //Player_RB.AddForce(100 * Vector2.right);
                            //Player_RB.AddForce(200 * Vector2.up);
                            break;
                        default:
                            break;
                    }
                }
            }
        }        
    }

    void CheckPosition()
    {
        if (Player_RB.transform.position.x > maxX)
        {
            float playerY = Player_RB.transform.position.y;
            float playerZ = Player_RB.transform.position.z;
            Player_RB.transform.position = new Vector3(minX, playerY, playerZ);
        }
        else if (Player_RB.transform.position.x < minX)
        {
            float playerY = Player_RB.transform.position.y;
            float playerZ = Player_RB.transform.position.z;
            Player_RB.transform.position = new Vector3(maxX, playerY, playerZ);
        }
    }
    
    void MoveCamera()
    {
        if (Player_RB.transform.position.y + (float)2 > midY)
        {
            float move = Player_RB.transform.position.y + (float)2 - midY;
            if (move < cameraOriginalPosiiton.y)
            {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, cameraOriginalPosiiton.y, -10);
            }
            else
            {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, move, -10);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            jump = Jump.Up;
        }
        else if (collision.collider.tag == "Platform")
        {
            jump = this.CheckJump(collision.contacts[0].normal);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            jump = 0;
        }
        else if (collision.collider.tag == "Platform")
        {
            jump = 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            jump = Jump.Up;
        }
        else if (collision.collider.tag == "Platform")
        {
            jump = this.CheckJump(collision.contacts[0].normal);
        }
    }

    private Jump CheckJump(Vector2 player)
    {
        if (player.y > 0)
            return Jump.Up;
        else if (player.y < 0)
            return Jump.None;
        else if (player.x < 0)
            return Jump.Left;
        else if (player.x > 0)
            return Jump.Right;
        else
            return Jump.None;
    }
}
