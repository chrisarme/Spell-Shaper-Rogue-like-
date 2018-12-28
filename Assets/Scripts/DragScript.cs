using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private bool selectIsDown = false;
    private bool isInside = false;
    private Vector3 startPos;

    private bool overLapping = false;
    private GameObject slotGameObject;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        checkSelectDown();

        if (isInside && selectIsDown)
        {
            //transform.SetPositionAndRotation(Input.mousePosition, transform.rotation);
            Vector3 mouseToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseToWorld.z = 0;
            transform.position = mouseToWorld + startPos;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isInside = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isInside = false;
    }

    void checkSelectDown()
    {
        if (Input.GetButtonDown("Interact"))
        {
            selectIsDown = true;
            startPos = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPos.z = 0;
        }

        if (Input.GetButtonUp("Interact"))
        {
            selectIsDown = false;
            shouldSnapToSlot();
        }
    }

    void OnCollisionEnter(Collision collider)
    {
        slotGameObject = collider.gameObject;
        overLapping = true;

        Debug.Log("In");
    }

    void OnCollisionExit(Collision collider)
    {
        overLapping = false;
        Debug.Log("Out");
    }

    void shouldSnapToSlot()
    {
        if (overLapping == true)
        {
            transform.position = slotGameObject.transform.position;
        }
    }
}
