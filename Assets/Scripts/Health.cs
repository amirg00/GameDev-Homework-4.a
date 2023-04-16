using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static int healthNum = 3;
    [Tooltip("Hearts images for player's lives")]
    [SerializeField] private Image[] hearts;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update the lives images according to remaining amount of lives
        for (var i = 0; i < hearts.Length; i++)
        {
            if (i >= healthNum)
            {
                hearts[i].enabled = false;
            }
        }
    }
}
