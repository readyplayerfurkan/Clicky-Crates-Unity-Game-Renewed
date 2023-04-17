using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Components assignments.
    private Rigidbody targetRb;
    private GameManager gameManager;
    public ParticleSystem explotionParticle;

    // Variables.
    private float minSpeed = 12.0f;
    private float maxSpeed = 16.0f;
    private float maxTorque = 10.0f;
    private float xSpawnPos = 4.0f;
    private float ySpawnPos = -3.0f;
    public int pointValue;
    

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();       
    }

    // Update is called once per frame
    void Update()
    {

    }

    //private void OnMouseDrag()
    //{
    //    if (gameManager.isGameActive)
    //    {
    //        if (!gameObject.CompareTag("Random"))
    //        {
    //            gameManager.UpdateScore(pointValue);
    //        }
    //        else
    //        {
    //            int randomScore = Random.Range(10, 50);
    //            gameManager.UpdateScore(randomScore);
    //        }

    //        Destroy(gameObject);
    //        Instantiate(explotionParticle, transform.position, explotionParticle.transform.rotation);
    //    }
    //}

    // Trigger method for sensor. It reduces live by one and destory game object if object isn't a bad target and game is active when the method is called.
    private void OnTriggerEnter(Collider other)
    {
        if (!gameObject.CompareTag("Bad") && gameManager.isGameActive)
        {
            gameManager.UpdateLives(-1);
        }
        Destroy (gameObject);
    }

    // This method destroy game object and instantiate explotion particle if game is active.
    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explotionParticle, transform.position, explotionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }
    
    // It generates random force through y axis.
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    // It generates random torque.
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    // It generates random spawn position through x and y axis.
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xSpawnPos, xSpawnPos), ySpawnPos);
    }
}
