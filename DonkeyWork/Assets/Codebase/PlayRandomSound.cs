using UnityEngine;
using UnityEngine;
using System.Collections;

public class PlayRandomSound : MonoBehaviour
{
    public AudioClip[] clipList;
    private bool playedSound;
    public bool playOnEnable;

    void OnEnable()
    {
        if (playOnEnable) PlayAudio();
    }
    public void PlayAudio()
    {
        int rand = Random.Range(0, 6000) % clipList.Length;
        GetComponent<AudioSource>().clip = clipList[rand];
        GetComponent<AudioSource>().Play();
    }
    void OnTriggerEnter(Collider c)
    {
      
        if (c.transform.tag == "Player" && !playedSound)
        {
            PlayAudio();

        }
    }
}
