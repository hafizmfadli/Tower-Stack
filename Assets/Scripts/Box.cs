using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Box : MonoBehaviour
{
    public event System.Action dropEvent;
    [SerializeField] private float dropSpeed = 6.0f;
    private bool isDropped;
    private bool isFall;
    private Rigidbody2D rb;
    private LayerMask boxLandedLayer;
    private GameManagement gameManager;
    private bool hasPlayingSound;
    private AudioSource sfx;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sfx = GetComponent<AudioSource>();
       
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManagement>();
        isDropped = false;
        isFall = false;
        
        // We won't box have dynamic rigidbody because
        // we wont physics simulation (physics simulation
        // can make stack of box collapse)
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseControl.isGamePaused)
        {
            if (!isDropped)
            {
                // we only can instantiate new box if last box has dropped
                if (Input.GetMouseButtonDown(0))
                {
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        transform.parent = null;
                        rb.isKinematic = false;
                        StartCoroutine(dropBox());
                    }
                }
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // collision detection between box

        isDropped = true;
        sfx.Play();
        //Debug.Log("Coliision detected");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // trigger for detecting falling box

        isFall = true;
        //Debug.Log("Trigger enter");
        gameManager.displayGameOverUI();
        Destroy(gameObject);

    }

    IEnumerator dropBox()
    {
        // Coroutine for makes box falling down
        while (!isDropped)
        {
            transform.Translate(0f, -dropSpeed * Time.deltaTime, 0f);
            yield return null;
        }
        myBoxDropped();
    }


    void myBoxDropped()
    {
        // set tag for dropped box for detecting
        // height of tower using raycasting,
        // this is to determine whether camera move or not
        gameObject.tag = "droppedBox";

        // set rigidbody to static to avoid tower collapse
        rb.bodyType = RigidbodyType2D.Static;
        if(dropEvent != null)
        {
            dropEvent();

        }
    }

    public bool isBoxLanding()
    {
        return isDropped;
    }
}