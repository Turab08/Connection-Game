using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Music Tracks")]
    public AudioClip mainMenuMusic;
    public AudioClip levelMusic;

    [Header("SFX Tracks")]
    public AudioClip plugIn;
    
    void Awake()
    {
        // Singleton pattern: ensures only one AudioManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // This is the magic line
            
            // Add an AudioSource if one isn't already attached
            sfxSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        // Subscribe: "Hey SceneManager, call this method when a scene changes."
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe: Always clean up after yourself!
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name); // Check your console!
        
        if (scene.name == "MainMenu") 
        {
            PlayMusic(mainMenuMusic);
        }
        else 
        {
            PlayMusic(levelMusic);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        // If the track is already playing, don't restart it
        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
