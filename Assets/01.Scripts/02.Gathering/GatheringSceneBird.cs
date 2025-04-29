using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringSceneBird : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * 0.005f;

        if(transform.position.x < -23)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
    }
}
