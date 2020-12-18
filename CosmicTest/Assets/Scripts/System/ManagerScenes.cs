using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScenes : Singleton<ManagerScenes> 
{
    /// <summary>
    /// Listado de las operaciones asincronas realizadas por el GameManager
    /// </summary>
    [SerializeField] private List<AsyncOperation> _loadOperations;

    /// <summary>
    /// Lista de prefabs Utiles de la Scena
    /// </summary>
    public List<GameObject> systemPrefabs;

    /// <summary>
    /// Nombre de la escena que se encuentra en ejecución
    /// </summary>
    string _currentLevelName;

    /// <summary>
    /// Nombre del nivel anterior
    /// </summary>
    string _lastLevelName;

    /// <summary>
    /// Barra de carga para la pantalla Loading
    /// </summary>
    UnityEngine.UI.Slider slider;

    /// <summary>
    /// Variable que define si el juego esta en ejecucion.
    /// </summary>
    [SerializeField] private bool is_pause;

    /// <summary>
    /// Varirable que almacena el nombre de la scen principal a carga.
    /// </summary>
    [SerializeField]
    string mainLevel;

    /// <summary>
    /// Propiedad que retorna el nombre de la escena que se encuentra en ejecución.
    /// </summary>
    /// <value>Nombre de la escena.</value>
    public string CurrentLevelName { get { return _currentLevelName; } }

    /// <summary>
    /// Propiedad que retorna el estado de ejecución (T&F). 
    /// </summary>
    public bool IsPaused { get { return is_pause; } }

    void Start()
    {
        _currentLevelName = string.Empty;
        _loadOperations = new List<AsyncOperation>();
        EditPrefabsGame();
    }

    /// <summary>
    /// Método que permite editar los prefabs necesarios para la correcta ejecución del juego
    /// </summary>
    void EditPrefabsGame()
    {
        for (int i = 0; i < systemPrefabs.Count; i++)
        {
            if (systemPrefabs[i].name.Equals("ManagerBaseData") || systemPrefabs[i].name.Equals("ManagerSound"))
            {
                systemPrefabs[i].SetActive(true);
            }
            else
            {
                systemPrefabs[i].SetActive(false);
            }
        }

        slider = systemPrefabs[1].GetComponentInChildren<UnityEngine.UI.Slider>();

        LoadLevel(mainLevel);
    }

    /// <summary>
    /// Delegado del método LoadLevel que se ejecuta una vez se ha terminado de cargar una escena asincronamente.
    /// </summary>
    /// <param name="ao">Objeto que contiene la información de la escena cargada.</param>
    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
            _loadOperations.Remove(ao);

        ValidateLevel();
        Debug.Log("[GameManager] Escena Cargada completamente");
    }

    /// <summary>
    /// Delegado del método UnLoadLevel que se ejecuta una vez se ha terminado de descargar una escena asincronamente.
    /// </summary>
    /// <param name="ao">Objeto que contiene la información de la escena cargada.</param>
    void OnUnLoadOperationComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
            _loadOperations.Remove(ao);
        
        Debug.Log("[GameManager] Escena descargada completamente");
    }

    /// <summary>
    /// Método que permite cargar una escena de manera asincrona
    /// </summary>
    /// <param name="levelName">Nombre de la escena que se desea cargar.</param>
    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        if (ao == null)
        {
            Debug.Log("[GameManager] Error al cargar el nivel" + levelName);
            return;
        }

        ao.completed += OnLoadOperationComplete;
        StartCoroutine(LoadingScreen(ao));
        _loadOperations.Add(ao);
        _lastLevelName = _currentLevelName;
        _currentLevelName = levelName;
    }

    /// <summary>
    /// Método que permite descargar una escena de manera asincrona.
    /// </summary>
    /// <param name="levelName">Nombre de la escena que se desea descargar.</param>
    public void UnLoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        ao.completed += OnUnLoadOperationComplete;
    }

    /// <summary>
    /// Método que permite salir del juego.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Método que permite cambiar la calidad de graficas.
    /// </summary>
    /// <param name="qualityIndex">Numero de calidad grafica segun ployect settings.</param>
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    /// <summary>
    /// Método que permite validar el nivel cargado para determinar las acciones a realizar.
    /// </summary>
    void ValidateLevel()
    {
        if (_lastLevelName != "")
            UnLoadLevel(_lastLevelName);

        ManagerSound.Instance.DeleteSoundsLevel();

        switch (CurrentLevelName)
        {
            case "MainMenu":
                ManagerSound.Instance.CreateSoundsLevel(MusicLevel.MAINMENU);
                ManagerSound.Instance.RandomBackGroundSound();
                break;

            case "EscenarioHuertoTest":
                ManagerSound.Instance.CreateSoundsLevel(MusicLevel.GAME);
                ManagerSound.Instance.PlayNewSound("MainGame");
                break;
        }
    }

    /// <summary>
    /// Método generado por si existe algun error.
    /// </summary>
    public void LoadErrorScene()
    {
        SceneManager.LoadScene("Error");
    }

    /// <summary>
    /// Pausa la ejecución de la aplicación
    /// </summary>
    public void Pause()
    {
        is_pause = !is_pause;

        ManagerSound.Instance.PauseAllSounds(is_pause);
        systemPrefabs[0].SetActive(is_pause);

        if (is_pause)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
   
    /// <summary>
    /// Coroutine para la pantalla de carga
    /// </summary>
    /// <returns>Proceso de carga</returns>
    /// <param name="ao">Asyncronous Operation object</param>
    IEnumerator LoadingScreen(AsyncOperation ao)
    {
        systemPrefabs[1].SetActive(true);
        ao.allowSceneActivation = false;

        while(ao.isDone == false)
        {
            slider.value = ao.progress;

            if(ao.progress.Equals(0.9f))
            {
                slider.value = 1f;
                ao.allowSceneActivation = true;
                systemPrefabs[1].SetActive(false);
            }
            yield return null;
        }
    }
}