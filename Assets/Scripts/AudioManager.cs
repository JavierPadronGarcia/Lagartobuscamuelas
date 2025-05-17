// ---------------------------------------------------------------------------------
// SCRIPT PARA LA GESTI”N DE AUDIO (vinculado a un GameObject vac˙å "AudioManager")
// ---------------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    // Instancia ˙nica del AudioManager (porque es una clase Singleton) STATIC
    public static AudioManager instance;

    // Se crean dos AudioSources diferentes para que se puedan
    // reproducir los efectos y la m˙sica de fondo al mismo tiempo
    public AudioSource sfxSource; // Componente AudioSource para efectos de sonido
    public AudioSource musicSource; // Componente AudioSource para la m˙sica de fondo

    // En vez de usar un vector de AudioClips (que podr˙} ser) vamos a utilizar un Diccionario
    // en el que cargaremos directamente los recursos desde la jerarqu˙} del proyecto
    // Cada entrada del diccionario tiene una string como clave y un AudioClip como valor
    public Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> musicClips = new Dictionary<string, AudioClip>();

    // MÈtodo Awake que se llama al inicio antes de que se active el objeto. ⁄til para inicializar
    // variables u objetos que ser·n llamados por otros scripts (game managers, clases singleton, etc).
    private void Awake()
    {

        // ----------------------------------------------------------------
        // AQUÕ ES DONDE SE DEFINE EL COMPORTAMIENTO DE LA CLASE SINGLETON
        // Garantizamos que solo exista una instancia del AudioManager
        // Si no hay instancias previas se asigna la actual
        // Si hay instancias se destruye la nueva
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }
        // ----------------------------------------------------------------

        // No destruimos el AudioManager aunque se cambie de escena
        DontDestroyOnLoad(gameObject);

        // Cargamos los AudioClips en los diccionarios
        LoadSFXClips();
        LoadMusicClips();

    }

    // MÈtodo privado para cargar los efectos de sonido directamente desde las carpetas
    private void LoadSFXClips()
    {
        // Los recursos (ASSETS) que se cargan en TIEMPO DE EJECUCI”N DEBEN ESTAR DENTRO de una carpeta denominada /Assets/Resources/SFX
        sfxClips["Crash"] = Resources.Load<AudioClip>("SFX/321143__rodincoil__concrete-and-glass-breaking-in-dumpster");
        sfxClips["Pop"] = Resources.Load<AudioClip>("SFX/570459__splendidjams__simple-pop-sound-effect");

        // UI Sounds
        sfxClips["Back001"] = Resources.Load<AudioClip>("SFX/KenneyUI/back_001");
        sfxClips["Back002"] = Resources.Load<AudioClip>("SFX/KenneyUI/back_002");
        sfxClips["Back003"] = Resources.Load<AudioClip>("SFX/KenneyUI/back_003");
        sfxClips["Back004"] = Resources.Load<AudioClip>("SFX/KenneyUI/back_004");
        sfxClips["Bong001"] = Resources.Load<AudioClip>("SFX/KenneyUI/bong_001");
        sfxClips["Click001"] = Resources.Load<AudioClip>("SFX/KenneyUI/click_001");
        sfxClips["Click002"] = Resources.Load<AudioClip>("SFX/KenneyUI/click_002");
        sfxClips["Click003"] = Resources.Load<AudioClip>("SFX/KenneyUI/click_003");
        sfxClips["Click004"] = Resources.Load<AudioClip>("SFX/KenneyUI/click_004");
        sfxClips["Click005"] = Resources.Load<AudioClip>("SFX/KenneyUI/click_005");
        sfxClips["Close001"] = Resources.Load<AudioClip>("SFX/KenneyUI/close_001");
        sfxClips["Close002"] = Resources.Load<AudioClip>("SFX/KenneyUI/close_002");
        sfxClips["Close003"] = Resources.Load<AudioClip>("SFX/KenneyUI/close_003");
        sfxClips["Close004"] = Resources.Load<AudioClip>("SFX/KenneyUI/close_004");
        sfxClips["Confirmation001"] = Resources.Load<AudioClip>("SFX/KenneyUI/confirmation_001");
        sfxClips["Confirmation002"] = Resources.Load<AudioClip>("SFX/KenneyUI/confirmation_002");
        sfxClips["Confirmation003"] = Resources.Load<AudioClip>("SFX/KenneyUI/confirmation_003");
        sfxClips["Confirmation004"] = Resources.Load<AudioClip>("SFX/KenneyUI/confirmation_004");
        sfxClips["Drop001"] = Resources.Load<AudioClip>("SFX/KenneyUI/drop_001");
        sfxClips["Drop002"] = Resources.Load<AudioClip>("SFX/KenneyUI/drop_002");
        sfxClips["Drop003"] = Resources.Load<AudioClip>("SFX/KenneyUI/drop_003");
        sfxClips["Drop004"] = Resources.Load<AudioClip>("SFX/KenneyUI/drop_004");
        sfxClips["Error001"] = Resources.Load<AudioClip>("SFX/KenneyUI/error_001");
        sfxClips["Error002"] = Resources.Load<AudioClip>("SFX/KenneyUI/error_002");
        sfxClips["Error003"] = Resources.Load<AudioClip>("SFX/KenneyUI/error_003");
        sfxClips["Error004"] = Resources.Load<AudioClip>("SFX/KenneyUI/error_004");
        sfxClips["Error005"] = Resources.Load<AudioClip>("SFX/KenneyUI/error_005");
        sfxClips["Error006"] = Resources.Load<AudioClip>("SFX/KenneyUI/error_006");
        sfxClips["Error007"] = Resources.Load<AudioClip>("SFX/KenneyUI/error_007");
        sfxClips["Error008"] = Resources.Load<AudioClip>("SFX/KenneyUI/error_008");
        sfxClips["Glass001"] = Resources.Load<AudioClip>("SFX/KenneyUI/glass_001");
        sfxClips["Glass002"] = Resources.Load<AudioClip>("SFX/KenneyUI/glass_002");
        sfxClips["Glass003"] = Resources.Load<AudioClip>("SFX/KenneyUI/glass_003");
        sfxClips["Glass004"] = Resources.Load<AudioClip>("SFX/KenneyUI/glass_004");
        sfxClips["Glass005"] = Resources.Load<AudioClip>("SFX/KenneyUI/glass_005");
        sfxClips["Glass006"] = Resources.Load<AudioClip>("SFX/KenneyUI/glass_006");
        sfxClips["Glitch001"] = Resources.Load<AudioClip>("SFX/KenneyUI/glitch_001");
        sfxClips["Glitch002"] = Resources.Load<AudioClip>("SFX/KenneyUI/glitch_002");
        sfxClips["Glitch003"] = Resources.Load<AudioClip>("SFX/KenneyUI/glitch_003");
        sfxClips["Glitch004"] = Resources.Load<AudioClip>("SFX/KenneyUI/glitch_004");
        sfxClips["Maximize001"] = Resources.Load<AudioClip>("SFX/KenneyUI/maximize_001");
        sfxClips["Maximize002"] = Resources.Load<AudioClip>("SFX/KenneyUI/maximize_002");
        sfxClips["Maximize003"] = Resources.Load<AudioClip>("SFX/KenneyUI/maximize_003");
        sfxClips["Maximize004"] = Resources.Load<AudioClip>("SFX/KenneyUI/maximize_004");
        sfxClips["Maximize005"] = Resources.Load<AudioClip>("SFX/KenneyUI/maximize_005");
        sfxClips["Maximize006"] = Resources.Load<AudioClip>("SFX/KenneyUI/maximize_006");
        sfxClips["Maximize007"] = Resources.Load<AudioClip>("SFX/KenneyUI/maximize_007");
        sfxClips["Maximize008"] = Resources.Load<AudioClip>("SFX/KenneyUI/maximize_008");
        sfxClips["Maximize009"] = Resources.Load<AudioClip>("SFX/KenneyUI/maximize_009");
        sfxClips["Minimize001"] = Resources.Load<AudioClip>("SFX/KenneyUI/minimize_001");
        sfxClips["Minimize002"] = Resources.Load<AudioClip>("SFX/KenneyUI/minimize_002");
        sfxClips["Minimize003"] = Resources.Load<AudioClip>("SFX/KenneyUI/minimize_003");
        sfxClips["Minimize004"] = Resources.Load<AudioClip>("SFX/KenneyUI/minimize_004");
        sfxClips["Minimize005"] = Resources.Load<AudioClip>("SFX/KenneyUI/minimize_005");
        sfxClips["Minimize006"] = Resources.Load<AudioClip>("SFX/KenneyUI/minimize_006");
        sfxClips["Minimize007"] = Resources.Load<AudioClip>("SFX/KenneyUI/minimize_007");
        sfxClips["Minimize008"] = Resources.Load<AudioClip>("SFX/KenneyUI/minimize_008");
        sfxClips["Minimize009"] = Resources.Load<AudioClip>("SFX/KenneyUI/minimize_009");
        sfxClips["Open001"] = Resources.Load<AudioClip>("SFX/KenneyUI/open_001");
        sfxClips["Open002"] = Resources.Load<AudioClip>("SFX/KenneyUI/open_002");
        sfxClips["Open003"] = Resources.Load<AudioClip>("SFX/KenneyUI/open_003");
        sfxClips["Open004"] = Resources.Load<AudioClip>("SFX/KenneyUI/open_004");
        sfxClips["Pluck001"] = Resources.Load<AudioClip>("SFX/KenneyUI/pluck_001");
        sfxClips["Pluck002"] = Resources.Load<AudioClip>("SFX/KenneyUI/pluck_002");
        sfxClips["Question001"] = Resources.Load<AudioClip>("SFX/KenneyUI/question_001");
        sfxClips["Question002"] = Resources.Load<AudioClip>("SFX/KenneyUI/question_002");
        sfxClips["Question003"] = Resources.Load<AudioClip>("SFX/KenneyUI/question_003");
        sfxClips["Question004"] = Resources.Load<AudioClip>("SFX/KenneyUI/question_004");
        sfxClips["Scratch001"] = Resources.Load<AudioClip>("SFX/KenneyUI/scratch_001");
        sfxClips["Scratch002"] = Resources.Load<AudioClip>("SFX/KenneyUI/scratch_002");
        sfxClips["Scratch003"] = Resources.Load<AudioClip>("SFX/KenneyUI/scratch_003");
        sfxClips["Scratch004"] = Resources.Load<AudioClip>("SFX/KenneyUI/scratch_004");
        sfxClips["Scratch005"] = Resources.Load<AudioClip>("SFX/KenneyUI/scratch_005");
        sfxClips["Scroll001"] = Resources.Load<AudioClip>("SFX/KenneyUI/scroll_001");
        sfxClips["Scroll002"] = Resources.Load<AudioClip>("SFX/KenneyUI/scroll_002");
        sfxClips["Scroll003"] = Resources.Load<AudioClip>("SFX/KenneyUI/scroll_003");
        sfxClips["Scroll004"] = Resources.Load<AudioClip>("SFX/KenneyUI/scroll_004");
        sfxClips["Scroll005"] = Resources.Load<AudioClip>("SFX/KenneyUI/scroll_005");
        sfxClips["Select001"] = Resources.Load<AudioClip>("SFX/KenneyUI/select_001");
        sfxClips["Select002"] = Resources.Load<AudioClip>("SFX/KenneyUI/select_002");
        sfxClips["Select003"] = Resources.Load<AudioClip>("SFX/KenneyUI/select_003");
        sfxClips["Select004"] = Resources.Load<AudioClip>("SFX/KenneyUI/select_004");
        sfxClips["Select005"] = Resources.Load<AudioClip>("SFX/KenneyUI/select_005");
        sfxClips["Select006"] = Resources.Load<AudioClip>("SFX/KenneyUI/select_006");
        sfxClips["Select007"] = Resources.Load<AudioClip>("SFX/KenneyUI/select_007");
        sfxClips["Select008"] = Resources.Load<AudioClip>("SFX/KenneyUI/select_008");
        sfxClips["Switch001"] = Resources.Load<AudioClip>("SFX/KenneyUI/switch_001");
        sfxClips["Switch002"] = Resources.Load<AudioClip>("SFX/KenneyUI/switch_002");
        sfxClips["Switch003"] = Resources.Load<AudioClip>("SFX/KenneyUI/switch_003");
        sfxClips["Switch004"] = Resources.Load<AudioClip>("SFX/KenneyUI/switch_004");
        sfxClips["Switch005"] = Resources.Load<AudioClip>("SFX/KenneyUI/switch_005");
        sfxClips["Switch006"] = Resources.Load<AudioClip>("SFX/KenneyUI/switch_006");
        sfxClips["Switch007"] = Resources.Load<AudioClip>("SFX/KenneyUI/switch_007");
        sfxClips["Tick001"] = Resources.Load<AudioClip>("SFX/KenneyUI/tick_001");
        sfxClips["Tick002"] = Resources.Load<AudioClip>("SFX/KenneyUI/tick_002");
        //sfxClips["Tick003"] = Resources.Load<AudioClip>("SFX/KenneyUI/tick_003"); Parece que el 003 no existe: del 2 salta al 4
        sfxClips["Tick004"] = Resources.Load<AudioClip>("SFX/KenneyUI/tick_004");
        sfxClips["Toggle001"] = Resources.Load<AudioClip>("SFX/KenneyUI/toggle_001");
        sfxClips["Toggle002"] = Resources.Load<AudioClip>("SFX/KenneyUI/toggle_002");
        sfxClips["Toggle003"] = Resources.Load<AudioClip>("SFX/KenneyUI/toggle_003");
        sfxClips["Toggle004"] = Resources.Load<AudioClip>("SFX/KenneyUI/toggle_004");
    }

    // MÈtodo privado para cargar la m˙sica de fondo directamente desde las carpetas
    private void LoadMusicClips()
    {
        // Los recursos (ASSETS) que se cargan en TIEMPO DE EJECUCI”N DEBEN ESTAR DENTRO de una carpeta denominada /Assets/Resources/Music
        //musicClips["MainTheme"] = Resources.Load<AudioClip>("Music/mainTheme");
    }

    // MÈtodo de la clase singleton para reproducir efectos de sonido
    public void PlaySFX(string clipName)
    {
        if (sfxClips.ContainsKey(clipName))
        {
            sfxSource.clip = sfxClips[clipName];
            sfxSource.Play();
        }
        else Debug.LogWarning("El AudioClip " + clipName + " no se encontrÅEen el diccionario de sfxClips.");
    }

    // MÈtodo de la clase singleton para reproducir m˙sica de fondo
    public void PlayMusic(string clipName)
    {
        if (musicClips.ContainsKey(clipName))
        {
            musicSource.clip = musicClips[clipName];
            musicSource.Play();
        }
        else Debug.LogWarning("El AudioClip " + clipName + " no se encontrÅEen el diccionario de musicClips.");
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

}

// -----------------------------------------
// EJEMPLOS DE USO °DESDE CUALQUIER SCRIPT!
// -----------------------------------------
//AudioManager.instance.PlayMusic("MainTheme");
//AudioManager.instance.PlaySFX("Jump");