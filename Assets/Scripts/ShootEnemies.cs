using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemies : MonoBehaviour
{
    public List<GameObject> enemiesInRange;

    private float lastShotTime;
    private MonsterData monsterData;

    // Start is called before the first frame update
    void Start()
    {
        enemiesInRange= new List<GameObject>();
        lastShotTime = Time.time;
        monsterData= gameObject.GetComponentInChildren<MonsterData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }

    void Shoot(Collider2D target)
    {
        GameObject bulletPrefab = monsterData.CurrentLevel.bullet;
        Vector3 startPosition = gameObject.transform.position;
        Vector3 targetPosition = target.transform.position;
        startPosition.z = bulletPrefab.transform.localPosition.z;
    }
}
