using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace Sumi
{
    public class VNRuntime : MonoBehaviour
    {
        [SerializeField] private TextAsset story;

        private StoryVM vm;
        private DialogueUI ui;
        private ChoiceUI choiceUI;

        private bool waitingForChoice;
        private bool choiceClickedThisFrame;

        private void Start()
        {
            SuiLexer lexer = new SuiLexer();

            List<SuiToken> tokens = lexer.Lex(story.text);

            SuiParser parser = new SuiParser(tokens);

            List<Instruction> instructions = parser.Parse();

            StoryProgram program = new StoryProgram(instructions);

            vm = new StoryVM(program);

            ui = FindFirstObjectByType<DialogueUI>();
            choiceUI = FindFirstObjectByType<ChoiceUI>();

            ShowNextInstruction();
        }

        private void Update()
        {
            // Prevent the same click that selected a choice
            // from also advancing the story.
            if (choiceClickedThisFrame)
            {
                choiceClickedThisFrame = false;
                return;
            }

            // Don't advance while choices are visible.
            if (waitingForChoice)
            {
                return;
            }

            // Normal dialogue advancement.
            if (Mouse.current != null &&
                Mouse.current.leftButton.wasPressedThisFrame)
            {
                ShowNextInstruction();
            }
        }

        private void ShowNextInstruction()
        {
            if (!vm.HasNext())
            {
                return;
            }

            Instruction instruction = vm.ExecuteNext();

            if (instruction == null)
            {
                return;
            }

            if (instruction.Type == InstructionType.Dialogue)
            {
                ui.ShowDialogue(
                    instruction.Speaker,
                    instruction.Text
                );
            }
            else if (instruction.Type == InstructionType.Choice)
            {
                List<Instruction> choices = vm.GetChoices();

                choices.Insert(0, instruction);

                waitingForChoice = true;

                choiceUI.ShowChoice(
                    choices,
                    Choose
                );
            }
        }

        private void Choose(string target)
        {
            // Block the mouse click from reaching the normal
            // story advancement code for this frame.
            choiceClickedThisFrame = true;

            vm.Choose(target);

            waitingForChoice = false;

            // Immediately show the selected branch.
            ShowNextInstruction();
        }
    }
}
