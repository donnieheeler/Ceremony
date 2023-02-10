using UnityEngine;
using UnityEngine.UI;

public class Volume_Slider : MonoBehaviour
{
    // Volume slider parameters
    public Slider volumeSlider;

    // Audio parameters
    public AudioSource audioSource;

    // Initializes and assigns the volume slider value to the audioSource volume
    private void Start()
    {
        volumeSlider.value = audioSource.volume;
    }

    // Sets the audio source volume based on the volume slider value
    public void SetVolume()
    {
        audioSource.volume = volumeSlider.value;
    }
}
