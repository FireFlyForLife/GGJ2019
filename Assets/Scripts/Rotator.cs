using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Camera m_cam;
    [SerializeField] private GameObject m_wallToEnable;
    [SerializeField] private float m_rotation1;
    [SerializeField] private float m_rotation2;
    [SerializeField] private float m_rotSpeed = 5;
    private float m_target;
    private bool m_isBusy = false;

    void Start()
    {
        m_target = m_rotation1;
    }

    void OnTriggerEnter(Collider c)
    {
        PlatformerPlayer p = c.GetComponent<PlatformerPlayer>();
        if (!m_isBusy && p)
        {
            m_cam.transform.parent.localEulerAngles =  new Vector3(0, m_target, 0);
            p.transform.position =  new Vector3(transform.position.x, p.transform.position.y, transform.position.z);
            if (Math.Abs(m_target - m_rotation1) < 0.001)
            {
                m_target = m_rotation2;
                if(m_wallToEnable)m_wallToEnable.SetActive(false);
            }
            else
            {
                m_target = m_rotation1;
                if (m_wallToEnable) m_wallToEnable.SetActive(true);
            }
        }
    }

    private IEnumerator Rotate(PlatformerPlayer p, float target)
    {
        while (m_isBusy)
        {
            float diff = m_target - p.transform.eulerAngles.y;
            float delta = diff / diff * m_rotSpeed * Time.deltaTime;

            if (Mathf.Abs(diff) <= delta)
                p.transform.localEulerAngles = new Vector3(0, m_target, 0);
            else p.transform.localEulerAngles += new Vector3(0,delta,0);
            yield return new WaitForEndOfFrame();
        }
    }
}
