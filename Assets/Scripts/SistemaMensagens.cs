using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class SistemaMensagens : MonoBehaviour
{
    [SerializeField] Text txtMensagem;
    [SerializeField] float tempo = 4;
    bool Visivel = false;
    float TempoAtual;
    //Singletone
    public static SistemaMensagens instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        txtMensagem=transform.GetComponent<Text>();
        txtMensagem.text = "";
    }
    public void ShowMessage(string texto)
    {
        txtMensagem.text = texto;
        Visivel = true;
        TempoAtual = tempo;
    }
    // Update is called once per frame
    void Update()
    {
        if (Visivel)
        {
            TempoAtual -= Time.deltaTime;
            if (TempoAtual <= 0)
            {
                Visivel = false;
                txtMensagem.text = "";
                TempoAtual = 0;
            }
        }
    }
}
