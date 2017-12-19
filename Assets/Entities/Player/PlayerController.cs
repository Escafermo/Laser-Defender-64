using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 15.0f;
    public float padding = 0.55f;
    public GameObject laser;
    public float laserSpeed;
    public float fireRate;
    public float playerHealth;
    public AudioClip playerFire;
    public AudioClip playerDestroy;
    public float loadDelay = 0.01f;

    private ScoreKeeper scoreKeeper;

    float xmin;
    float xmax;

    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;

        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }
	
    void Fire()
    {
        GameObject projectile = Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, laserSpeed, 0);
        AudioSource.PlayClipAtPoint(playerFire, transform.position);
    }

	void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }  else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire",0.00000001f,fireRate);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            playerHealth -= missile.GetDamage();
            missile.Hit();
            if (playerHealth <= 0)
            {
                Die();
                LoadTheLevel();
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(playerDestroy, transform.position);
    }

    public void LoadTheLevel()
    {
        Debug.Log("Load fuck");
        LevelManager manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        manager.LoadLevel("Win");
    }
}
