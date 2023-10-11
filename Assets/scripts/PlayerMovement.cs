using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator anim;
    private Vector2 moveDirection;
    private Vector2 lastMoveDirection;


    private bool enterAllowed;
    private string sceneToLoad;

    public GameObject keyObject;
    public GameObject doorObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with" + collision.gameObject.name);
        if (collision.GetComponent<bushdoor>())
        {
            sceneToLoad = "start";
            enterAllowed = true;
        }

       // if (collision.gameObject.name == "key"){
          //  keyObject.SetActive(false); }


        if (collision.gameObject.name == "door") 
        { if (!keyObject.activeSelf)
              {     doorObject.SetActive(false);  } }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        enterAllowed = false;
    }


    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Animate();

        if (enterAllowed)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if ((moveX == 0 && moveY == 0) && moveDirection.x != 0 || moveDirection.y != 0)
        {
            lastMoveDirection = moveDirection;
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    void Animate()
    {

        anim.SetFloat("AnimMoveX", moveDirection.x);
        anim.SetFloat("AnimMoveY", moveDirection.y);
        anim.SetFloat("AnimMoveMagnitude", moveDirection.magnitude);
        anim.SetFloat("AnimLastMoveX", lastMoveDirection.x);
        anim.SetFloat("AnimLastMoveY", lastMoveDirection.y);
    }
}


