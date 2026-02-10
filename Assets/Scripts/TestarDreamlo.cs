using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestarDreamlo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            PlayerDataManager p = GameObject.FindObjectOfType<PlayerDataManager>();
            p.AddNewHighscore("Joaquim", 200);
            SistemaMensagens.instance.ShowMessage("Enviado");
        }
    }
}
