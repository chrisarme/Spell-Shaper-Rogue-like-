using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTextScript : MonoBehaviour {

    public Text text;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnValueChanged(float newVal)
    {
        //Debug.Log(newVal);
        text.text = newVal.ToString("n" + 2);
    }
}
