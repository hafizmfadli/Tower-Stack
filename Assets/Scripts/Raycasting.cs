using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting : MonoBehaviour
{
    [SerializeField] private int numberOfRay = 8;
    [SerializeField] private float rayLength;
    [SerializeField] private CameraMovement maincamera;
    private float spaceHorizontal;
    private SpriteRenderer background;


    // Start is called before the first frame update
    void Start()
    {
        background = GameObject.FindGameObjectWithTag("Background").GetComponent<SpriteRenderer>();
        float lengthX = background.size.x;
        spaceHorizontal = lengthX / (numberOfRay - 1);
    }

    // Update is called once per frame
    void Update()
    { 
        // create vertical ray in order to detect box
        verticalRay();
    }

    void verticalRay()
    {
        /*
         * Create N Vertical Ray with X space for each distance between ray
         */

        Vector2 initialPosition = new Vector2(transform.position.x, transform.position.y);
        for(int i = 0; i < numberOfRay; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(initialPosition +  Vector2.right * spaceHorizontal * i, Vector2.up * -1, rayLength);
            Debug.DrawRay(initialPosition + Vector2.right * spaceHorizontal * i ,Vector2.up * rayLength * -1, Color.red);
            if (hit.collider != null)
            {
                if(hit.collider.tag == "droppedBox")
                {
                    //Debug.Log("Hit dropped box cuk !");
                    maincamera.slideUp();  
                }
            }
        }
    }

    

}
