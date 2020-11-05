using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBG : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float startPosition = 22.13f;
    [SerializeField] private float endPosition = -16.05f;


    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= endPosition)
        {
            transform.position = new Vector3(0f, startPosition, 0f);
        }
    }

    public void slideBackground()
    {
        transform.Translate(new Vector3(0f, speed * Time.deltaTime * -1, 0f));
    }
}
