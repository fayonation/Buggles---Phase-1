using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBar : MonoBehaviour
{
    private Transform bar;
    // Start is called before the first frame update
    private void Start()
    {
    }

    public void setSize(float sizeNormalized) {
        if(!bar){
            bar = transform.Find("bar");
        }
        
        bar.localScale = new Vector3(1f, sizeNormalized);
    } 
}
