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
        //bloquear a seta do rato no centro do ecrã
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;

        float andar = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * andar * velocidadeAndar*Time.deltaTime);
        
        float rodar = Input.GetAxis("Mouse X");
        transform.Rotate(transform.up * rodar * velocidadeRodar * Time.deltaTime);
        
        float lado = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.left * -1 * lado * velocidadeAndar * Time.deltaTime);

        //TODO: saltar
        //TODO: limitar a subida de planos com grau de inclinação muito grande
        //TODO: CharacterController?
    }
}
