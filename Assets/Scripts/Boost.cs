using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float forca = 5;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        rb.AddForce(Vector3.right * forca);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
