using System;
using UnityEngine;

public enum EDirection { Forward, Back, Right, Left }

[Serializable]
public class Fence
{
    public EDirection direction;
    public Transform transform;
    public bool isConnected
    {
        get
        {
            return m_IsConnected;
        }
        set
        {
            m_IsConnected = value;

            if(null != transform)
            {
                transform.gameObject.SetActive(m_IsConnected);
            }
            else
            {
                //Debug.LogWarning(direction + " object transform is null.");
            }
        }
    }
    private bool m_IsConnected;

    public Fence(EDirection direction, Transform transform)
    {
        this.direction = direction;
        this.transform = transform;
        this.isConnected = false;
    }

    public Vector3 GetVector()
    {
        switch (direction)
        {
            case EDirection.Forward:
                return Vector3.forward;
            case EDirection.Back:
                return Vector3.back;
            case EDirection.Right:
                return Vector3.right;
            case EDirection.Left:
                return Vector3.left;
            default:
                return Vector3.zero;
        }
    }
}
