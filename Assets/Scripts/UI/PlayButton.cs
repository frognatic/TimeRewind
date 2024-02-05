using TimeReverse;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayButton : MonoBehaviour
    {
        [Header("Button settings")]
        [SerializeField] private Image iconImage;
        
        [Header("Sprites to change")]
        [SerializeField] private Sprite playSprite;
        [SerializeField] private Sprite pauseSprite;

        private void OnEnable()
        {
            TimeReverseController.OnPlayRewind += SetPauseSprite;
            TimeReverseController.OnPauseRewind += SetPlaySprite;
        }

        private void OnDisable()
        {
            TimeReverseController.OnPlayRewind -= SetPauseSprite;
            TimeReverseController.OnPauseRewind -= SetPlaySprite;
        }

        private void SetPlaySprite() => SetSprite(playSprite);
        private void SetPauseSprite() => SetSprite(pauseSprite);
        private void SetSprite(Sprite spriteToSet) => iconImage.sprite = spriteToSet;
    }
}
