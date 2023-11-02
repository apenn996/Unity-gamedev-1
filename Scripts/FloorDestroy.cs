using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorDestroy : MonoBehaviour
{
    [SerializeField] private float distanceThreshold = 70;
        private bool loadingNew = false;
   
    private GameObject player;
    
    void Start()
    {
        
        
        distanceThreshold = 50f;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(player.transform.position.z + " " + gameObject.transform.position.z + " " + distanceThreshold);
        if (player.transform.position.z - gameObject.transform.position.z > distanceThreshold)
        {
            
            Destroy(gameObject);
        }
        if (player.transform.position.y + gameObject.transform.position.y < -40  && loadingNew == false)
        {
            loadingNew = true;
            Debug.Log("yep: " + GameController.health + ", time is: " + Time.deltaTime);
            
            GameController.health -= 10;
            ResetLevel();
            
        }
 
    }

    private void ResetLevel()
    {
        Debug.Log("yep2: " + GameController.health + ", time is: " + Time.deltaTime);
        PlayerPrefs.SetInt(("PlayerHealth"), GameController.health);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        loadingNew = false;
        //(SceneManager.GetActiveScene().buildIndex + 0) % 0

    }
}
