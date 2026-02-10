using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtacarPlayer : MonoBehaviour
{
    [SerializeField] GameObject ObjetoAtirar;
    [SerializeField] GameObject Player;
    [SerializeField] float DistanciaAtirar = 5;
    [SerializeField] float IntervaloAtirar = 5;
    [SerializeField] Transform PontoAtirar;
    [SerializeField] float forca = 10;
    float IntervaloAtual = 0;
    MoverNPC npc;
    
    // Start is called before the first frame update
    void Start()
    {
        npc=GetComponent<MoverNPC>();
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (npc.Estado == MoverNPC.NPCEstados.Morto) return;
        if (IntervaloAtual > 0)
        {
            IntervaloAtual -= Time.deltaTime;
            return;
        }
        if(npc!=null && Player!=null && npc.Estado == MoverNPC.NPCEstados.Atacar)
        {
            if(Vector3.Distance(transform.position,Player.transform.position)< DistanciaAtirar)
            {
                IntervaloAtual = IntervaloAtirar;
                var objeto = Instantiate(ObjetoAtirar, PontoAtirar.position, PontoAtirar.rotation);
                //Vector3 velocidade= Utils.BallisticVelocity(transform.position, Player.transform.position, 45);
                //Debug.Log(velocidade);
                objeto.GetComponent<Rigidbody>().AddForce(PontoAtirar.forward*forca);
            }

        }
    }
}
