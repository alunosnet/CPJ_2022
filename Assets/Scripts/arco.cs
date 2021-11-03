﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Arco que dispara flechas
/// </summary>
public class arco : MonoBehaviour
{
    [SerializeField] float MaxForca = 100f;
    [SerializeField] float Forca;
    [SerializeField] float incForca = 1;
    [SerializeField] bool Carregar = false;
    [SerializeField] GameObject Flecha;
    [SerializeField] float AnguloX = 0;
    [SerializeField] float SensibilidadeX = 200f;
    GameObject FlechaADisparar;
    [SerializeField] float ScaleFactor;
    
    private void Start()
    {
        ScaleFactor = 1 / transform.localScale.z;
    }
    // Update is called once per frame
    void Update()
    {
        //olhar para cima/baixo
        AnguloX += Input.GetAxis("Mouse Y") * SensibilidadeX * Time.deltaTime;
        //limitar o angulo
        AnguloX = Mathf.Clamp(AnguloX, -30, 30);
        //aplicar a rotação
        transform.localRotation = Quaternion.Euler(AnguloX, 0, 0);

        //Disparar
        if (Input.GetButtonDown("Fire1"))
        {
            Carregar = true;
            //instanciar flecha
            FlechaADisparar = Instantiate(Flecha, transform.position + transform.forward,
                                transform.rotation);
            //não respeita gravidade
            FlechaADisparar.GetComponent<Rigidbody>().useGravity = false;
            //tornar a flecha filha do player (cubo vermelho)
            FlechaADisparar.transform.parent = transform;
            //TODO: alterar o material para parcialmente transparente
        }
        if (Carregar==true)
        {
            Forca += incForca * Time.deltaTime;
            if (Forca > MaxForca) Forca = MaxForca;
            FlechaADisparar.transform.localScale =new Vector3(FlechaADisparar.transform.localScale.x, 
                                                    FlechaADisparar.transform.localScale.y,
                                                    ScaleFactor* Forca / MaxForca);
        }
        if (Input.GetButtonUp("Fire1")/* || Forca==MaxForca*/)
        {
            //disparar
            //TODO: alterar o material para totalmente opaco
            //ativar a gravidade
            FlechaADisparar.GetComponent<Rigidbody>().useGravity = true;
            //flecha deixa de ser filha do cubo vermelho
            FlechaADisparar.transform.parent = null;
            //dar força à flecha
            FlechaADisparar.GetComponent<Rigidbody>().AddForce(Forca * transform.forward);
            Forca = 0;
            Carregar = false;
        }
    }
}
