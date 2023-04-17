using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{
    private GameManager gameManager;
    private Camera cam;
    private Vector3 mousePos;
    private TrailRenderer trail;
    private BoxCollider col;

    private bool swipping = false;

    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        trail = GetComponent<TrailRenderer>();
        col = GetComponent<BoxCollider>();
        trail.enabled = false;
        col.enabled = false;

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swipping = true;
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0)) 
            {
                swipping = false;
                UpdateComponents();
            }

            if (swipping)
            {
                UpdateMousePosition();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Target>())
        {
            collision.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }

    void UpdateMousePosition()
        {
            mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            transform.position = mousePos;
        }

    void UpdateComponents()
    {
        trail.enabled = swipping;
        col.enabled = swipping;
    }
}
