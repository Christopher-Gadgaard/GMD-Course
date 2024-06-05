using UnityEngine;

namespace Game.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Sound Effects")]
        public AudioClip bombExplodeClip;
        public AudioClip pickupItemClip;
        public AudioClip gameOverClip;
        
        [Header("Background Music")]
        public AudioClip backgroundMusicClip;

        private AudioSource effectsSource;
        private AudioSource musicSource;

        [Header("Volume Levels")]
        [Range(0f, 1f)] public float effectsVolume = 1f;
        [Range(0f, 1f)] public float musicVolume = 1f;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            effectsSource = gameObject.AddComponent<AudioSource>();
            musicSource = gameObject.AddComponent<AudioSource>();
            
            effectsSource.volume = effectsVolume;
            musicSource.volume = musicVolume;

            PlayBackgroundMusic();
        }

        public void PlaySound(AudioClip clip)
        {
            effectsSource.PlayOneShot(clip);
        }

        private void PlayBackgroundMusic()
        {
            musicSource.clip = backgroundMusicClip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
}