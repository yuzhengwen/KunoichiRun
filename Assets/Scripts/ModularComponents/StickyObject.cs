using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyObject : MonoBehaviour
{
    Transform originalParent;
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        originalParent = collision.gameObject.transform.parent;
        collision.gameObject.transform.SetParent(transform);
    }
    private void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        collision.gameObject.transform.SetParent(originalParent);
    }
}
