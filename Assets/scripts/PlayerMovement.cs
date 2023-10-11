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

    public AudioClip soundEffect;
    public AudioClip soundEffect2;
    public AudioClip soundEffect3;
    public AudioClip soundEffect4;
    public AudioClip soundEffect5;

    private bool enterAllowed;
    private string sceneToLoad;

    public GameObject keyObject;
    public GameObject doorObject;
    public GameObject nokeyText;
    public GameObject havekeyText;
    public GameObject insideText;

    private bool hasKey = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with" + collision.gameObject.name);
        if (collision.GetComponent<bushdoor>())
        {
            sceneToLoad = "start";
            enterAllowed = true;
            AudioSource.PlayClipAtPoint(soundEffect3, transform.position);
        }

        if (collision.gameObject.name == "key"){
            AudioSource.PlayClipAtPoint(soundEffect,transform.position);

            // AudioSource soundManager = GameObject.Find("SoundManager").GetComponent<AudioSource>();
            //soundManager.Play();
           // AudioSource soundManager = GameObject.Find("key").GetComponent<AudioSource>();
            //soundManager.Play();
            Debug.Log("Sound Played");
            keyObject.SetActive(false);
            hasKey = true;


        }

        if (collision.gameObject.name == "door") //&& (!keyObject.activeSelf))
        {
            if (!hasKey)
            {
                doorObject.SetActive(true);
                AudioSource.PlayClipAtPoint(soundEffect4, transform.position);

            }
            else
            {
                doorObject.SetActive(false);
                AudioSource.PlayClipAtPoint(soundEffect2, transform.position);

            }
        }

        // { doorObject.SetActive(false);  } 
        if (!hasKey)
        {
            if ((collision.gameObject.tag == "npc_outside"))
            {

                nokeyText.SetActive(true);
                InvokeRepeating("Hide", 2.0f, 0f);
                AudioSource.PlayClipAtPoint(soundEffect5, transform.position);
            }
        }
        else
        {
            if ((collision.gameObject.tag == "npc_outside"))
            {

                havekeyText.SetActive(true);
                InvokeRepeating("Hide", 2.0f, 0f);
                AudioSource.PlayClipAtPoint(soundEffect5, transform.position);
            }
        }



            if ((collision.gameObject.tag == "npc_inside"))
        {

            insideText.SetActive(true);
            InvokeRepeating("Hide", 2.0f, 0f);
            AudioSource.PlayClipAtPoint(soundEffect5, transform.position);
        }
    }
    private void Hide()
    {
        nokeyText.SetActive(false);
        havekeyText.SetActive(false);
        insideText.SetActive(false);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        enterAllowed = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key is pressed.");
        }
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


