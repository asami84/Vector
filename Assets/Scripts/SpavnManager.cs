using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpavnManager : MonoBehaviour
{
    public float MaxSpavnDis;
    public GameObject EnemyPrefab;
    private int Dificl=1;
    public GameObject[] Bonuses;
    public GameObject Player;
    private Player PlayerScript;

    // Start is called before the first frame update
    void Start()
    {
        //спавним игрока
        SpawnEnemyWave();
        PlayerScript=Player.GetComponent<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        int PowerNomber=FindObjectsOfType<Bonus>().Length;
        // print(PowerNomber);
        // print(PlayerScript.isPowerBon);
        // print(PlayerScript.isPowerWall);
        // print(PlayerScript.isPowerScale);
        if (PowerNomber<1 && PlayerScript.isPowerBon==false && PlayerScript.isPowerScale==false && PlayerScript.isPowerWall==false)
        {
            print("спавн бонуса");
            SpawnPowerBon();
        }
        GameObject[] Enemy=GameObject.FindGameObjectsWithTag("Enemy");
        int EnemyNomber=Enemy.Length;
        print(Dificl);
        // int EnemyNomber=FindObjectsOfType<Enemy>().Length;
        if (EnemyNomber<1)
        {
            Dificl+=1;
            SpawnEnemyWave();
        }
        if (Player.transform.position.y<-25)
        {
            Dificl=0;
        }
    }
    private Vector3 RandomPos()
    {
        float SpavnPosX=Random.Range(-MaxSpavnDis,MaxSpavnDis);
        float SpavnPosZ=Random.Range(-MaxSpavnDis,MaxSpavnDis);
        Vector3 SpavnPos=new Vector3(SpavnPosX,2,SpavnPosZ);
        return SpavnPos;
    }
    void SpawnEnemyWave()
    {
        for(int i=0;i<Dificl;i+=1)
        {
            Instantiate(EnemyPrefab,RandomPos(),transform.rotation);
        }
    }
    void SpawnPowerBon()
    {
        int Index=Random.Range(0,Bonuses.Length);
        Instantiate(Bonuses[Index],RandomPos(),Bonuses[Index].transform.rotation);
    }
}
