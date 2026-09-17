using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace Sumi
{
    public class ChoiceUI : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private Button choiceButton;

        private System.Action<string> onChoiceSelected;

        public void ShowChoice(
            List<Instruction> choices,
            System.Action<string> onChoiceSelected)
        {
            this.onChoiceSelected = onChoiceSelected;

            foreach (Instruction instruction in choices)
            {
                Button button = Instantiate(
                    choiceButton,
                    container
                );

                button.gameObject.SetActive(true);

                TextMeshProUGUI text =
                button.GetComponentInChildren<TextMeshProUGUI>(true);

                text.text = instruction.Text;

                button.onClick.AddListener(() =>
                {
                    onChoiceSelected(instruction.Target);

                    foreach (Transform child in container)
                    {
                        if (child.gameObject != choiceButton.gameObject)
                        {
                            Destroy(child.gameObject);
                        }
                    }
                });
            }
        }
    }
}
