using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectInvisible : MonoBehaviour
{
    public System.Action OnInvisible;
    void OnBecameInvisible()
    {
        OnInvisible?.Invoke();
    }
}
