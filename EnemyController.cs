using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb; 
    private Vector2 position;
    public float moveSpeed = 5.0f;
    private float horizontal = 0f;
    private float vertical = 0f;
    public int maxHealth = 5;
    private int currentHealth;
	public GameObject capsule;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
			//SceneManager.LoadScene(2);
			Instantiate(capsule);
            Destroy(gameObject);
        }
        position = rb.position;
        if (position.x >= 8.0f && position.y >= 3.5 && horizontal == 0f)
        {
            horizontal = -1.0f;
            vertical = 0.0f;
        }
        else if (position.x <= -8.0f && position.y <= -1.5f && horizontal == 0f)
        {
            horizontal = 1.0f;
            vertical = 0.0f;
        }
        else if (position.x <= -8.0f && position.y >= 3.5f && vertical == 0f)
        {
            horizontal = 0.0f;
            vertical = -1.0f;
        }
        else if (position.x >= 8.0f && position.y <= -1.5f && vertical == 0f)
        {
            horizontal = 0.0f;
            vertical = 1.0f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        position = rb.position;
        position.x = position.x + moveSpeed * horizontal * Time.deltaTime;
        position.y = position.y + moveSpeed * vertical * Time.deltaTime;
        rb.MovePosition(position);
    }
    
    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        // Debug.Log(currentHealth + "/" + maxHealth);
    }
}
