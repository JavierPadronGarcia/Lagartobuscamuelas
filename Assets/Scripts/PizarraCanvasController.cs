using Unity.VisualScripting;
using UnityEngine;

public class PizarraCanvasController : MonoBehaviour
{
    [SerializeField] GameObject datosPartida;
    [SerializeField] GameObject ayudaPistola;
    [SerializeField] GameObject ayudaMartillo;

    [SerializeField] GameObject martillo;
    [SerializeField] GameObject pistola;

    [SerializeField] Transform martilloPosicion;
    [SerializeField] Transform pistolaPosicion;

    private void Start()
    {
        DesactivarTodo();
        datosPartida.SetActive(true);
    }

    public void ShowDatosPartida()
    {
        DesactivarTodo();
        datosPartida.SetActive(true);
    }

    public void ShowAyudaPistola()
    {
        DesactivarTodo();
        ayudaPistola.SetActive(true);
    }

    public void ShowAyudaMartillo()
    {
        DesactivarTodo();
        ayudaMartillo.SetActive(true);
    }

    private void DesactivarTodo()
    {
        datosPartida.SetActive(false);
        ayudaPistola.SetActive(false);
        ayudaMartillo.SetActive(false);
    }

    public void RestablecerMartillo()
    {
        martillo.transform.SetParent(null);
        martillo.transform.SetPositionAndRotation(martilloPosicion.position, martilloPosicion.rotation);
    }

    public void RestablecerPistola()
    {
        pistola.transform.SetParent(null);
        pistola.transform.SetPositionAndRotation(pistolaPosicion.position, pistolaPosicion.rotation);
    }

    public void CargarEscena(string sceneName)
    {
        SCManager.instance.LoadScene(sceneName);
    }
}
