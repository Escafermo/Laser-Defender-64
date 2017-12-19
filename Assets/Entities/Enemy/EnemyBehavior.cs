using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

    public float health;
    public GameObject laser;
    public float laserSpeed;
    public float enemyFireRate;
    public int scoreValue;
    public AudioClip enemyFire;
    public AudioClip enemyDestroy;

    private ScoreKeeper scoreKeeper;

    private void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
        scoreKeeper.Score(scoreValue);
        AudioSource.PlayClipAtPoint(enemyDestroy, transform.position);
    }

    void Fire()
    {
        Vector3 offset = new Vector3(0, -0.5f, 0);
        Vector3 firePos = transform.position + offset;
        GameObject projectile = Instantiate(laser, firePos, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, laserSpeed, 0);
        AudioSource.PlayClipAtPoint(enemyFire, transform.position);
    }

    void Update()
    {
        float probabilityFire = Time.deltaTime * enemyFireRate;
        if(Random.value < probabilityFire)
        {
            Fire();
        }
    }
}
