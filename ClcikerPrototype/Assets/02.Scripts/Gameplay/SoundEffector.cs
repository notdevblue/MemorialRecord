using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffector : MonoBehaviour
{
    Dictionary<string, AudioClip> soundEffects = new Dictionary<string, AudioClip>();

    [SerializeField] AudioSource sourcePrefab = null;

    Queue<AudioSource> sourcePool = new Queue<AudioSource>();
    
    private void Awake()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Sound/SoundEffect");

        for (int i = 0; i < clips.Length; i++)
        {
            Debug.Log(clips[i].name);
            soundEffects.Add(clips[i].name, clips[i]);
        }
    }

    public void PlaySoundEffect(string name = "")
    {

    }

    public AudioSource GetAudioClip()
    {
        AudioSource result = null;
        if(sourcePool.Count > 0)
        {
            if(!sourcePool.Peek().gameObject.activeSelf)
            {
                result = sourcePool.Dequeue();
            }
            else
            {
                result = InstantiateAudioClip();
            }
        }
        else
        {
            result = InstantiateAudioClip();
        }

        return result;
    }

    public AudioSource InstantiateAudioClip()
    {
        AudioSource result = Instantiate(sourcePrefab, transform);

        result.volume = SaveManager.SoundEffectVolume;
        sourcePool.Enqueue(result);

        return result;
    }
}
