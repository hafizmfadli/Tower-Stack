using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHead : MonoBehaviour
{
    [SerializeField] private GameObject bgPrefab;
    [SerializeField] private float distanceThreshold = 2.0f;
    private float bgUpperBound;
    private float bgRadius;
    private GameObject newBG;
    private bool hasInstantiateNewBG;
    public List<GameObject> listOfBG;

    private void Awake()
    {
        listOfBG = new List<GameObject>();
        Instantiate(bgPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        bgRadius = bgPrefab.GetComponent<SpriteRenderer>().size.y / 2;
        bgUpperBound = bgPrefab.transform.position.y + bgRadius;
        listOfBG.Add(bgPrefab);
        hasInstantiateNewBG = false;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.y - bgUpperBound) <= distanceThreshold)
        {
            // jarak antara camera head dengan upperbound bg telah memnuhi threshold
            if (!hasInstantiateNewBG)
            {
                newBG = Instantiate(bgPrefab, new Vector3(0f, bgUpperBound + bgRadius, 0f), Quaternion.identity);
                listOfBG.Add(newBG);
                hasInstantiateNewBG = true;
            }
            
        }

        if(transform.position.y > bgUpperBound)
        {
            
            // camerahead telah berada pada background yang baru

            // update bgupperbound
            bgUpperBound = newBG.transform.position.y + bgRadius;
            
            hasInstantiateNewBG = false;
        }
        
    }
}
