using UnityEngine;
using UnityEngine;
using System.Collections;

public class PlayRandomSound : MonoBehaviour
{
				public AudioClip[] clipList;
			
				public void PlayAudio()
				{
                int rand = Random.Range(0, 6000) % clipList.Length;
                GetComponent<AudioSource>().clip = clipList[rand];
                GetComponent<AudioSource>().Play();
    }
}
