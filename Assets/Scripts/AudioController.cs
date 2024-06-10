using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource BGM;

    public AudioClip buttonClick;

    private void Start()
    {
        BGM.Play();
    }

    public void ButtonSFX()
    {
        BGM.PlayOneShot(buttonClick);
    }
}
