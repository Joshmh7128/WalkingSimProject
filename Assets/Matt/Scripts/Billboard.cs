using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Distance in front of / behind the main camera to look at
    // The further behind the camera this is, the more objects will face opposite the camera
    // The closer to 0, the more objects will turn directly toward the camera
    public float lookOffset = -10;

    private Camera mainCamera;
    private GameObject[] objectsToBillboard;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

        objectsToBillboard = GameObject.FindGameObjectsWithTag("Billboard");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        foreach (var obj in objectsToBillboard)
        {
            //Vector3 between = new Vector3(mainCamera.transform.position.x, obj.transform.position.y, mainCamera.transform.position.z) - obj.transform.position;
            //Vector3 lookTarget = new Vector3(mainCamera.transform.position.x, obj.transform.position.y, mainCamera.transform.position.z);
            Vector3 lookTarget = new Vector3(mainCamera.transform.position.x, obj.transform.position.y, mainCamera.transform.position.z) + new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z).normalized * lookOffset;
            obj.transform.LookAt(lookTarget);

            //obj.transform.rotation = Quaternion.Euler(new Vector3(0, mainCamera.transform.eulerAngles.y, 0));
        }
    }
}
