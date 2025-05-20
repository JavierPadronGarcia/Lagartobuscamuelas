using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject Ayuda; // Referencia al objeto Context
    [SerializeField] private GameObject Credits; // Referencia al objeto Credits
    [SerializeField] private GameObject Dosjugadores; // Referencia al objeto Dosjugadores
    [SerializeField] private GameObject IC; // Referencia al objeto Ui
    [SerializeField] private GameObject Configuracion; // Referencia al objeto Configuracion
    [SerializeField] private PlayableDirector timelineDirector; // Referencia al Timeline

    private bool Ayudabool;
    private bool Creditsbool;
    private bool Dosjugadoresbool;

    public Animator animatorAyuda;
    public Animator animatorCreditos;
    public Animator animatorDosjugadores;
    public Animator animatorIC;
    public Animator animatorConfiguracion;

    private void Start()
    {
        if(timelineDirector != null) timelineDirector.Stop();
    }
    public void ExitGame()
    {
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    public void ActivarAyuda()
    {
        if (Ayudabool == true)
        {
            animatorAyuda.SetTrigger("out");
            Ayudabool = false;
        }
        else
        {
            Ayuda.SetActive(true); // Activa el objeto UI
            animatorAyuda.SetTrigger("in");
            Ayudabool = true;
        }
    }

    public void ActivarCredits()
    {
        if (Creditsbool == true)
        {
            animatorCreditos.SetTrigger("out");
            Creditsbool = false;
        }
        else
        {
            Credits.SetActive(true); // Activa el objeto UI
            animatorCreditos.SetTrigger("in");
            Creditsbool = true;
        }
    }

    public void ActivarDosJugadoresLobby()
    {
        if (Dosjugadoresbool == true)
        {
            animatorDosjugadores.SetTrigger("out");
            Dosjugadoresbool = false;
            StartCoroutine(EjecutarConEspera());
        }
        else
        {
            animatorIC.SetTrigger("in");
            StartCoroutine(EjecutarConEspera2());
            Dosjugadoresbool = true;
        }
    }

    IEnumerator EjecutarConEspera()
    {
        yield return new WaitForSeconds(0.7f); // Espera 0.5 segundos
        Dosjugadores.SetActive(false);
        IC.SetActive(true);
        animatorIC.Play("MenuICOut");
    }

    IEnumerator EjecutarConEspera2()
    {
        yield return new WaitForSeconds(0.9f); // Espera 0.5 segundos
        Dosjugadores.SetActive(true); // Activa el objeto UI
        animatorDosjugadores.SetTrigger("in");
        IC.SetActive(false);
    }

    IEnumerator EjecutarConEspera3()
    {
        yield return new WaitForSeconds(0.9f); // Espera 0.5 segundos
        timelineDirector.Play();
    }

    public void LoadScene(string sceneName) {
        SCManager.instance.LoadScene(sceneName);
    }

    public void EjecutarAnimacionYTimeline()
    {
        animatorConfiguracion.Play("DifuminarCFG");
        StartCoroutine(EjecutarConEspera3());
    }

    public void ActivarConfiguracion()
    {
        animatorIC.Play("Voltear");        
    }

    public void ActivarICdeConfiguracion()
    {
        animatorConfiguracion.Play("Voltear2CFG");
    }
}

