using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    /*
     * Script for moving the camera
     */

    [SerializeField] private float moveAmount = 10f;
    [SerializeField] private float speed = 2f;
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = new Vector3(0f, transform.position.y + moveAmount, -1f);
    }

    public void slideUp()
    {
        StartCoroutine(moveVertically());
        targetPosition = new Vector3(0f, transform.position.y + moveAmount, -1f);

    }

    IEnumerator moveVertically()
    {
        /*
         * Coroutines for move camera vertically
         * 
         */

        float percent = 0;
        Vector3 originalPosition = transform.position;

        while(percent <= 1)
        {

            percent += speed * Time.deltaTime;
            float interpolation = percent;
            transform.position = Vector3.Lerp(originalPosition, targetPosition, interpolation);
            yield return null;
        }

       
    }

    public float getHeight()
    {
        return transform.position.y;
    }

   
}
