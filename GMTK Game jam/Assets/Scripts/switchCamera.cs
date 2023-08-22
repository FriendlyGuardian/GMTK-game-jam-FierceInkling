using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchCamera : MonoBehaviour
{
    public GameObject Camera_1;
    public GameObject Camera_2;
    public void Cam_1()
    {
        Camera_1.SetActive(true);
        Camera_2.SetActive(false);
    }
    public void Cam_2()
    {
        Camera_1.SetActive(false);
        Camera_2.SetActive(true);
    }
}
