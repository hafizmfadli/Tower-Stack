using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] AudioSource backsound;

    public void setVolume(float _volume)
    {
        backsound.volume = _volume;
    }
}
