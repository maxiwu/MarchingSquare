using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour {

    //[SerializeField] GameObject movee;
    [SerializeField] public float radius = 0.5f;

    private void OnMouseDown()
    {        

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            //Debug.Log("Pressed left click.");
            Vector3 clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(Input.mousePosition);
            //Debug.Log(clickedPosition);
            transform.position = new Vector3(clickedPosition.x, clickedPosition.y, transform.position.z);

            //CalculateMeatball(clickedPosition, radius);
        }
    }

    private void CalculateMeatball(Vector3 clickedPosition, float radius)
    {
        throw new NotImplementedException();
    }
}
