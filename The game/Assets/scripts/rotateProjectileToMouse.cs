using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateProjectileToMouse : MonoBehaviour
{

    Camera cam;
    public float maxLength = 100;
    Ray rayMouse;
    Vector3 pos;
    Vector3 direction;
    Quaternion rotation;
    

    // private void Start() {
    //     if(cam == null)
    //         cam = Camera.main;
    // }
    void Start() // to follow use update
    {
        if(cam == null)
            cam = Camera.main;
        RaycastHit hit;
        var mousePos = Input.mousePosition;
        rayMouse = cam.ScreenPointToRay(mousePos);
        if(Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit,maxLength)){
            rotateToMouseDirection(gameObject, hit.point);
        } else{
            var pos = rayMouse.GetPoint(maxLength);
            rotateToMouseDirection(gameObject, pos);
        }
    }

    void rotateToMouseDirection(GameObject obj, Vector3 destination){
        direction = destination - obj.transform.position;
        rotation = Quaternion.LookRotation(direction);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }

    public Quaternion getRotation(){
        return rotation;
    }
}
