using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTail : MonoBehaviour
{
    /*
     * Script for create ray in the bellow main camera (camera tail)
     * Ray is needed to check box, and then destroy it
     */

    [SerializeField] private int numberOfRays = 2;
    [SerializeField] private float rayLength = 5f;
    [SerializeField] private CameraHead cameraHead;
    private SpriteRenderer background;
    private float raySpace;


    // Start is called before the first frame update
    void Start()
    {
        background = GameObject.FindGameObjectWithTag("Background").GetComponent<SpriteRenderer>();
        raySpace = background.size.x / (numberOfRays - 1);
    }

    // Update is called once per frame
    void Update()
    {
        verticalRay();
    }

    void verticalRay()
    {
        Vector2 originPosition = transform.position;

        for(int i = 0; i < numberOfRays; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(originPosition + Vector2.right * raySpace * i, Vector2.up * -1, rayLength);
            Debug.DrawRay(originPosition + Vector2.right * raySpace * i, Vector2.up * -1 * rayLength, Color.red);
            if(hit.collider != null)
            {
                // hit something
                if(hit.collider.tag == "droppedBox")
                {
                    // hit drop box
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

}
