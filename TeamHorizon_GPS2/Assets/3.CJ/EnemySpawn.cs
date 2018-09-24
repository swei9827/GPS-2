using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum ENEMY_TYPE
{
    ENEMY1,
    ENEMY2,
    ENEMY3
};

[System.Serializable]
public class EnemySpawn : MonoBehaviour
{
    public GameObject Enemies;
    public List<Enemy> enemy = new List<Enemy>();
    Vector3 vector;

    void Start()
    {
        
    }

    void Update()
    {
        for(int i=0; i<enemy.Count; i++)
        {
            Debug.Log(enemy[i].enemyType);
            Debug.Log(enemy[i].speedy);
            Instantiate(Enemies,new Vector3(0,1,enemy[i].posZ),this.transform.rotation);
            enemy.RemoveAt(i);
        }
    }
}


[System.Serializable]
public class Enemy
{
    public ENEMY_TYPE enemyType;
    public int hp;
    public int speedX;
    public int speedZ;
    public int speedy;
    public int posZ;
}


