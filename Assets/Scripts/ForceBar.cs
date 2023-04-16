using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBar : MonoBehaviour
{
    private Vector3 localScale;
    private float _maxForce;
    private float _maxBarX;
    
    // Start is called before the first frame update
    void Start()
    {
        _maxBarX = transform.localScale.y; 
        localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);

        // Set scale to the new localScale.
        transform.localScale = localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = localScale;
    }

    public void SetMaxForce(float force)
    {
        this._maxForce = force;
    }

    // Gets current time force and translate the x of the scale accordingly.
    public void SetForce(float currForce)
    {
        if (currForce > _maxForce)
        {
            return;
        }
        float relation = currForce / _maxForce;
        localScale.y = relation * _maxBarX;
    }
}
