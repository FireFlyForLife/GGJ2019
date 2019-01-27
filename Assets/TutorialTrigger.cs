using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private int m_idx;
    private TutorialManager m_manager;
    void Start()
    {
        m_manager = FindObjectOfType<TutorialManager>();
    }

	// Use this for initialization
    void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<PlayerManager>())
            m_manager.TriggerIndex(m_idx);
    }
}
