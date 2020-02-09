using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{
    public static SoundMgr _instance;
    
    //songs

    public AudioClip menuSong;
    public AudioClip levelSong;
    public AudioClip gameOverSong;

    //sfx

    public AudioClip bombUpSfx;
    public AudioClip fireSfx;
    public AudioClip sickSfx;
    public AudioClip skateSfx;
    public AudioClip bombExplosionSfx;
    public AudioClip bombermanDie;

    private AudioSource audioSource;

    private List<AudioSource> audioSfxList;

    public void Awake()
    {
        //only keep first copy of script
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
        if (audioSource == null)
        {
            audioSource = this.gameObject.AddComponent<AudioSource>();

            if (audioSource)
            {
                audioSource.playOnAwake = false;
            }
        }

        audioSfxList = new List<AudioSource>();

        DontDestroyOnLoad(this);

    }

    public void PlaySfx(AudioClip sfx)
    {

        //list is decrementing but not in inspector?

        if (audioSfxList.Count > 5)
        {
            for (int i = 0; i < 3; i++)//destroy first 3
            {
                var temp = audioSfxList[i];
                audioSfxList[i] = null;
                Destroy(temp);
                audioSfxList.Remove(audioSfxList[i]);
            }

        }

        AudioSource audioSrc = this.gameObject.AddComponent<AudioSource>();

        if (audioSrc != null)
        {
            if (sfx != null)
                audioSrc.PlayOneShot(sfx);
        }

        audioSfxList.Add(audioSrc);

    }

    public void PlaySong(AudioClip song)
    {
        if (audioSource != null)
        {
            if (song != null)
            {
                audioSource.clip = song;

                audioSource.loop = true;

                audioSource.Play();
            }
                
        }
    }

    public void ClearSfx()
    {
        for (int i = 0; i < audioSfxList.Count; i++)
        {
            var temp = audioSfxList[i];
                audioSfxList[i] = null;
                Destroy(temp);
        }
        audioSfxList.Clear();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
