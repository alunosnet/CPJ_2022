using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moverplayer : MonoBehaviour
{
    [SerializeField] float velocidadeAndar = 5;
    [SerializeField] float velocidadeRodar = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float andar = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * andar * velocidadeAndar*Time.deltaTime);

        float rodar = Input.GetAxis("Horizontal");
        transform.Rotate(transform.up * rodar * velocidadeRodar * Time.deltaTime);
        //TODO: Rodar com o rato e as teclas para andar para o lado
    }
}
