using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    AudioSource theAudioSource = null;
    [SerializeField]
    Slider theSlider = null;
    [SerializeField]
    float volume = 0.5f;

    void Awake()
    {
        DontDestroyOnLoad(Instance);
        if (Instance != this)
            Destroy(gameObject);
    }
    public void SetAudioSlider(Slider _slider)
    {
        theSlider = _slider;
        theSlider.onValueChanged.AddListener(delegate { ChangeAudioScale(); });
        theSlider.value = volume;
    }
    public void ChangeAudioScale()
    {
        volume = theSlider.value;
        theAudioSource.volume = volume;
    }
}
