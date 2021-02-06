using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teleporter : MonoBehaviour
{
    public bool working = true;
    public string destinationScene = "world_0_menu";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && working)
        {
            SceneManager.LoadScene(destinationScene);
        }
    }

}
