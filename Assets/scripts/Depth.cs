using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depth : MonoBehaviour
{

    SpriteRenderer tempRend;

    float timer = 3;
    // Start is called before the first frame update

    void Awake()
    {
        tempRend = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempRend.sortingOrder = (int)Camera.main.WorldToScreenPoint(this.transform.position).y * -1;
    }
}
