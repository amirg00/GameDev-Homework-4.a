using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This component destroys its object whenever it triggers a 2D collider with the given tag.
 */
public class DestroyOnTrigger2D : MonoBehaviour {
    [Tooltip("Every object tagged with this tag will trigger the destruction of this object")]
    [SerializeField] string triggeringTag;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == triggeringTag && enabled) {
            Destroy(other.gameObject);
            
            // Player collides with target
            if (!this.CompareTag("Player"))
            {
                Destroy(this.gameObject);
                return;
            }
            
            // Player got no spare lives
            if (Health.healthNum == 1)
            {
                Health.healthNum--;
                Destroy(this.gameObject);
            }
            else
            {
                Health.healthNum--;
            }
            
        }
    }

    private void Update() {
        /* Just to show the enabled checkbox in Editor */
    }
}
