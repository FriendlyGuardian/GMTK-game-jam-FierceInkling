using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos, height, startposY;
    public GameObject cam;
    public float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        startposY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }
    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        float temp2 = (cam.transform.position.y * (1 - parallaxEffect));
        float dist2 = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(startpos + dist, startposY + dist2, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;

      
        if (temp2 > startposY + height) startposY += height;
        else if (temp2 < startposY - height) startposY -= height;
    }

    // Update is called once per frame
   
}
