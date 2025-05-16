using UnityEngine;
using UnityEngine.Playables;

public class ActivacionesCanvas : MonoBehaviour
{
    [SerializeField] private GameObject Ayuda; // Referencia al objeto Context
    [SerializeField] private GameObject Credits; // Referencia al objeto Credits
    [SerializeField] private GameObject Dosjugadores; // Referencia al objeto Dosjugadores
    [SerializeField] private GameObject IC; // Referencia al objeto Ui
    [SerializeField] private GameObject Configuracion; // Referencia al objeto Configuracion

    public Animator animatorAyuda;
    public Animator animatorCreditos;
    public Animator animatorDosjugadores;
    public Animator animatorIC;
    public Animator animatorConfiguracion;


    public void ActivarAyuda()
    {
        Ayuda.SetActive(true);
    }
    public void DesactivarAyuda()
    {
        Ayuda.SetActive(false);
    }
    public void ActivarCreditos()
    {
        Credits.SetActive(true);
    }
    public void DesactivarCreditos()
    {
        Credits.SetActive(false);
    }
    public void ActivarDosjugadores()
    {
        Dosjugadores.SetActive(true);
    }
    public void DesactivarDosjugadores()
    {
        Dosjugadores.SetActive(false);
    }
    public void ActivarIC()
    {
        IC.SetActive(true);
    }
    public void DesactivarIC()
    {
        IC.SetActive(false);        
    }
    public void ActivarConfiguracion()
    {
        Configuracion.SetActive(true);
    }
    public void DesactivarConfiguracion()
    {
        Configuracion.SetActive(false);
    }
    public void DesactivarICYActivarConfiguracion()
    {
        IC.SetActive(false);
        Configuracion.SetActive(true);        
    }
    public void DesactivarConfiguracionYActivarIC()
    {
        Configuracion.SetActive(false);
        IC.SetActive(true);
        animatorIC.Play("Voltear2IC");        
        //animatorConfiguracion.Play("VolverarCFG");
    }
}
