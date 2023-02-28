using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Vector3 faceVector = transform.position - GameManager.instance.playerObject.transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(faceVector.x, 0f, faceVector.z));
    }
}
