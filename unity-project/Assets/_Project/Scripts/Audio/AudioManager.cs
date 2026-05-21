using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using SignalLost.Core;

namespace SignalLost.Audio
{
    /// <summary>
    /// Pooled one-shot + cued ambience player. Assign AudioMixerGroups for music/sfx/voice in Inspector.
    /// </summary>
    public class AudioManager : MonoBehaviour, IService
    {
        [SerializeField] private AudioMixerGroup sfxGroup;
        [SerializeField] private AudioMixerGroup voiceGroup;
        [SerializeField] private AudioMixerGroup ambienceGroup;
        [SerializeField] private int oneShotPoolSize = 16;

        private readonly Queue<AudioSource> _pool = new();
        private AudioSource _ambience;
        private AudioSource _voice;

        public void Register() => Services.Register(this);

        private void Awake()
        {
            for (int i = 0; i < oneShotPoolSize; i++)
            {
                var src = NewSource("OneShot_" + i, sfxGroup);
                _pool.Enqueue(src);
            }
            _ambience = NewSource("Ambience", ambienceGroup); _ambience.loop = true;
            _voice    = NewSource("Voice", voiceGroup);
        }

        private AudioSource NewSource(string n, AudioMixerGroup g)
        {
            var go = new GameObject(n);
            go.transform.SetParent(transform, false);
            var s = go.AddComponent<AudioSource>();
            s.playOnAwake = false;
            s.outputAudioMixerGroup = g;
            return s;
        }

        public void PlayOneShot(AudioClip clip, Vector3 worldPos, float volume = 1f)
        {
            if (clip == null || _pool.Count == 0) return;
            var s = _pool.Dequeue();
            s.transform.position = worldPos;
            s.clip = clip;
            s.volume = volume;
            s.spatialBlend = 1f;
            s.Play();
            _pool.Enqueue(s);
        }

        public void PlayAmbience(AudioClip clip, float volume = 0.6f)
        {
            if (clip == null) return;
            _ambience.clip = clip;
            _ambience.volume = volume;
            _ambience.spatialBlend = 0f;
            _ambience.Play();
        }

        public void PlayVoice(AudioClip clip, float volume = 1f)
        {
            if (clip == null) return;
            _voice.clip = clip;
            _voice.volume = volume;
            _voice.spatialBlend = 0f;
            _voice.Play();
        }
    }
}
