using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ForceClickSpawner : MonoBehaviour
{    
    [SerializeField] NumberField scoreField;
    [SerializeField] protected InputAction spawnAction = new InputAction(type: InputActionType.Button);
    [SerializeField] protected GameObject prefabToSpawn;
    [SerializeField] protected float velocityOfSpawnedObject;
    [SerializeField] protected ForceBar forceBar;
        
    private float MAX_FORCE_TIME = 3f;
    private float _holdDownStartTime;

    private void Start()
    {
        forceBar = GetComponentInChildren<ForceBar>();
        forceBar.SetMaxForce(MAX_FORCE_TIME);
    }

    void OnEnable()  {
        spawnAction.Enable();
    }

    void OnDisable()  {
        spawnAction.Disable();
    }

    protected virtual GameObject spawnObject(float rotationDegree, Vector3 direction) {
        Debug.Log("Spawning a new object");
        
        // Step 1: spawn the new object.
        Vector3 positionOfSpawnedObject = transform.position;  // span at the containing object position.
        Debug.Log("Current Degree: " + rotationDegree);
        Quaternion rotationOfSpawnedObject = Quaternion.Euler(0, 0, -rotationDegree); // no rotation.
        GameObject newObject = Instantiate(prefabToSpawn, positionOfSpawnedObject, rotationOfSpawnedObject);

        // Modify the text field of the new object.
        ScoreAdder newObjectScoreAdder = newObject.GetComponent<ScoreAdder>();
        if (newObjectScoreAdder)
            newObjectScoreAdder.SetScoreField(scoreField);
        
        // Step 2: modify the velocity of the new object.
        DirectionMover newObjectMover = newObject.GetComponent<DirectionMover>();
        if (newObjectMover) {
            newObjectMover.SetVelocity(velocityOfSpawnedObject);
            newObjectMover.SetDirection(direction);
        }

        return newObject;
    }

    private void Update()
    {
        // Measure time before holding the key
        if (spawnAction.WasPressedThisFrame())
        {
            _holdDownStartTime = Time.time;
            Debug.Log("Start at: " + _holdDownStartTime);
        }
        
        // Set player shooting force according to the current time
        if (spawnAction.IsPressed())
        {
            Debug.Log("Middle at: " + (Time.time - _holdDownStartTime));
            forceBar.SetForce(Time.time - _holdDownStartTime);
        }
        
        // Key is released and the force is reset
        if (spawnAction.WasReleasedThisFrame())
        {
            forceBar.SetForce(0f);
            float holdDownTime = Time.time - _holdDownStartTime;
            int timeForceNormalized = (int)(holdDownTime);
            Debug.Log("End at: " + timeForceNormalized);

            int spwanObjectsAmount = 1;

            // Deciding the amount of objects to be spawned by the force time.
            if (timeForceNormalized == 0)
            {
                return;
            }
            else if (timeForceNormalized == 1)
            {
                spwanObjectsAmount = 2;
            }
            else if (timeForceNormalized == 2)
            {
                spwanObjectsAmount = 4;
            }
            else if (timeForceNormalized >= MAX_FORCE_TIME)
            {
                spwanObjectsAmount = 6;
            }
            Shoot(spwanObjectsAmount);
        }
        
    }
    
    /**
     * The function shoots lasers according to a given amount of wanted lasers.
     */
    private void Shoot(int laserAmount)
    {
        float startAngle = -90f;
        float endAngle = 90f;
        float angleStep = (endAngle - startAngle) / laserAmount;
        float currAngle = startAngle;
        
        // Shoot the laser according to the degree
        for (int i = 0; i < laserAmount; i++)
        {
            float laserDirX = Mathf.Sin((currAngle * Mathf.PI) / 180f);
            float laserDirY = Mathf.Cos((currAngle * Mathf.PI) / 180f);

            Vector3 direction = new Vector3(laserDirX, laserDirY, 0f);
            spawnObject(currAngle, direction);
            currAngle += angleStep;
        }
    }
}
