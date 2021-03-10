using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


//For each sound we want
//Audio Clip
//Audio Mixer Group as string to be mapped to the groups
//volume
//priority

public class AudioData
{
    public AudioClip clip;
    public AudioMixerGroup group;
    public float volume = 0.5f;
    public int priority = 10;   //Priority is an integer between 0 and 255. 0=highest priority, 255=lowest priority.
    public bool canMultipleInstances = false;  //if the audio clip can have multiple instances of it played at once 
}


public class AudioManager : MonoBehaviour
{

    //We want a max number of sounds to be played back at once per group type based on importance
    const int maxSoundsPerMixerGroup = 16;

    //List of name of 
    //public Dictionary<string, AudioMixerGroup> mixerGroups = new Dictionary<string, AudioMixerGroup>();

    public Dictionary<string, AudioData> sounds = new Dictionary<string, AudioData>();

    public Dictionary<string, AudioSource> soundsPlaying = new Dictionary<string, AudioSource>();


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (var channel in soundsPlaying)
        {
            if (channel.Value.isPlaying == false)
            {
                soundsPlaying.Remove(channel.Key);
            }
        }
    }

    //returns true if sound doesn't exist and was added successfully
    public bool RegisterSound(string soundName, AudioData sound)
    {
        bool newKey = !sounds.ContainsKey(soundName);
        //if the sound doesn't exist already
        if (newKey)
        {
            sounds.Add(soundName, sound);
        }
        return newKey;
    }

    public void PlaySound(string sound)
    { 
        AudioData clip;
        
        //if we find the clip
        if(sounds.TryGetValue(sound, out clip))
        {
            AudioSource source;
            if (soundsPlaying.TryGetValue(sound, out source))
            {

            }

            if(!source.isPlaying)
            {
                source.priority = clip.priority;
                source.outputAudioMixerGroup = clip.group;
                source.PlayOneShot(clip.clip, clip.volume);
                return;
            }
        }
    }
}
