using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class holdingController : MonoBehaviour
{
   
    Camera cam;
    GameObject camParent;
    public bool shootMode = true;
    // GameObject player;
    float defaultYMinLimit;
    float defaultYMaxLimit;
    float defaultcamDistance;

    // Start is called before the first frame update
    void Start()
    {
        if(cam == null){
            cam = Camera.main;
            camParent = cam.transform.parent.gameObject;
            // player = camParent.GetComponent<cameraController>().target.gameObject;
            defaultcamDistance = camParent.GetComponent<cameraController>().distance;
            defaultYMaxLimit = camParent.GetComponent<cameraController>().yMaxLimit;
            defaultYMinLimit = camParent.GetComponent<cameraController>().yMinLimit;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.parent.GetComponent<playerController>().isHolding){
            if(shootMode){                
                gameObject.transform.rotation = camParent.transform.rotation;
                camParent.GetComponent<cameraController>().distance = -2;
                camParent.GetComponent<cameraController>().yMinLimit = -60;
                camParent.GetComponent<cameraController>().yMaxLimit = 60;
                camParent.GetComponent<cameraController>().target = gameObject.transform.GetChild(1).Find("bulletOrigin");
            } else{
                camParent.GetComponent<cameraController>().target = gameObject.transform.parent.gameObject.transform;
                camParent.GetComponent<cameraController>().distance = defaultcamDistance;
                camParent.GetComponent<cameraController>().yMinLimit = defaultYMinLimit;
                camParent.GetComponent<cameraController>().yMaxLimit = defaultYMaxLimit;
                gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        } else{
                gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                camParent.GetComponent<cameraController>().target = gameObject.transform.parent.gameObject.transform;
                camParent.GetComponent<cameraController>().distance = defaultcamDistance;
                camParent.GetComponent<cameraController>().yMinLimit = defaultYMinLimit;
                camParent.GetComponent<cameraController>().yMaxLimit = defaultYMaxLimit;
        }

    }
}
