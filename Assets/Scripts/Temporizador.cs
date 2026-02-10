using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temporizador : MonoBehaviour
{
    [SerializeField] float tempo = 0;
    [SerializeField] Text txtTempo;
    public float GetTempo()
    {
        return tempo;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (txtTempo == null)
            txtTempo = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        tempo += Time.deltaTime;
    }
    private void OnGUI()
    {
        if (txtTempo != null)
            txtTempo.text = "Tempo: " + tempo.ToString("F2");
    }
}
