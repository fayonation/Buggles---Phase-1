using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drops : MonoBehaviour
{
	public GameObject theDrop;

    private void OnDestroy() {
        if(theDrop != null)
            Instantiate(theDrop, transform.position, transform.rotation);
    }
}
