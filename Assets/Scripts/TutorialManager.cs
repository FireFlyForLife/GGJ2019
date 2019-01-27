using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private int currentId = 0;
    private int targetId = 0;
    [SerializeField] private RectTransform m_panel;
    [SerializeField] private UnityEngine.UI.Text m_text;
    [SerializeField] private float m_maxHeight = 100;
    [SerializeField] private float m_lerpSpeed = 1;
    [SerializeField] private float m_instructionTime = 1;
    [SerializeField] private string[] m_instructions;
    private float m_currentHeight;
    private float m_currentDuration;

	// Use this for initialization
	void Start ()
	{
	    m_currentDuration = m_instructionTime;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (targetId > currentId)
	    {
	        if (MoveScreen(true))
	        {
	            m_text.text = m_instructions[currentId];
	            m_currentDuration -= Time.deltaTime;
	            if (m_currentDuration <= 0)
	            {
	                currentId += 1;
	                m_currentDuration = m_instructionTime;
	                m_text.text = "";
	            }
	        }
	    }
	    else MoveScreen(false);
	}


    bool MoveScreen(bool visible)
    {
        if (visible)
        {
            if (m_currentHeight < m_maxHeight)
            {
                float diff = m_maxHeight - m_currentHeight;
                float delta = m_lerpSpeed * Time.deltaTime;
                if (delta >= diff) m_currentHeight = m_maxHeight;
                else  m_currentHeight += delta;
                m_panel.sizeDelta = new Vector2(m_panel.sizeDelta.x, m_currentHeight);
                return false;
            }
            else return true;
        }
        else
        {
            if (m_currentHeight > 0)
            {
                float delta = m_lerpSpeed * Time.deltaTime;
                if (delta > m_currentHeight) m_currentHeight = 0;
                else m_currentHeight -= delta;
                m_panel.sizeDelta = new Vector2(m_panel.sizeDelta.x,m_currentHeight);
                return false;
            }
            else return true;
        }
    }

    public void TriggerIndex(int idx)
    {
        if (idx > targetId)
            targetId = idx;
    }
}
