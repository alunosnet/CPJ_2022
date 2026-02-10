using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiraVida : MonoBehaviour
{
    [SerializeField] int ValorTirar = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player")) return;
        Vida temp = collision.transform.GetComponentInParent<Vida>();
        if (temp != null)
        {
           // Debug.Log(collision.transform.root.name);
            Vector3 direcao = collision.transform.root.position - transform.position;
            direcao.y = 1;
            direcao.Normalize();
            NpcRagDoll npcRagDoll = collision.transform.GetComponentInParent<NpcRagDoll>();
            if (npcRagDoll != null)
                temp.TiraVida(ValorTirar, direcao, collision.GetContact(0).point);
            else
                temp.TiraVida(ValorTirar);
            ValorTirar = 0;
        }
        else
        {
            Rigidbody rb= collision.transform.GetComponentInParent<Rigidbody>();
            if (rb != null) {
                Vector3 direcao = collision.transform.root.position - transform.position;
                direcao.y = 1;
                direcao.Normalize();
                rb.AddForce(direcao, ForceMode.Impulse);
            }
        }
    }
    
    
}
