using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] float RaioExplosao=10.0f;
    [SerializeField] float ForcaExplosao=10.0f;

    [SerializeField] float TempoExplodir=-1f;
    //TODO: Dano da explosão em NPC, SOM, Efeitos de particulas

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   private void OnTriggerEnter(Collider other){
       Explodir();
   }
    private void OnCollisionEnter(Collision collision){
        Explodir();
    }
    void Explodir(){
        Debug.Log("BOOM");
        Vector3 posicaoExplosao=transform.position;
        Collider[] colliders=Physics.OverlapSphere(posicaoExplosao,RaioExplosao);
        foreach(Collider obj in colliders){
            Rigidbody rb=obj.GetComponent<Rigidbody>();
            if(rb!=null)
                rb.AddExplosionForce(ForcaExplosao,posicaoExplosao,RaioExplosao,3.0f);

            //TODO: se tiver vida => tirar vida
        }

        //TODO: SOM e efeito de particulas

        Destroy(this.gameObject);
    }
}
