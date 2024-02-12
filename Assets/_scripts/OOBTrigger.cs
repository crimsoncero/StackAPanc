using System;
using UnityEngine;

public class OOBTrigger : MonoBehaviour
{
    public event Action PancakeFallen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pancake")
        {
            Debug.Log("boom");
            Destroy(collision.transform.parent.gameObject);
            PancakeFallen?.Invoke();
        }
    }
}
