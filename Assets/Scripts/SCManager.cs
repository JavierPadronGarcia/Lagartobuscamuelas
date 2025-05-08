using UnityEditor;
using UnityEngine;
using System.Collections;
// ---------------------------------------------------------------------------------
// SCRIPT PARA LA GESTIÓN DE ESCENAS (vinculado a un GameObject vacío "SceneManager")
// ---------------------------------------------------------------------------------
using UnityEngine.SceneManagement; // Se incluye la librería para el manejo de escenas
// OJO: al incluir esta librería, no se podrá usar el nombre "SceneManager" porque
// ya hay una clase Static con dicho nombre. Por eso la clase se llama "SCManager"

public class SCManager : MonoBehaviour {
  
    [SerializeField] private GameObject Ayuda; // Referencia al objeto Context
    [SerializeField] private GameObject Credits; // Referencia al objeto Credits
    [SerializeField] private GameObject Dosjugadores; // Referencia al objeto Dosjugadores
    [SerializeField] private GameObject UI; // Referencia al objeto Ui
    private bool Ayudabool;
    private bool Creditsbool;
    private bool Dosjugadoresbool;
    public Animator animatorAyuda;
    public Animator animatorCreditos;
    public Animator animatorDosjugadores;
    public Animator animatorIC;

    // Creamos una variable estática para almacenar la única instancia
    public static SCManager instance;
  
  // Método Awake que se llama al inicio antes de que se active el objeto. Útil para inicializar
  // variables u objetos que serán llamados por otros scripts (game managers, clases singleton, etc).
  private void Awake() {
    
    // ----------------------------------------------------------------
    // AQUÍ ES DONDE SE DEFINE EL COMPORTAMIENTO DE LA CLASE SINGLETON
    // Garantizamos que solo exista una instancia del SCManager
    // Si no hay instancias previas se asigna la actual
    // Si hay instancias se destruye la nueva
    if (instance == null) instance = this;
    else { Destroy(gameObject); return; }
    // ----------------------------------------------------------------
    
    // No destruimos el SceneManager aunque se cambie de escena
    DontDestroyOnLoad(gameObject);
  
  }
  
  // Método para cargar una nueva escena por nombre
  public void LoadScene(string sceneName) {
    SceneManager.LoadScene(sceneName);
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
            //UI.SetActive(false); // Desactiva el objeto UI
            animatorIC.SetTrigger("in");
            StartCoroutine(EjecutarConEspera2());
            //UI.SetActive(false); // Desactiva el objeto UI            
            Dosjugadoresbool = true;
        }
    }
    IEnumerator EjecutarConEspera()
    {        
        yield return new WaitForSeconds(0.7f); // Espera 0.5 segundos
        Dosjugadores.SetActive(false);
        UI.SetActive(true);
        animatorIC.SetTrigger("out");
        //UI.SetActive(true); // Activa el objeto UI                      
    }
    IEnumerator EjecutarConEspera2()
    {
        yield return new WaitForSeconds(0.9f); // Espera 0.5 segundos
        Dosjugadores.SetActive(true); // Activa el objeto UI
        animatorDosjugadores.SetTrigger("in");
        UI.SetActive(false);
        //Dosjugadores.SetActive(false);
        //UI.SetActive(true);
        //animatorIC.SetTrigger("out");
        //UI.SetActive(true); // Activa el objeto UI                      
    }
}

// -----------------------------------------
// EJEMPLOS DE USO ¡DESDE CUALQUIER SCRIPT!
// -----------------------------------------
//SCManager.instance.LoadScene("Bienvenida");
//SCManager.instance.LoadScene("Derrota");
//SCManager.instance.LoadScene("Victoria");
//SCManager.instance.LoadScene("Level 1");
