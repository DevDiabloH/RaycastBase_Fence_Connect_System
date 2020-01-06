using System;
using UnityEngine;

public class FenceConnector : MonoBehaviour
{
    private Fence[] m_Fences;
    private LayerMask m_Mask;
    private readonly string m_LayerMaskName = "Fence";

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        FindNearbyFence();
    }

    private void OnDisable()
    {
        FindNearbyFence(false);
    }

    private void Initialize()
    {
        m_Fences = new Fence[Enum.GetNames(typeof(EDirection)).Length];

        try
        {
            m_Mask = 1 << LayerMask.NameToLayer(m_LayerMaskName);
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }

        for (int i=0; i<m_Fences.Length; i++)
        {
            EDirection _dir = (EDirection)i;

            if(null == this.transform.Find(_dir.ToString()))
            {
                //Debug.LogError("The fence must have " + m_Fences.Length + " directions, " +
                //    "There is no object in the " + _dir.ToString() + " direction.");
                m_Fences[i] = new Fence(_dir, null);
            }
            else
            {
                m_Fences[i] = new Fence(_dir, this.transform.Find(_dir.ToString()));
            }
        }
    }

    private void FindNearbyFence(bool isEnqble=true)
    {
        for(int i=0; i<m_Fences.Length; i++)
        {
            Vector3 origin = this.transform.position;
            Vector3 direction = m_Fences[i].GetVector();
            RaycastHit hit;
            
            if (Physics.Raycast(origin, direction, out hit, 1f, m_Mask))
            {
                //Debug.DrawRay(origin + (Vector3.up * 0.5f), direction, Color.green, 3f);
                Fence instance = hit.transform.GetComponent<FenceConnector>().GetRequest(direction);

                if(null != instance.transform)
                {
                    instance.isConnected = isEnqble;
                    m_Fences[i].isConnected = isEnqble;
                }
            }
            else
            {
                //Debug.DrawRay(origin + (Vector3.up * 0.5f), direction, Color.red, 3f);
                m_Fences[i].isConnected = false;
            }
        }
    }

    public Fence GetRequest(Vector3 directionVector)
    {
        if(null == m_Fences)
        {
            Initialize();
        }

        for(int i=0; i<m_Fences.Length; i++)
        { 
            if(-m_Fences[i].GetVector() == directionVector)
            {
                return m_Fences[i];
            }
        }

        return null;
    }
}
