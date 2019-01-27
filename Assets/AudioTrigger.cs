using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{

    private AudioSource m_source;
    private bool m_hasPlayed = false;
	// Use this for initialization
	void Start ()
	{
	    m_source = GetComponent<AudioSource>();
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<PlayerManager>())
        {
            m_source.Play();
            m_hasPlayed = false;
        }
    }
}
