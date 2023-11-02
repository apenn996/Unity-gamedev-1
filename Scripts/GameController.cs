using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] public static int health = 50;
    [SerializeField] public float scalingFactor=3.5f;
    private float tempS;
    [SerializeField] public int jumpIntensity=10;
    [SerializeField] public int timer = 30;
    [SerializeField] private Text Timertext;
    [SerializeField] private Text Leveltext;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject winPanel;
    [SerializeField] public GameObject slowTimePanel;
    [SerializeField] public GameObject losePanel;
    [SerializeField] public GameObject invincibilityPanel;
    [SerializeField] public GameObject activePanel;
    private GameObject player;
    //private float lowestHeight;
    //[SerializeField] private int health;
    // Start is called before the first frame update
    void Start()
    {
        //winPanel = GameObject.FindGameObjectWithTag("WinPanel");
        // lowestHeight = gameObject.transform.position.y;
        int level = SceneManager.GetActiveScene().buildIndex + 1;
        Leveltext.text = "Level: " + level;
        player = GameObject.FindGameObjectWithTag("Player");
        scalingFactor = PlayerPrefs.GetFloat("PlayerSpeed", (float)3.5);
        health = PlayerPrefs.GetInt("PlayerHealth", 50);
        StartCoroutine("CountDown");
    }
    
    // Update is called once per frame
    void Update()
    {
             
        healthBar.transform.localScale = new Vector3(health/50f, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        if (health <= 0)
        {
            Time.timeScale = 0;
            activePanel.SetActive(false);
            losePanel.SetActive(true);
            
            //GameOver();
        }
        //scalingFactor += ((float).0001);
       // timer -= Time.deltaTime;
    }

    IEnumerator CountDown()
    {
        
        while (timer > 0)
        {
            Timertext.text = "Timer: " + timer;
            yield return new WaitForSeconds(1);
            timer--;
        }
        Timertext.text = "Timer: 0";
        GameObject.FindGameObjectWithTag("Player").GetComponent<EndlessRunnerCharacterController>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = true;
        winPanel.SetActive(true);
       
        yield return new WaitForSeconds(5);
        winPanel.SetActive(false);
        
        ChangeLevel();
        yield return new WaitForSeconds(4);
    }

    private void ChangeLevel()
    {
       
        
        int level = SceneManager.GetActiveScene().buildIndex + 1;
        if (level == 4)
            scalingFactor = 2;
        PlayerPrefs.SetFloat(("PlayerSpeed"), scalingFactor + 1);
        PlayerPrefs.SetInt(("PlayerHealth"), health);
        SceneManager.LoadScene((level) % 4);
        

        //(SceneManager.GetActiveScene().buildIndex + 0) % 0

    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        tempS = scalingFactor;

        GameObject.FindGameObjectWithTag("Player").GetComponent<EndlessRunnerCharacterController>().enabled = false;

    }
    public void ResumeGame()
    {
       
       
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("Player").GetComponent<EndlessRunnerCharacterController>().enabled = true;
        scalingFactor = tempS;

    }
    public void GameOver()
        {

#if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }
    public void resetGame()
    {
        PlayerPrefs.DeleteKey("PlayerHealth");
        PlayerPrefs.DeleteKey("PlayerSpeed");
        health = 50;
        scalingFactor = 3;
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        activePanel.SetActive(true);
        losePanel.SetActive(false);
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("PlayerHealth");
        PlayerPrefs.DeleteKey("PlayerSpeed");
    }
}
