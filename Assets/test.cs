using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Wall")){
            Debug.Log("wall");
        }
        if (collider.CompareTag("Target")){
            Debug.Log("Target");
        }
    }
}
