using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayBathroomAudios : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> bathroomClips = new List<AudioClip>();
    [Space]
    [SerializeField]
    private AudioSource extraBathSound;
    [SerializeField]
    private AudioSource fartSound;
    [Space]
    [SerializeField]
    private AudioClip extraBathSound1;
    [SerializeField]
    private AudioClip extraBathSound2;
    [SerializeField]
    private AudioClip extraBathSound3;
    [Space]
    [SerializeField]
    private AudioClip fartSound1;
    [SerializeField]
    private AudioClip fartSound2;
    [SerializeField]
    private AudioClip fartSound3;
    [Space]
    [SerializeField]
    private GameObject purrBalloon;
    [SerializeField]
    private GameObject arrow1;
    [SerializeField]
    private GameObject poopSplat;
    [SerializeField]
    private GameObject purrCat;
    [SerializeField]
    private GameObject grabCat;
    [Space]
    [SerializeField]
    private List<GameObject> fartBalloons;
    [SerializeField]
    private List<GameObject> wordBalloons;

    private List<BathroomAudio> extraSounds;
    private List<BathroomAudio> fartSounds;
    private AudioSource audioSource;
    private int index = 0;
    private int extraIndex = 0;
    private int fartIndex = 0;
    private int fartBalloonIndex = 0;
    private int wordBalloonIndex = 0;
    private float delay;
    private float playNext;
    private GameObject fartBalloon;
    private GameObject wordBalloon;
    private bool coroutineFinished;
    private int layerDefault;

    private void OnEnable()
    {
        // Set Extra and Fart sound data 
        extraSounds = new List<BathroomAudio>()
        {
            new BathroomAudio(extraBathSound1, false, 1, 0),
            new BathroomAudio(extraBathSound2, false, 1, -0.5f),
            new BathroomAudio(extraBathSound3, true, 0.25f, 0.5f)
        };

        fartSounds = new List<BathroomAudio>()
        {
            new BathroomAudio(fartSound1, false, 0.25f, 0),
            new BathroomAudio(fartSound2, false, 0.6f, 0),
            new BathroomAudio(fartSound3, false, 1, 0)
        };
    }

    private void Start()
    {
        layerDefault = LayerMask.NameToLayer("Default");

        audioSource = GetComponent<AudioSource>();
        delay = 3.25f;
        audioSource.clip = bathroomClips[index];
        audioSource.PlayDelayed(delay);

        // Show Text
        wordBalloon = wordBalloons[wordBalloonIndex];
        StartCoroutine(ActivateWordBalloonDelay(wordBalloon, (delay + 2.5f), 7.5f));

        playNext = audioSource.clip.length + delay + 0.75f;
        index++;
        Invoke("PlayNextClip", playNext);
    }

    private void PlayNextClip()
    {
        // Main Audio (Captain A)
        audioSource.clip = bathroomClips[index];
        coroutineFinished = false;

        // default delay
        delay = 0.25f;
        if (index == (int)Enums.BathroomAudioClip.Clip4)
        {
            // Set clips, etc...
            SetNextExtraBathSound(extraIndex);

            delay = 0.75f;
            var doorKnock = audioSource.clip.length + delay;
            // Door Knock
            extraBathSound.PlayDelayed(doorKnock);
            ShowNextWordBalloon();
            StartCoroutine(ActivateWordBalloonDelay(wordBalloon, 3f, 4f));
        }
        if (index == (int)Enums.BathroomAudioClip.Clip7)
        {
            // Fart3 sound
            fartSound.PlayDelayed(1.5f);
            ShowNextFartBalloon();
            StartCoroutine(ActivateFartBalloonDelay(fartBalloon, 1.90f));
        }
        if (index == (int)Enums.BathroomAudioClip.Clip9)
        {
            // Set clips, etc...
            SetNextExtraBathSound(extraIndex);
            // Cat purring
            extraBathSound.Play();
            purrBalloon.SetActive(true);

            delay = 2.2f;
            StartCoroutine(ShowArrowDelay());
        }
        // Play main audio (Captain A)
        audioSource.PlayDelayed(delay);


        if (index == (int)Enums.BathroomAudioClip.Clip2)
        {
            // Show Fart Balloon
            fartBalloon = fartBalloons[fartBalloonIndex];
            StartCoroutine(ActivateFartBalloonDelay(fartBalloon, delay));

            // Set clips, etc...
            SetNextExtraBathSound(extraIndex);
            SetNextFartSound(fartIndex);

            // Water dibbles
            extraBathSound.Play();

            var fartDelay = 8.75f;
            // Fart1 sound
            fartSound.PlayDelayed(fartDelay);
        }
        if (index == (int)Enums.BathroomAudioClip.Clip3)
        {
            // Show Next Fart Balloon
            ShowNextFartBalloon();
            StartCoroutine(ActivateFartBalloonDelay(fartBalloon, 1.25f));

            // Wait to Show Next Fart Balloon
            StartCoroutine(ActivateFartBalloonWait(2f));
        }
        if (index == (int)Enums.BathroomAudioClip.Clip6)
        {
            float setDelay = 6f;
            // Set clips, etc...
            SetNextFartSound(fartIndex);
            var nextFart = fartSound.clip.length + setDelay + 0.5f;
            // Fart2 sound
            fartSound.PlayDelayed(setDelay);

            // Disable Living Particle System
            fartBalloons[1].SetActive(false);

            // Show Fart Balloon
            ShowNextFartBalloon();
            StartCoroutine(ActivateFartBalloonDelay(fartBalloon, setDelay));

            StartCoroutine(SetNextFartSoundDelay(nextFart));
        }
        if (index == (int)Enums.BathroomAudioClip.Clip8)
        {
            ShowNextWordBalloon();
            StartCoroutine(ActivateWordBalloonDelay(wordBalloon, 0.01f, 8f));
            wordBalloonIndex++;
            wordBalloon = wordBalloons[wordBalloonIndex];
            StartCoroutine(ActivateWordBalloonDelay(wordBalloon, 0.01f, 12f));
        }

        // Play first clip in Mono Start
        if (index < (bathroomClips.Count-1))
        {
            playNext = audioSource.clip.length + delay;
            Invoke("PlayNextClip", playNext);
            index++;
        }
    }

    private void SetNextExtraBathSound(int index)
    {
        // Extra Bathroom Sounds
        extraBathSound.clip = extraSounds[index].audioClip;
        extraBathSound.loop = extraSounds[index].loop;
        extraBathSound.volume = extraSounds[index].volume;
        extraBathSound.panStereo = extraSounds[index].stereoPan;

        extraIndex++;
    }

    private void SetNextFartSound(int index)
    {
        // Fart Sounds
        fartSound.clip = fartSounds[index].audioClip;
        fartSound.loop = fartSounds[index].loop;
        fartSound.volume = fartSounds[index].volume;
        fartSound.panStereo = fartSounds[index].stereoPan;

        fartIndex++;
    }

    private IEnumerator ShowArrowDelay()
    {
        poopSplat.SetActive(true);
        var arrowDelay = (audioSource.clip.length + delay) - 4;
        yield return new WaitForSeconds(arrowDelay);
        purrCat.SetActive(false);
        grabCat.layer = layerDefault;
        arrow1.SetActive(true);
    }

    private IEnumerator SetNextFartSoundDelay(float wait)
    {
        yield return new WaitForSeconds(wait);
        // Set clips, etc...
        SetNextFartSound(fartIndex);
        // Fart3 sound
        fartSound.Play();
        // Show Fart Balloon
        ShowNextFartBalloon();
        fartBalloon.SetActive(true);
    }

    private IEnumerator ActivateFartBalloonDelay(GameObject fartBalloon, float wait)
    {
        yield return new WaitForSeconds(wait);
        fartBalloon.SetActive(true);
        coroutineFinished = true;
    }

    private IEnumerator ActivateFartBalloonWait(float wait)
    {
        // Stop Coroutine process when we execute Rat Balloon
        if (fartBalloonIndex < 3)
        {
            yield return new WaitUntil(() => coroutineFinished);
            fartBalloonIndex++;
            fartBalloon = fartBalloons[fartBalloonIndex];
            coroutineFinished = false;

            StartCoroutine(ActivateFartBalloonDelay(fartBalloon, wait));

            // Wait to Show Next Fart Balloon
            StartCoroutine(ActivateFartBalloonWait(4f));
        }
    }

    private IEnumerator ActivateWordBalloonDelay(GameObject wordBalloon, float fadeIn, float fadeOut)
    {
        
        yield return new WaitForSeconds(fadeIn);
        wordBalloon.SetActive(true);
        yield return new WaitForSeconds(fadeOut);
        wordBalloon.GetComponent<Animator>().SetTrigger("FadeOut");
    }

    private void ShowNextFartBalloon()
    {
        fartBalloons[fartBalloonIndex].SetActive(false);
        fartBalloonIndex++;
        fartBalloon = fartBalloons[fartBalloonIndex];
    }

    private void ShowNextWordBalloon()
    {
        wordBalloons[wordBalloonIndex].SetActive(false);
        wordBalloonIndex++;
        wordBalloon = wordBalloons[wordBalloonIndex];
    }

    // Make a class to contain no more than 2 Audio Sources
    private class BathroomAudio
    {
        public AudioClip audioClip;
        public bool loop;
        public float volume;
        public float stereoPan;

        public BathroomAudio(AudioClip audioClip, bool loop, float volume, float stereoPan)
        {
            this.audioClip = audioClip;
            this.loop = loop;
            this.volume = volume;
            this.stereoPan = stereoPan;
        }
    }
}
