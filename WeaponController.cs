using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject BulletSpawner;
    private SpriteRenderer _renderer;
    private Animator animate;
	public Sprite gunNormal;
	public Sprite gunUpSprite;
	public Sprite gunDownSprite;
	public GameObject bullet;

    private float horizontal;
    private float vertical;
    private float fire;
    private bool firing = false;
    
    private bool right = false;
    private bool left = false;
    private bool up = false;
    private bool down = false;

	private bool spawnRight = false;
    private bool spawnLeft = false;
    private bool spawnUp = false;
    private bool spawnDown = false;

	public float extra = 0.4f;
    private float extraX = 0.2f;
    private float extraY = 0.2f;
    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        animate = GetComponent<Animator>();

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        fire = Input.GetAxis("Fire1");
        
        if (horizontal > 0){
            right = true;
        }
        else if (horizontal < 0){
            left = true;
        }
        else if (vertical > 0){
            up = true;
        }
        else if (vertical < 0){
            down = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        fire = Input.GetAxis("Fire1");
		Vector2 spawnPosition = this.transform.position;
        
        if (right || horizontal > 0){
            // Right
			if(_renderer.sprite != gunNormal)
			{
				_renderer.sprite = gunNormal;
			}
            _renderer.flipX = true;
        }
        else if (left || horizontal < 0){
            // Left
			spawnPosition.x = spawnPosition.x - extraX -.1f;
			if(_renderer.sprite != gunNormal)
			{
				_renderer.sprite = gunNormal;
			}
            _renderer.flipX = false;
        }
        else if (up || vertical > 0){
            // Up
			spawnPosition.x = spawnPosition.x - extraX;
			spawnPosition.y = spawnPosition.y + extraY + .1f;
			_renderer.sprite = gunUpSprite;
        }
        else if (down || vertical < 0){
            // Down
			spawnPosition.x = spawnPosition.x - extraX;
			_renderer.sprite = gunDownSprite;
        }
		BulletSpawner.transform.position = spawnPosition;
    }

	void FixedUpdate()
    {
		horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 position = BulletSpawner.transform.position;
        position.x = position.x + 4.0f * horizontal * Time.deltaTime;
        position.y = position.y + 4.0f * vertical * Time.deltaTime;
		
		
        if (fire >= 1f && firing == false)
        {
			
            // Debug.Log(fire);
			Vector3 bulletPosistion = new Vector3(position.x + extraX, position.y + extraY, 0f);
            Instantiate(bullet, bulletPosistion, BulletSpawner.transform.rotation);
            firing = true;
        }
        else if (fire == 0f)
        {
            firing = false;
        }
    }    
}
