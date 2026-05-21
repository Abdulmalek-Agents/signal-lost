using TMPro;
using UnityEngine;
using UnityEngine.UI;
using SignalLost.Core;

namespace SignalLost.UI
{
    /// <summary>
    /// Minimal HUD: awareness arc, scanner battery, interact prompt, mission progress.
    /// </summary>
    public class HUDController : MonoBehaviour, IService
    {
        [SerializeField] private Image awarenessFill;
        [SerializeField] private TMP_Text interactPrompt;
        [SerializeField] private TMP_Text progressLabel;
        [SerializeField] private TMP_Text subtitleLabel;

        [Header("Channels")]
        [SerializeField] private FloatEventChannel onAwarenessChanged;
        [SerializeField] private FloatEventChannel onMissionProgress;
        [SerializeField] private StringEventChannel onTranscriptShow;

        public void Register() => Services.Register(this);

        private void OnEnable()
        {
            if (onAwarenessChanged != null) onAwarenessChanged.OnRaised += SetAwareness;
            if (onMissionProgress != null) onMissionProgress.OnRaised += SetProgress;
            if (onTranscriptShow != null) onTranscriptShow.OnRaised += SetSubtitle;
        }
        private void OnDisable()
        {
            if (onAwarenessChanged != null) onAwarenessChanged.OnRaised -= SetAwareness;
            if (onMissionProgress != null) onMissionProgress.OnRaised -= SetProgress;
            if (onTranscriptShow != null) onTranscriptShow.OnRaised -= SetSubtitle;
        }

        private void SetAwareness(float v)
        {
            if (awarenessFill != null)
            {
                awarenessFill.fillAmount = v;
                awarenessFill.gameObject.SetActive(v >= 0.3f);
            }
        }

        private void SetProgress(float v)
        {
            if (progressLabel != null) progressLabel.text = $"Anomalies: {Mathf.RoundToInt(v * 100)}%";
        }

        public void SetInteractPrompt(string p)
        {
            if (interactPrompt == null) return;
            interactPrompt.gameObject.SetActive(!string.IsNullOrEmpty(p));
            interactPrompt.text = p ?? string.Empty;
        }

        private void SetSubtitle(string s)
        {
            if (subtitleLabel == null) return;
            subtitleLabel.text = s;
            subtitleLabel.gameObject.SetActive(!string.IsNullOrEmpty(s));
            CancelInvoke(nameof(ClearSubtitle));
            Invoke(nameof(ClearSubtitle), 6f);
        }
        private void ClearSubtitle() { if (subtitleLabel != null) subtitleLabel.text = string.Empty; }
    }
}
