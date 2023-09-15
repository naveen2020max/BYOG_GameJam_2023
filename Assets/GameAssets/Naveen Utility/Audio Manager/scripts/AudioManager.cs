using UnityEngine.Audio;
using UnityEngine;

namespace Naveen
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;
        [Range(.1f, 3f)]
        public float pitch;
        public bool loop;

        [HideInInspector]
        public AudioSource source;
    }

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        public Sound[] sounds;

        public bool Suma => 1 > 10;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);

            foreach (Sound s in sounds)
            {
                GameObject go = new GameObject(s.name);
                go.transform.SetParent(transform);
                s.source = go.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }

        public void Play(string _name)
        {
            Sound s = System.Array.Find(sounds, sound => sound.name == _name);
            if (s == null)
                return;
            s.source.Play();
        }


    }
}
