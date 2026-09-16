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

        private void Start()
        {
            SuiLexer lexer = new SuiLexer();

            List<SuiToken> tokens = lexer.Lex(story.text);

            SuiParser parser = new SuiParser(tokens);

            List<Instruction> instructions = parser.Parse();

            StoryProgram program = new StoryProgram(instructions);

            vm = new StoryVM(program);

            ui = FindFirstObjectByType<DialogueUI>();

            Instruction instruction = vm.ExecuteNext();

            ui.ShowDialogue(
                instruction.Speaker,
                instruction.Text
            );
        }

        private void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (vm.HasNext())
                {
                    Instruction instruction = vm.ExecuteNext();

                    ui.ShowDialogue(
                        instruction.Speaker,
                        instruction.Text
                    );
                }
            }
        }
    }
}
