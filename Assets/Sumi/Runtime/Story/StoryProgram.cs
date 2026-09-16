using UnityEngine;

using System.Collections.Generic;

namespace Sumi
{
    public class StoryProgram
    {
        private readonly List<Instruction> instructions = new();
        public IReadOnlyList<Instruction> Instructions => instructions;
        public void Add(Instruction instruction)
        {
            instructions.Add(instruction);
        }
        public void AddDialogue(string speaker, string text)
        {
            instructions.Add(
                new Instruction(
                    InstructionType.Dialogue,
                    speaker,
                    text
                )
            );
        }
    }
}
