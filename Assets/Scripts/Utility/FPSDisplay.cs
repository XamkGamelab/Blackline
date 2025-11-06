using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _fpsText;
    [SerializeField]
    private float _updateDelay;

    private float _frameRate;
    private float _updateTimer;

    private void Update()
    {
        if (_updateTimer < _updateDelay)
        {
            _updateTimer += Time.deltaTime;
            return;
        }

        _frameRate = Mathf.RoundToInt(1f / Time.deltaTime);
        _fpsText.text = _frameRate.ToString();
        _updateTimer = 0f;
    }
}
