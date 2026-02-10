using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoverNPC : MonoBehaviour
{
    public enum NPCEstados { Idle=0, Patrulha=1, Atacar=2, Morto=3}
    public Transform[] Pontos;
    [SerializeField] int ProximoPonto = 0;
    [SerializeField] float Velocidade = 3;
    [SerializeField] float DistanciaMinima = 2;
    [SerializeField] public NPCEstados Estado = NPCEstados.Patrulha;
    [SerializeField] Transform Olhos;
    [SerializeField] float AnguloVisao = 90;
    [SerializeField] float DistanciaVisao = 10;
    NavMeshAgent Agente;
    GameObject player;
    Animator animator;
    Vida vida;
    // Start is called before the first frame update
    void Start()
    {
        Agente = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        vida = GetComponent<Vida>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Estado == NPCEstados.Morto) return;

        if (vida != null && vida.GetVida() <= 0)
        {
            Estado = NPCEstados.Morto;
            return;
        }
        
        if (Agente == null)
        {
            if (Vector3.Distance(transform.position, Pontos[ProximoPonto].position) < DistanciaMinima)
            {
                ProximoPonto = ProximoPonto + 1;
                if (ProximoPonto > Pontos.Length - 1)
                    ProximoPonto = 0;
            }

            Vector3 direcao = Pontos[ProximoPonto].position - transform.position;
            Quaternion rotacao = Quaternion.LookRotation(direcao, Vector3.up);
            //rodar o npc na direcao do ponto
            transform.rotation = rotacao;
            //mover o npc para o ponto
            transform.Translate(Vector3.forward * Velocidade * Time.deltaTime);
            if (animator != null)
                animator.SetFloat("velocidade", Velocidade);
        }
        else
        {
            if (animator != null)
                animator.SetFloat("velocidade", Agente.velocity.magnitude);
            if (Estado == NPCEstados.Patrulha)
            {
                if (Pontos.Length == 0)
                {
                    Estado = NPCEstados.Idle;
                    return;
                }
                if (Vector3.Distance(transform.position, Pontos[ProximoPonto].position) < DistanciaMinima)
                {
                    ProximoPonto = ProximoPonto + 1;
                    if (ProximoPonto > Pontos.Length - 1)
                        ProximoPonto = 0;
                }
                Agente.isStopped = false;
                Agente.speed = Velocidade;
                Agente.SetDestination(Pontos[ProximoPonto].position);
            }
            if (player == null) return;
            if (Vector3.Distance(transform.position,player.transform.position)<=DistanciaVisao && Utils.CanYouSeeThis(Olhos, player.transform, null, AnguloVisao))
            {
                Estado = NPCEstados.Atacar;
            }
            else
            {
                if (Pontos!=null && Pontos.Length>0)
                    Estado = NPCEstados.Patrulha;
                else
                    Estado = NPCEstados.Idle;
            }
            if (Estado == NPCEstados.Atacar)
            {
                Agente.isStopped = false;
                Agente.speed = Velocidade;
                Vector3 OlharPara = new Vector3(player.transform.position.x,transform.position.y ,player.transform.position.z);
                transform.LookAt(OlharPara);
                Agente.SetDestination(player.transform.position);
            }
        }
    }
}
