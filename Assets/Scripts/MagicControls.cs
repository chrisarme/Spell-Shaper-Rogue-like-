using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MagicControls : MonoBehaviour
{
    public GameObject[] magicPrefabs;
    public Transform magicSpawn;

    // The array size of 4 comes from the number of spells the user can use
    int[] magicPrefabNum = new int[4];

    public Text[] cooldownText;

    float magicSpeed = 15f;
    float magicSize = .75f;
    int magicCount = 3;

    bool[] canFire = new bool[4];
    float magicCooldownTime = 1;
    float[] timeStamp = new float[4];

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            timeStamp[i] = Time.time;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (cooldownText.Length > 4)
        {
            Debug.LogError("cooldownText size is greater than 4!!!");
        }

        DisplayCooldown();
        CanUseMagic();

        if (Input.GetButtonDown("MainFire") && Time.timeScale != 0)
        {
            Fire(0);
        }
        else if (Input.GetButtonDown("MoveFire") && Time.timeScale != 0)
        {
            Fire(1);
        }
	}

    void Fire(int indexOfSpell)
    {
        // indexOfSpell is the number in the array of player spells 
        // 0 = base spell (mouse1), 1 = movement spell (space), 2 = misc spell 1 (q), 3 = misc spell 2 (e / mouse2)
        if (canFire[indexOfSpell] == true)
        {
            BeginCooldown(indexOfSpell);

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
                    magicBullets.Add(Instantiate(magicPrefabs[magicPrefabNum[indexOfSpell]], magicSpawn.position, Quaternion.Euler(new Vector3(0, 0, 0))));

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
                    magicBullets.Add(Instantiate(magicPrefabs[magicPrefabNum[indexOfSpell]], magicSpawn.position, Quaternion.Euler(new Vector3(0, 0, 0))));

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
        for (int i = 0; i < 4; i++)
        {
            if (magicCooldownTime - (Time.time - (timeStamp[i] - magicCooldownTime)) <= 0)
            {
                canFire[i] = true;
            }
        }
    }

    public void ChangeMagicSpeed(float speed)
    {
        magicSpeed = speed;
        // modify speed based on type, size, and extra speed (maybe more)
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
        for (int i = 0; i < cooldownText.Length; i++)
        {
            if (canFire[i] == false)
            {
                float cooldown = Mathf.Round((magicCooldownTime - (Time.time - (timeStamp[i] - magicCooldownTime))) * 10f) / 10f;
                Debug.Log(i);
                cooldownText[i].text = cooldown.ToString();
            }
            else if (canFire[i] == true)
            {
                cooldownText[i].enabled = false;
            }
        }
    }

    public void BeginCooldown(int index)
    {
        timeStamp[index] = Time.time + magicCooldownTime;
        cooldownText[index].enabled = true;
        canFire[index] = false;
    }

    public void ChangeMagicTypeDEBUG(int index)
    {
        // this needs to change!
        magicPrefabNum[0] = index;
    }
}
