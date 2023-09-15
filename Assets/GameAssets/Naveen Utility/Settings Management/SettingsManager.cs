using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;


public class SettingsManager : MonoBehaviour
{
    public Text QualityLevelText;

    public AudioMixer audioMix;
    // Start is called before the first frame update
    void Start()
    {
        ChangeQualityLevelText();
    }

    public void SetVolume(float v)
    {
        audioMix.SetFloat("volume", v);
    }

    public void IncreaseQualityLevel()
    {
        QualitySettings.IncreaseLevel(false);
        ChangeQualityLevelText();
    }

    public void DecreaseQualityLevel()
    {
        QualitySettings.DecreaseLevel(false);
        ChangeQualityLevelText();
    }


    private void ChangeQualityLevelText()
    {
        QualityLevelText.text = QualitySettings.names[QualitySettings.GetQualityLevel()];

    }
}
