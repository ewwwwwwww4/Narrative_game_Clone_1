using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{

    public GameObject door;
    private void OnTriggerEnter2D(Collider2D  col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("picking");
            door.SetActive(false);

            this.gameObject.SetActive(false);
        }
    }
}
