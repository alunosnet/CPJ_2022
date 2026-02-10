using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetivoNivel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var temporizador = FindObjectOfType<Temporizador>();
            var tempo = temporizador.GetTempo();
            var pontuacao = FindObjectOfType<PlayerDataManager>();
            pontuacao.AddNewHighscore("Nome",(int) tempo);
            SistemaMensagens.instance.ShowMessage("Game Over");
            
        }
    }
}
