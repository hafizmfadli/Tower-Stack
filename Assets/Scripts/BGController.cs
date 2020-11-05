using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{
    public CameraTail cameraTail;
    private float upperBoundPos;

    private void Awake()
    {
        cameraTail = FindObjectOfType<CameraTail>();
    }

    // Start is called before the first frame update
    void Start()
    {
        upperBoundPos = transform.position.y + GetComponent<SpriteRenderer>().size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(upperBoundPos < cameraTail.transform.position.y)
        {
            // destroy all box who haved not present in the screen again
            // We do this in order for optimization memory
            Destroy(gameObject);
        }
        
    }
}
