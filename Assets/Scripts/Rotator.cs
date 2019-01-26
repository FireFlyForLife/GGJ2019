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
    [SerializeField] private PlayerManager player;
    private float m_target;
    private bool m_isBusy = false;

    void Start()
    {
        m_target = m_rotation1;
    }

    IEnumerator TurnTowardsTarget(float rotation)
    {
        yield return null;
    }

    void OnTriggerEnter(Collider c)
    {
        if (!m_isBusy)
        {
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
            m_cam.transform.parent.localEulerAngles =  new Vector3(0, m_target, 0);
            c.transform.position = transform.position;
            //c.transform.position =  new Vector3(transform.position.x, c.transform.position.y, transform.position.z);
            //m_cam.transform.position = c.transform.position + c.transform.InverseTransformVector(new Vector3(0, 0, 10));
            //if (Math.Abs(m_target - m_rotation1) < 0.001)
            //{
            //    m_target = m_rotation2;
            //    if(m_wallToEnable)m_wallToEnable.SetActive(false);
            //}
            //else
            //{
            //    m_target = m_rotation1;
            //    if (m_wallToEnable) m_wallToEnable.SetActive(true);
            //}
        }
    }
}
