using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MagicControls : MonoBehaviour
{
    public GameObject[] magicPrefabs;
    public Transform magicSpawn;

    int magicPrefabNum = 1;

    public Text cooldownText;

    float magicSpeed = 15f;
    float magicSize = .75f;
    int magicCount = 3;

    bool canFire = true;
    float magicCooldownTime = 1;
    float timeStamp;

	// Use this for initialization
	void Start ()
    {
        timeStamp = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        DisplayCooldown();
        CanUseMagic();

        if (Input.GetButtonDown("Fire1") && Time.timeScale != 0)
        {
            //Fire();
            Fire2();
        }
	}

    void Fire()
    {
        //...setting shoot direction
        Vector2 shootDirection;
        Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 myPos = new Vector2(magicSpawn.position.x, magicSpawn.position.y);
        shootDirection = target - myPos;
        shootDirection.Normalize();

        // Create the Bullet from the Bullet Prefab
        GameObject magic = Instantiate(magicPrefabs[magicPrefabNum], magicSpawn.position, Quaternion.Euler(new Vector3(0, 0, 0)));

        // Change bullet size
        magic.transform.localScale = new Vector3(magicSize, magicSize, magicSize);

        // Add velocity to the bullet
        magic.GetComponent<Rigidbody2D>().velocity = shootDirection * magicSpeed;

        // Destroy the bullet after 2 seconds
        Destroy(magic, 2.0f);
    }

    void Fire2()
    {
        if (canFire == true)
        {
            BeginCooldown();

            //...setting shoot direction
            Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector2 myPos = new Vector2(magicSpawn.position.x, magicSpawn.position.y);
            float radius = (Mathf.Pow(target.x - myPos.x, 2) + Mathf.Pow(target.y - myPos.y, 2));

            List<Vector2> targets = new List<Vector2>();
            List<Vector2> shootDirections = new List<Vector2>();
            List<GameObject> magicBullets = new List<GameObject>();

            Vector2 shootDirection = target - myPos;
            //shootDirection.Normalize();

            float angleDifference = Mathf.PI / 2 / magicCount;

            if (magicCount % 2 == 0)
            {
                //even
                for (int i = 0; i < magicCount; i++)
                {
                    float theta = Mathf.Atan2(target.y - myPos.y, target.x - myPos.x);

                    //Vector2 targetPoint = new Vector2(myPos.x + (radius * Mathf.Cos(theta + ((-Mathf.PI / 4 - (i * angleDifference))))), myPos.y + (radius * Mathf.Sin(theta + ((-Mathf.PI / 4 - (i * angleDifference))))));
                    // WORKING
                    Vector2 targetPoint = new Vector2(myPos.x + (radius * Mathf.Cos(theta + (((angleDifference * (.5f + i)))) - (Mathf.PI / 4))), myPos.y + (radius * Mathf.Sin(theta + (((angleDifference * (.5f + i)))) - (Mathf.PI / 4))));
                    //Vector2 targetPoint = new Vector2(myPos.x + (radius * Mathf.Sin(theta + (((angleDifference * (.5f + i)))))), myPos.y + (radius * Mathf.Cos(theta + (((angleDifference * (.5f + i)))))));

                    //targets.Add(point);
                    shootDirections.Add(targetPoint - myPos);
                    shootDirections[i].Normalize();


                    // Create the Bullet from the Bullet Prefab
                    magicBullets.Add(Instantiate(magicPrefabs[magicPrefabNum], magicSpawn.position, Quaternion.Euler(new Vector3(0, 0, 0))));

                    // Change bullet size
                    magicBullets[i].transform.localScale = new Vector3(magicSize, magicSize, magicSize);

                    // Add velocity to the bullet
                    magicBullets[i].GetComponent<Rigidbody2D>().velocity = shootDirections[i].normalized * magicSpeed;

                    // Change direction of bullet to direction
                    float rot_z = Mathf.Atan2(shootDirections[i].y, shootDirections[i].x) * Mathf.Rad2Deg;
                    magicBullets[i].GetComponent<Rigidbody2D>().transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

                    // Destroy the bullet after 2 seconds
                    Destroy(magicBullets[i], 2.0f);
                }
            }
            else
            {
                //odd
                for (int i = 0; i < magicCount; i++)
                {
                    float theta = Mathf.Atan2(target.y - myPos.y, target.x - myPos.x);

                    //Vector2 targetPoint = new Vector2(myPos.x + (radius * Mathf.Cos(theta + ((-Mathf.PI / 4 - (i * angleDifference))))), myPos.y + (radius * Mathf.Sin(theta + ((-Mathf.PI / 4 - (i * angleDifference))))));
                    Vector2 targetPoint = new Vector2(myPos.x + (radius * Mathf.Cos(theta + (((angleDifference * (.5f + i)))) - (Mathf.PI / 4))), myPos.y + (radius * Mathf.Sin(theta + (((angleDifference * (.5f + i)))) - (Mathf.PI / 4))));

                    //targets.Add(point);
                    shootDirections.Add(targetPoint - myPos);
                    //shootDirections[i].Normalize();

                    // Create the Bullet from the Bullet Prefab
                    magicBullets.Add(Instantiate(magicPrefabs[magicPrefabNum], magicSpawn.position, Quaternion.Euler(new Vector3(0, 0, 0))));

                    // Change bullet size
                    magicBullets[i].transform.localScale = new Vector3(magicSize, magicSize, magicSize);

                    // Add velocity to the bullet
                    magicBullets[i].GetComponent<Rigidbody2D>().velocity = shootDirections[i].normalized * magicSpeed;

                    // Change direction of bullet to direction
                    float rot_z = Mathf.Atan2(shootDirections[i].y, shootDirections[i].x) * Mathf.Rad2Deg;
                    magicBullets[i].GetComponent<Rigidbody2D>().transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

                    // Destroy the bullet after 2 seconds
                    Destroy(magicBullets[i], 2.0f);
                }
            }
            //Vector2 shootDirection;
            //shootDirection = target - myPos;
            //shootDirection.Normalize();

            // Create the Bullet from the Bullet Prefab
            //GameObject magic = Instantiate(magicPrefab, magicSpawn.position, Quaternion.Euler(new Vector3(0, 0, 0)));

            //// Change bullet size
            //magic.transform.localScale = new Vector3(magicSize, magicSize, magicSize);

            //// Add velocity to the bullet
            //magic.GetComponent<Rigidbody2D>().velocity = shootDirection * magicSpeed;

            //// Destroy the bullet after 2 seconds
            //Destroy(magic, 2.0f);
        }
    }

    void CanUseMagic()
    {
        if (Mathf.CeilToInt(magicCooldownTime - (Time.time - (timeStamp - magicCooldownTime))) <= 0)
        {
            
            canFire = true;
        }
    }

    public void ChangeMagicSpeed(float speed)
    {
        magicSpeed = speed;
    }

    public void ChangeMagicSize(float size)
    {
        magicSize = size;
    }

    public void ChangeMagicCount(float count)
    {
        magicCount = (int)count;
    }

    public void DisplayCooldown()
    {
        if (canFire == false)
        {
            int cooldown = Mathf.CeilToInt(magicCooldownTime - (Time.time - (timeStamp - magicCooldownTime)));

             cooldownText.text = cooldown.ToString();
        }
        else if (canFire == true)
        {
            cooldownText.enabled = false;
        }
    }

    public void BeginCooldown()
    {
        timeStamp = Time.time + magicCooldownTime;
        cooldownText.enabled = true;
        canFire = false;
    }

    public void ChangeMagicTypeDEBUG(int index)
    {
        magicPrefabNum = index;
    }
}
