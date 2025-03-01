using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject Player;
    private Rigidbody RB;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Player=GameObject.Find("Player");
        RB=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 PlayerPos=Player.transform.position;
        Vector3 EnemyPos=transform.position;
        Vector3 Direction=PlayerPos-EnemyPos;//направление от врага к игроку
        Direction=Direction.normalized;//длину вектора направления уменьшаем до 1
        RB.AddForce(Direction*speed);
        if(transform.position.y<-30)
        {
            Destroy(gameObject);
            
        }
    }
}
