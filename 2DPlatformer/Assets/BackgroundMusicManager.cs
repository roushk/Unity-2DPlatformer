using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BackgroundMusicManager : MonoBehaviour
{
    public GameObject combatMusic;
    public GameObject explorationMusic;

    public AudioMixerGroup combatMusicAMG;
    public AudioMixerGroup explorationMusicAMG;

    public AnimationCurve volumeFromCurve;
    public AnimationCurve volumeToCurve;

    bool fadingToCombat = false;

    float onCombatMusicVolume;
    float onExplorationMusicVolume;

    float offCombatMusicVolume = -80.0f;
    float offExplorationMusicVolume = -80.0f;

    public void PlayCombatMusic()
    {
        StartCoroutine(StartFade(true, 1.0f));
    }

    public void PlayExplorationMusic()
    {
        StartCoroutine(StartFade(false, 5.0f));
    }

    public IEnumerator StartFade(bool toCombat, float duration)
    {
        fadingToCombat = toCombat;
        AudioMixer fromAudioMixer;
        AudioMixer toAudioMixer;
        float currentTime = 0;

        float currentFromVol = 0;
        float currentToVol = 0;

        float goalFromVol = 0;
        float goalToVol = 0;

        string fromParam;
        string toParam;

        bool currentlyFadingToCombat = toCombat;
        if (toCombat)
        {
            fadingToCombat = true;
            fromAudioMixer = explorationMusicAMG.audioMixer;
            toAudioMixer = combatMusicAMG.audioMixer;

            fromParam = "ExplorationVolume";
            toParam = "CombatVolume";

            fromAudioMixer.GetFloat(fromParam, out currentFromVol);
            toAudioMixer.GetFloat(toParam, out currentToVol);

            goalFromVol = offExplorationMusicVolume;
            goalToVol = onCombatMusicVolume;
        }
        else
        {
            fromParam = "CombatVolume";
            toParam = "ExplorationVolume";

            fromAudioMixer = combatMusicAMG.audioMixer;
            toAudioMixer = explorationMusicAMG.audioMixer;

            fromAudioMixer.GetFloat(fromParam, out currentFromVol);
            toAudioMixer.GetFloat(toParam, out currentToVol);

            goalFromVol = offCombatMusicVolume;
            goalToVol = onExplorationMusicVolume;
        }


        //Make sure that we are going the correct direction, if needs to stop than the other coroutine will take over
        while (currentTime < duration && currentlyFadingToCombat == fadingToCombat)
        {
            currentTime += Time.deltaTime;

            //change from linear to passed in curves for cleaner transition
            float newFromVol = Mathf.Lerp(currentFromVol, goalFromVol, volumeFromCurve.Evaluate(currentTime / duration));
            float newToVol = Mathf.Lerp(currentToVol, goalToVol, volumeToCurve.Evaluate(currentTime / duration));

            toAudioMixer.SetFloat(toParam,newToVol);
            fromAudioMixer.SetFloat(fromParam, newFromVol);

            yield return null;
        }
        yield break;
    }


    // Start is called before the first frame update
    void Start()
    {
        explorationMusicAMG = explorationMusic.GetComponent<AudioSource>().outputAudioMixerGroup;
        combatMusicAMG = combatMusic.GetComponent<AudioSource>().outputAudioMixerGroup;

        explorationMusicAMG.audioMixer.GetFloat("ExplorationVolume", out onExplorationMusicVolume);

        combatMusicAMG.audioMixer.GetFloat("CombatVolume", out onCombatMusicVolume);

        combatMusicAMG.audioMixer.SetFloat("CombatVolume", offCombatMusicVolume);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
            StartCoroutine(StartFade(true, 1.0f));  //to combat
        if (Input.GetKeyDown(KeyCode.H))
            StartCoroutine(StartFade(false, 1.0f)); //to background
        if (Input.GetKeyDown(KeyCode.J))
            StartCoroutine(StartFade(true, 3.0f));  //to combat slow
        if (Input.GetKeyDown(KeyCode.K))
            StartCoroutine(StartFade(false, 3.0f)); //to background slow

    }
}
