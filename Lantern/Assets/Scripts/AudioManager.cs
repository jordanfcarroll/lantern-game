using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance { get; private set; }

    public Sound[] sounds;

	void Awake () 
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

		foreach (Sound s in sounds) 
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.pitch = s.pitch;
			s.source.volume = s.volume;
			s.source.loop = s.loop;
		}
	}

	public void Play (string name) {
		Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.Play();
	}

	public void PlayDelayStop (string name, float delay) {
		// Play sound for fixed amount of time
		Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.Play();

		if (delay > 0) {
			StartCoroutine(DelayStop(name, delay));
		}
	}

	public void Stop (string name) {
		Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.Stop();
	}

	public void FadeOut (string name) {
		Sound s = Array.Find(sounds, sound => sound.name == name);
		StartCoroutine(FadeVolume(name));
	}

	IEnumerator FadeVolume (string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
		while (s.source.volume > 0) {
			s.source.volume -= 0.01f;
            yield return null;
		}
		s.source.Stop();
	}

	IEnumerator DelayStop (string name, float seconds) {
		yield return new WaitForSeconds(seconds);
		Stop(name);
	}
	
}
