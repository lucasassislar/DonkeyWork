using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DialogueTrigger : MonoBehaviour
{
    public UnityEvent Activatedialogue;
    public UnityEvent Deactivatedialogue;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider c)
    {
        Debug.Log("colidio");
        if (c.transform.tag == "Player")
        {
            Activatedialogue.Invoke();
            Debug.Log("colidio");
        }
     
    }
    void OnTriggerExit(Collider c)
    {
        if (c.transform.tag == "Player")
        {
            Deactivatedialogue.Invoke();
        }

    }



}
