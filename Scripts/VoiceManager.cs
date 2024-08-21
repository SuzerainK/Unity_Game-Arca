using System.Collections;
using System;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    public Sound[] sounds;

    public static VoiceManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void StopAll()
    {
        Sound s;
        int soundQueue = 0;
        while (soundQueue < sounds.Length)
        {
            s = sounds[soundQueue];
            s.source.Stop();
            soundQueue++;

        }
    }

    public void RaisePitch(string name, bool enabled)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        if (enabled)
        {

            s.source.pitch = 2;
        }
        else
        {
            s.source.pitch = 1;
        }
    }
}
