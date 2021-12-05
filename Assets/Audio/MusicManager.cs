using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{

    [SerializeField]
    List<AudioClip> musicTracks;

    [SerializeField]
    AudioMixer mixer;

    AudioSource audioSource;

    public enum Track
    { 
        Overworld,
        Encounter
    }


    private MusicManager() { }

    private static MusicManager instance =  null;

    public MusicManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<MusicManager>();
                DontDestroyOnLoad(instance.transform.root);
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    void DestroyAllClones()
    {
        MusicManager[] clones = FindObjectsOfType<MusicManager>();
        foreach(MusicManager clone in clones)
        {
            if(clone != Instance)
            {
                Destroy(clone.gameObject);
            }
        }
    }

    void onEnterEncounterHandler()
    {
        StartCoroutine(FadeInTrackOverDuration(Track.Encounter, 5));
    }

    void onExitEncounterHandler()
    {
        StartCoroutine(FadeInTrackOverDuration(Track.Overworld, 5));
    }


    // Start is called before the first frame update
    void Start()
    {

        audioSource = GetComponent<AudioSource>();

        DestroyAllClones();

        WorldTraveller traveller = FindObjectOfType<WorldTraveller>();
        traveller.onEnterEncounter.AddListener(onEnterEncounterHandler);
        traveller.onExitEncounter.AddListener(onExitEncounterHandler);
    }

    IEnumerator FadeInTrackOverDuration(Track trackID, float duration)
    {
        float timer = 0.0f;
        PlayTrack(trackID);

        while(timer < duration)
        {
            timer += Time.deltaTime;

            audioSource.volume = Mathf.SmoothStep(0.0f, 1.0f, timer / duration);

            yield return new WaitForEndOfFrame();
        }
        audioSource.volume = 1.0f;
    }

    public void PlayTrack(Track trackID)
    {
        audioSource.clip = musicTracks[(int)trackID];
        audioSource.Play();
    }

    public void SetMusicVolume(float volumeDB)
    {
        mixer.SetFloat("VolumeMusic", volumeDB);
    }

    public void SetMusicVolumeNormalized(float normalizedMusicVolume)
    {
        float volumeDB = Mathf.Lerp(-80, 20, normalizedMusicVolume);
        mixer.SetFloat("VolumeMusic", volumeDB);
    }
}
