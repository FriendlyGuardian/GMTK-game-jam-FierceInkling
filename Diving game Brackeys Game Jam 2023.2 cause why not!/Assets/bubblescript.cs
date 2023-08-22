using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubblescript : MonoBehaviour
{
    private diverMovement diver;
    private GameObject Diver;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
        
        Diver = GameObject.Find("Diver");
        diver = Diver.GetComponent<diverMovement>();
    }

    private void Awake()
    {
        anim.Play("Bubble");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                diver.addAir(2);
                Destroy(gameObject);
            }
        }
    }
}
