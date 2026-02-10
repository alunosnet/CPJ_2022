using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;
/// <summary>
/// Arco que dispara flechas
/// </summary>
public class arco : MonoBehaviour
{
    [SerializeField] float MaxForca = 100f;
    [SerializeField] public float Forca;
    [SerializeField] float incForca = 1;
    [SerializeField] public bool Carregar = false;
    [SerializeField] GameObject Flecha;
    [SerializeField] float AnguloX = 0;
    [SerializeField] float SensibilidadeX = 200f;
    GameObject FlechaADisparar;
    [SerializeField] float ScaleFactor;
    Animator _animator;
    [SerializeField] Transform PosicaoAtirar;
    [SerializeField] Transform PosicaoGuardar;
    [SerializeField] GameObject _arco;
    private void Start()
    {
        _animator=GetComponentInParent<Animator>();
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
            FlechaADisparar = Instantiate(Flecha, transform.position,transform.rotation);
            //não respeita gravidade
            FlechaADisparar.GetComponent<Rigidbody>().isKinematic = true;
            //tornar a flecha filha do player (cubo vermelho)
            FlechaADisparar.transform.parent = transform;
        }
        if (Carregar == true && FlechaADisparar == null)
        {
            Forca = 0;
            Carregar = false;
        }
        if (Carregar==true)
        {
            _arco.transform.parent = PosicaoAtirar;
            _arco.transform.localPosition = Vector3.zero;
            _arco.transform.localRotation = Quaternion.Euler(0, 180, 0);
            
            _animator.SetBool("aim", true);
            Forca += incForca * Time.deltaTime;
            if (Forca > MaxForca) Forca = MaxForca;
        }
        else
        {
            _animator.SetBool("aim", false);
            _arco.transform.parent = PosicaoGuardar;
            _arco.transform.localPosition = Vector3.zero;
            _arco.transform.localRotation = Quaternion.identity;
        }
        if (Input.GetButtonUp("Fire1") && FlechaADisparar!=null/* || Forca==MaxForca*/)
        {
            //disparar
            //ativar a gravidade
            FlechaADisparar.GetComponent<Rigidbody>().isKinematic = false;
            //flecha deixa de ser filha do cubo vermelho
            FlechaADisparar.transform.parent = null;
            //dar força à flecha
            FlechaADisparar.GetComponent<Rigidbody>().AddForce(Forca * transform.forward);
            Forca = 0;
            Carregar = false;
        }
    }
}
