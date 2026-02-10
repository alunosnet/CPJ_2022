using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] float Intervalo = 5;
    [SerializeField] GameObject Modelo;
    [SerializeField] Transform[] Pontos;
    [SerializeField] int MaxNPCS = 5;
    float NextSpawn;

    // Start is called before the first frame update
    void Start()
    {
        NextSpawn = Intervalo;
    }

    // Update is called once per frame
    void Update()
    {
        NextSpawn -= Time.deltaTime;
        if (NextSpawn <= 0)
        {
            NextSpawn = Intervalo + Random.Range(0, 10);
            //contar npcs
            if (GameObject.FindObjectsOfType<MoverNPC>().Length >= MaxNPCS) return;

            Vector3 posicao = new Vector3(transform.position.x+Random.Range(-2,2),
                                        transform.position.y,
                                        transform.position.z+Random.Range(-2,2));
            var NPC = Instantiate(Modelo, posicao, Quaternion.identity);
            NPC.GetComponent<MoverNPC>().Pontos = Pontos;
        }
    }
}
