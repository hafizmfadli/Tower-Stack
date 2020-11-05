using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject boxPrefab;
    [SerializeField] private GameManagement gameManagement;
    [SerializeField] private Vector3[] localwayPoints;
    [SerializeField] private Vector2 rangeMovePosition;
    [SerializeField] private float speed = 10f;
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private float acceleration = 1f;
    private Vector3[] globalwayPoints;
    private int fromWaypointIdx;
    private float percentBetweenWaypoints;

    private GameObject currBox;
    private Box currBoxScript;
    private bool isBoxLanded;
    private bool hasAccelerated = true;
    private int currentHeight = 0;

    // Start is called before the first frame update
    void Start()
    {
        instantiateBox();

        // create waypoints
        globalwayPoints = new Vector3[localwayPoints.Length];
        for(int i = 0; i < localwayPoints.Length; i++)
        {
            globalwayPoints[i] = localwayPoints[i] + transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseControl.isGamePaused)
        {
            

            if (gameManagement.getCurrentHeight() > 0 && gameManagement.getCurrentHeight() % 10 <= 2)
            {
                // accelerate spawner

                if(currentHeight != gameManagement.getCurrentHeight())
                {
                    // condition to prevent accelerate multiple times in current height
                    hasAccelerated = false;
                }

                if (!hasAccelerated)
                {
                    accelerateMovement();
                    hasAccelerated = true;
                    currentHeight = gameManagement.getCurrentHeight();
                    //Debug.Log("accelerate");
                    //Debug.Log("Current speed : " + speed);
                }
            }

            transform.Translate(calculateSpawnerVelocity());
            if (currBoxScript.isBoxLanding())
            {
                instantiateBox();
            }

        }       
    }
    
    void instantiateBox()
    {
        currBox = Instantiate(boxPrefab, transform.position, Quaternion.identity);
        currBox.transform.SetParent(this.transform);
        currBoxScript = currBox.GetComponent<Box>();
    }


    void accelerateMovement()
    {
        if(speed <= 30)
        {
            speed += acceleration * Time.deltaTime;
        }

    }

    Vector3 calculateSpawnerVelocity()
    {
        /*
         * This is to calculate velocity each time
         * Code from : Sebastian Lague , modify by Hafiz Muhammad Fadli
         * 
         */

        fromWaypointIdx %= globalwayPoints.Length;
        int toWaypointIdx = (fromWaypointIdx + 1) % globalwayPoints.Length;

        // buat waypoint mengikuti ketinggian spawner
        globalwayPoints[fromWaypointIdx].y = transform.position.y;
        globalwayPoints[toWaypointIdx].y = transform.position.y;

        float distanceBetween = Vector3.Distance(globalwayPoints[fromWaypointIdx], globalwayPoints[toWaypointIdx]);
        percentBetweenWaypoints += Time.deltaTime * speed / distanceBetween;

        percentBetweenWaypoints = Mathf.Clamp01(percentBetweenWaypoints);

        // interpolation between to points
        Vector3 newPos = Vector3.Lerp(globalwayPoints[fromWaypointIdx], globalwayPoints[toWaypointIdx], percentBetweenWaypoints);

        if(percentBetweenWaypoints >= 1)
        {
            percentBetweenWaypoints = 0;
            fromWaypointIdx++;
        }

        return newPos - transform.position;
    }


    private void OnDrawGizmos()
    {
        /*
         * Draw waypoint gizmos
         */

        if(localwayPoints != null)
        {

            float size = 0.3f;
            for(int i = 0; i < localwayPoints.Length; i++)
            {
                Vector3 globalwayPoint = (Application.isPlaying) ? globalwayPoints[i] : localwayPoints[i] + transform.position;
                Debug.DrawLine(globalwayPoint - Vector3.up * size, globalwayPoint + Vector3.up * size, Color.red);
                Debug.DrawLine(globalwayPoint - Vector3.right * size, globalwayPoint + Vector3.right * size, Color.red);

            }
        }
    }


}
