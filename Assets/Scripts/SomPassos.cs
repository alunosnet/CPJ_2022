using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomPassos : MonoBehaviour
{
    //Array for audio clips
    public AudioClip[] clips;
    //index of the current clip
    public int NextClip = 0;
    //reference to the audio source
    AudioSource _audioSource;
    
    //play the next audio clip
    public void PlayNextClip()
    {
        //if there is a clip to play
            //play the clip
            _audioSource.PlayOneShot(clips[NextClip]);
            //increase the index
            NextClip++;
        if (NextClip >= clips.Length)
        {
            NextClip = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //reference to the audio source
        _audioSource = GetComponent<AudioSource>();
        //test if the audio source is null
        if (_audioSource == null)
        {
            //if it is null, create an audio source
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
