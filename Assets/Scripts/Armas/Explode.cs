using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] float RaioExplosao=10.0f;
    [SerializeField] float ForcaExplosao=10.0f;
    [SerializeField] bool TemTimer=false;
    [SerializeField] float TempoExplodir=-1f;
    [SerializeField] int TiraVida = 50;
    Rigidbody _rigidbody;
    //TODO:  SOM, Efeitos de particulas

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TemTimer)
        {
            TempoExplodir -= Time.deltaTime;
            if (TempoExplodir <= 0)
                Explodir();
        }
    }
   private void OnTriggerEnter(Collider other){

       if(TemTimer==false)
            Explodir();
        else
            PararFlecha(other.gameObject);
   }
    private void OnCollisionEnter(Collision collision){
        if(TemTimer==false)
            Explodir();
        else
            PararFlecha(collision.gameObject);
    }
    void PararFlecha(GameObject obj){
        _rigidbody.velocity=Vector3.zero;
        _rigidbody.collisionDetectionMode=CollisionDetectionMode.ContinuousSpeculative;
        _rigidbody.isKinematic=true;
        transform.parent=obj.transform;
    }
    void Explodir(){
        Vector3 posicaoExplosao=transform.position;
        Collider[] colliders=Physics.OverlapSphere(posicaoExplosao,RaioExplosao);
        foreach(Collider obj in colliders){
            Rigidbody rb=obj.GetComponent<Rigidbody>();
            if(rb!=null)
                rb.AddExplosionForce(ForcaExplosao,posicaoExplosao,RaioExplosao,3.0f);

            Vida vd=obj.GetComponent<Vida>();
            if (vd != null)
                vd.TiraVida(TiraVida);
        }

        //TODO: SOM e efeito de particulas

        Destroy(this.gameObject);
    }
}
