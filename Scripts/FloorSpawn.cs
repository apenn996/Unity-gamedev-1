using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawn : MonoBehaviour
{
    GameObject player;
    [SerializeField] private GameObject Obstacle;
    [SerializeField] private GameObject ObstacleW;
    [SerializeField] private GameObject ObstacleM;
    [SerializeField] private GameObject healthPack;
    [SerializeField] private GameObject gainSize;
    [SerializeField] private GameObject slowBuff;
    [SerializeField] private GameObject invincibilityStar;
    private bool icreated = false;
    private Vector3 nextPlatformPos = Vector3.zero;
    private Vector3 nextObstaclePos = Vector3.zero;
    
    private Vector3 nextBuffPos = Vector3.zero;
    [SerializeField] private GameObject Floor;
    [SerializeField] private float distanceThreshold;
    private Vector3 myVector;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Instantiate(Floor, new Vector3(0,0,0), Quaternion.identity);
        nextPlatformPos += new Vector3(Random.Range(0, 5), 0, Random.Range(40,50));
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Vector3.Distance(nextPlatformPos,player.transform.position) < distanceThreshold)
        {
            GameObject e = new GameObject();
            //Debug.Log("Close Enough");
            GameObject plat = Instantiate(Floor, nextPlatformPos, Quaternion.identity);
            Buffs(e);

            e.transform.parent = plat.transform;
            
            GameObject obsM = Instantiate(ObstacleM, new Vector3(0, 10, 25) + nextPlatformPos, Quaternion.identity);
            obsM.transform.parent = e.transform;
            GameObject obsW = Instantiate(ObstacleW, new Vector3(-7, 0, Random.Range((-4), 4)) + nextPlatformPos, Quaternion.identity);
            obsW.transform.parent = e.transform;
            obsW = Instantiate(ObstacleW, new Vector3(7, 0, Random.Range((-4), 4)) + nextPlatformPos, Quaternion.identity);
            obsW.transform.parent = e.transform;
            for (int i = 0; i < 6; i++)
            {
                nextObstaclePos = new Vector3(Random.Range(-4, 4), Random.Range((float).5, 5), Random.Range((i*-5), i*5)) + nextPlatformPos;
                GameObject obs = Instantiate(Obstacle, nextObstaclePos, Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
                obs.transform.parent = e.transform;
            }
            
        
                nextPlatformPos += new Vector3(Random.Range(-5, 5), Random.Range(-2, 2), 50);
         
            
        } //Random.Range(-2, 3)
       // invincibilityStar.transform.Rotate(0, 0, 50 * Time.deltaTime);
        //slowBuff.transform.Rotate(0, 0, 50 * Time.deltaTime);
        //gainSize.transform.Rotate(0, 0, 50 * Time.deltaTime);

    }

    private void Buffs(GameObject e)
    {
        nextBuffPos = new Vector3(Random.Range(-3, 3), Random.Range(2, 3), Random.Range((-10), 10)) + nextPlatformPos;
        int rand = Random.Range(0, 4);
        switch (rand){
            case 0:
                 GameObject hp = Instantiate(healthPack, nextBuffPos, Quaternion.identity);
                hp.transform.parent = e.transform;
                break;
            case 1:
                //nextBuffPos.y++;
               GameObject st = Instantiate(slowBuff, nextBuffPos, Quaternion.Euler(new Vector3(90, 0, 0)));
                st.transform.parent = e.transform;
               
                break;
            case 2:
                GameObject gs = Instantiate(gainSize, nextBuffPos, Quaternion.Euler(new Vector3(0, 0, 0)));
                gs.transform.parent = e.transform;
                break;
            case 3:
              //  Debug.Log("IN INV: " + rand);
                GameObject iStar = Instantiate(invincibilityStar, nextBuffPos, Quaternion.Euler(new Vector3(0, 0, 0)));
                iStar.transform.parent = e.transform;
                
                break;
            default:
                break;
        }
    }
}
