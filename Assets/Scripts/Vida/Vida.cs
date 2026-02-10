using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Gerir o estado da vida do GameObject
/// Destroi quando vida<=0
/// </summary>
public class Vida : MonoBehaviour
{
    [SerializeField] int _Vida = 100;
    [SerializeField] float _TempoMorrer = 0;
    Animator _Animator;
    NpcRagDoll _NpcRagDoll;
    public int GetVida()
    {
        return _Vida;
    }

    public void TiraVida(int valor)
    {
        _Vida -= valor;
        if (_Vida <= 0)
            Morre();
    }
    public void TiraVida(int valor,Vector3 forca,Vector3 hitPoint)
    {
        _Vida -= valor;
        if (_Vida <= 0)
            Morre(forca,hitPoint);

    }
    private void Morre()
    {
        if (_NpcRagDoll != null)
        {
            _NpcRagDoll.EnableRagdoll();
            _Animator.enabled = false;
        }
        Destroy(this.gameObject, _TempoMorrer);
        //if(_Animator!=null)
        _Animator?.SetBool("morre", true);
        _Vida = 0;
    }
    private void Morre(Vector3 forca, Vector3 hitPoint)
    {
        _NpcRagDoll.TriggerRagdoll(forca,hitPoint);
        _Animator.enabled = false;
        Destroy(this.gameObject, _TempoMorrer);
        //if(_Animator!=null)
        _Animator?.SetBool("morre", true);
        _Vida = 0;
    }
    public void GanhaVida(int valor)
    {
        _Vida += valor;
        if (_Vida > 100)
            _Vida = 100;
    }
    // Start is called before the first frame update
    void Start()
    {
        _Animator=GetComponent<Animator>();
        _NpcRagDoll=GetComponent<NpcRagDoll>();
    }

    // Update is called once per frame
    void Update()
    {

        if (_Vida <= 0)
            Morre();
    }

}
