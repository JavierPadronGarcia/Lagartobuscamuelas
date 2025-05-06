using UnityEditor;
using UnityEngine;
// ---------------------------------------------------------------------------------
// SCRIPT PARA LA GESTIÓN DE ESCENAS (vinculado a un GameObject vacío "SceneManager")
// ---------------------------------------------------------------------------------
using UnityEngine.SceneManagement; // Se incluye la librería para el manejo de escenas
// OJO: al incluir esta librería, no se podrá usar el nombre "SceneManager" porque
// ya hay una clase Static con dicho nombre. Por eso la clase se llama "SCManager"

public class SCManager : MonoBehaviour {
  
    [SerializeField] private GameObject Context; // Referencia al objeto Context
    [SerializeField] private GameObject Credits; // Referencia al objeto Credits
    private bool Contextbool;
    private bool Creditsbool;

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
    public void ActivarContext()
    {        
        if (Contextbool == true)
        {
            Context.SetActive(false); // Desactiva el objeto Context
            Contextbool = false;
        }
        else
        {
            Context.SetActive(true); // Activa el objeto Context
            Contextbool = true;
        }        
    }
    public void ActivarCredits()
    {
        if (Creditsbool == true)
        {
            Credits.SetActive(false); // Desactiva el objeto Credits
            Creditsbool = false;
        }
        else
        {
            Credits.SetActive(true); // Activa el objeto Credits
            Creditsbool = true;
        }        
    }
}

// -----------------------------------------
// EJEMPLOS DE USO ¡DESDE CUALQUIER SCRIPT!
// -----------------------------------------
//SCManager.instance.LoadScene("Bienvenida");
//SCManager.instance.LoadScene("Derrota");
//SCManager.instance.LoadScene("Victoria");
//SCManager.instance.LoadScene("Level 1");
