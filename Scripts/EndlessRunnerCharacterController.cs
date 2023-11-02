using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessRunnerCharacterController : MonoBehaviour
{
   // static float scalingFactor = 1;
   // [SerializeField] private GameObject floor;
    private double LogNum;
    [SerializeField] private GameController gameController;
    private bool isGrounded = true;
    Vector3 myVector = new Vector3(0,0,0);
    private bool isSlowed=false;
    private bool isBigger= false;
    private bool isInvincible = false;

    // Start is called before the first frame updates
    //start awake and update are three default methods unity calls on to start a script? 
    private void Awake()//right at first frame before game starts
    {
       // Debug.Log("Awake - time is: " + Time.time);
    }
    void Start()//happens for each component
    {
        //gameController.scalingFactor = 1f;
        myVector = gameObject.transform.position;
      //  Debug.Log("Start - time is: " + Time.time);
    }

    // Update is called once per frame
     void Update() //called once per frame for every script in a scene?
    {
        // gameController.scalingFactor+=(float).0001/60;
        //gameController.scalingFactor += (float).0001;
        //myVector.z += (float).001*(scalingFactor*scalingFactor);
       
        transform.Translate(new Vector3((float)(Input.GetAxis("Horizontal") *.1), 0,  gameController.scalingFactor * Time.fixedDeltaTime * 1.5f));
        //gameObject.transform.position = new Vector3((float)(Input.GetAxis("Horizontal") *.1),Input.GetAxis("Vertical"), (float)(LogNum *.01)); //bad? way of translating an object
        if (Input.GetKeyDown(KeyCode.Space)  && isGrounded == true)
        {
            Debug.Log("space pressed");
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 90, ForceMode.Impulse);
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.S) && isGrounded == false)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * 500, ForceMode.Impulse);
            
        }
        // Debug.Log("Update - time is: " + Time.deltaTime); //deltaTime shows the difference in time between the last frame and current frame
        //Debug.Log("Update - position is: " + GameObject.FindGameObjectWithTag("Player").transform.position);
       
        }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ObstacleC" && isInvincible == false) 
        {
            Debug.Log("obstacle");
            GameController.health -= 10; 
        }
        if(other.gameObject.tag == "healthPack" && GameController.health < 50)
        {
            Debug.Log("healthpack");
            GameController.health += 10;
        }
        if(other.gameObject.tag == "slowBuff" && isSlowed == false)
        {
            StartCoroutine("slowTime");
        }
        if (other.gameObject.tag == "gainSize" && isBigger == false)
        {
            StartCoroutine("gainSize");
        }
        if (other.gameObject.tag == "invincibilityStar" && isInvincible == false)
        {
            StartCoroutine("invincibilityStar");
        }
    }
            
    

    IEnumerator slowTime()
    {
        isSlowed = true;
        Time.timeScale = 0.5f;
        float temp = gameController.scalingFactor;
        gameController.scalingFactor = gameController.scalingFactor / (float)2.5;
        gameController.slowTimePanel.SetActive(true);
        yield return new WaitForSeconds(5);
        gameController.slowTimePanel.SetActive(false);
        Time.timeScale = 1;
        gameController.scalingFactor = temp;
        isSlowed = false;
}

    IEnumerator gainSize()
    {
        isBigger = true;
       Vector3 temp = gameObject.transform.localScale;
        gameObject.transform.localScale += new Vector3(2, 2, 2);
       
        yield return new WaitForSeconds(5);
        gameObject.transform.localScale = temp;
        isBigger = false;
    }
    IEnumerator invincibilityStar()
    {
        isInvincible= true;
        
        gameController.invincibilityPanel.SetActive(true);
        yield return new WaitForSeconds(5);
        gameController.invincibilityPanel.SetActive(false);
       
        isInvincible = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            //Debug.Log("collision enter");
            isGrounded = true;
            //Debug.Log(isGrounded);
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            isGrounded = false;
        }
    }
    void FixedUpdate() //called once per frame for every script in a scene at a consistant interval?
    {
       // Debug.Log("Update - time is: " + Time.deltaTime); //deltaTime shows the difference in time between the last frame and current frame
    }
}
