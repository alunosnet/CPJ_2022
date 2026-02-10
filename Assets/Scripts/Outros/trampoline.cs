using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class trampoline : MonoBehaviour
{
    [SerializeField] public float ForcaSalto = -10f;
    [SerializeField] public MoverPlayer2 Player;
 
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<MoverPlayer2>();
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("Salta");
            //Player.isKinematic = false;
            //Player.AddForce(Vector3.up * ForcaSalto);
            Player.Salta(ForcaSalto);
        }
             
    }
}
