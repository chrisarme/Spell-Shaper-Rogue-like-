using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Sprite defaultSprite;
    public Sprite damageSprite;

    double health;

    //float timer;
    //bool isDamaged;
    //int currentSprite;

    //SpriteRenderer spriteRend;

    // Use this for initialization
    void Start ()
    {
        health = 100;
        //timer = 0;
        //isDamaged = false;
        //currentSprite = 0;

        //spriteRend = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckHealth();
        //Debug.Log("Hit!");

        

        /*if (isDamaged == true)
        {
            //Debug.Log(timer);

            timer += Time.deltaTime;
            if (timer > 1f)
            {
                isDamaged = false;
                spriteRend.sprite = defaultSprite;
            }
            else
            {
                if (Time.frameCount % 10 == 0)
                {
                    if (currentSprite == 0)
                    {
                        spriteRend.sprite = damageSprite;
                        currentSprite = 1;
                    }
                    else if (currentSprite == 1)
                    {
                        spriteRend.sprite = defaultSprite;
                        currentSprite = 0;
                    }
                }
            }
        }*/
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // check if it is magic
        if (collider.gameObject.layer == 9)
        {
            Debug.Log("Hit!");
            health -= 10;

            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(collider.GetComponent<Rigidbody2D>().velocity.x / 3, collider.GetComponent<Rigidbody2D>().velocity.y / 3);
            Destroy(collider.gameObject);

            //Debug.Log(health);
            //timer = 0;
            //isDamaged = true;
        }
    }

    void CheckHealth()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
