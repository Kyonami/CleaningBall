using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    #region Components
    [SerializeField]
    private Slider theSlider = null;
    #endregion

    #region Properties
    public Slider TheSlider { get => theSlider; set => theSlider = value; }
    #endregion
    
    void Awake()
    {
        SoundManager.Instance.SetAudioSlider(theSlider);
    }
}
