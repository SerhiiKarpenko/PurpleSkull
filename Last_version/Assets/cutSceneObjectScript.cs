using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class cutSceneObjectScript : MonoBehaviour
{

    private UnityEvent onTriggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "player")
            onTriggerEnter.Invoke();
    }

}
