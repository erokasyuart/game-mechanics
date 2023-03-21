using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 10;
    public int damage;
    public GameObject target;
    public Vector3 startPosition;
    public Vector3 targetPosition;
    private Vector3 normaliseDirection;
    private GameManagerBehaviour gameManager;
    // Start is called before the first frame update
    void Start()
    {
        normaliseDirection = (targetPosition - startPosition).normalized; //creates a standard direction vector
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManagerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += normaliseDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        target = other.gameObject;
        if (target.tag.Equals("Enemy"))
        {
            Transform healthBarTransform = target.transform.Find("HealthBar");
            HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>(); //gets the script
            healthBar.currentHealth -= damage;

            if (healthBar.currentHealth <= 0)
            {
                Destroy(target);
                AudioSource audioSource = target.GetComponent<AudioSource>();
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
                gameManager.Gold += 50;
            }
            Destroy(gameObject);
        }
    }
}
