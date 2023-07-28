using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAudio : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> clips = new List<AudioClip>();
    
    private AudioSource audioSource;
    private int clip;
    private bool audioReady = false;

    private void OnEnable()
    {
        PlayRandomizeAudioClip();
    }

    private void Awake()
    {
        // Pick random Captain A audio clip
        int clipsCount = clips.Count - 1;

        if (clipsCount > 0)
        {
            var ran1 = Random.Range(0, clipsCount);
            var ran2 = Random.Range(0, clipsCount);
            if (ran1 < ran2)
            {
                clip = ran2 - ran1;
            }
            else
            {
                clip = ran1 - ran2;
            }
        }
        else
        {
            Debug.LogError("Captain-A: Random Audio Clip list is empty.");
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!audioSource.isPlaying && audioReady)
        {
            EventDispatcher.Instance.Dispatch(ConstantStrings.FADE_NEXT_SCENE, this, null);
            this.gameObject.SetActive(false);
        }
    }

    private void PlayRandomizeAudioClip()
    {
        // From Dispatch event 
        audioSource.clip = clips[clip];
        audioSource.Play();
        audioReady = true;
    }
}
