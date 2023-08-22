using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public GameObject Diver;
    public float speedX, speedY , speedrot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Diver.transform.position.x - transform.position.x) * Time.deltaTime * speedX, (Diver.transform.position.y - transform.position.y) * Time.deltaTime * speedY, 0);
    }
}
