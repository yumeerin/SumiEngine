using UnityEngine;

namespace Sumi
{
    public class DialogueUI:MonoBehaviour
    {
    [SerializeField] private TMPro.TextMeshProUGUI SpeakerText;
    [SerializeField] private TMPro.TextMeshProUGUI DialogueText;

    public void ShowDialogue(string speaker, string text)
    {
        SpeakerText.text = speaker;
        DialogueText.text = text;
    }
    }
}
