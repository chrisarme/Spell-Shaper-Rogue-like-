using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControls : MonoBehaviour {

    public GameObject[] pauseObjects;
    public GameObject playerObject;

    bool debugMenuUp = false;
    MagicControls magicControls;

	// Use this for initialization
	void Start ()
    {
        Time.timeScale = 1;
        //pauseObjects = GameObject.FindGameObjectsWithTag("PauseMenu");

        magicControls = playerObject.GetComponent<MagicControls>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (debugMenuUp)
            {
                closeMenu();
            }
            else if (!debugMenuUp)
            {
                openMenu();
            }
        }
	}

    void openMenu()
    {
        foreach (GameObject obj in pauseObjects)
        {
            obj.SetActive(true);
        }

        Time.timeScale = 0;
        debugMenuUp = true;
    }

    void closeMenu()
    {
        Time.timeScale = 1;
        debugMenuUp = false;

        foreach (GameObject obj in pauseObjects)
        {
            obj.SetActive(false);
        }
    }

    public void closeDebugMenuAndSave()
    {
        closeMenu();

        //Then new stuff
        //Player.instance
        foreach (GameObject obj in pauseObjects)
        {
            if (obj.name == "SizeSlider")
            {
                //magicControls.ChangeMagicSpeed(obj.GetComponent<Slider>);
            }
        }
    }
}
