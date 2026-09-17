using UnityEngine;
using System.Collections.Generic;

namespace Sumi
{
    public class StoryVM
    {
        private StoryProgram program;
        private int position = 0;

        public StoryVM(StoryProgram program)
        {
            this.program = program;
        }

        public Instruction ExecuteNext()
        {
            while (position < program.Instructions.Count)
            {
                Instruction instruction = program.Instructions[position];

                position++;

                if (instruction.Type == InstructionType.Label)
                {
                    continue;
                }

                if (instruction.Type == InstructionType.Jump)
                {
                    JumpToLabel(instruction.Target);
                    continue;
                }

                if (instruction.Type == InstructionType.Choice)
                {
                    return instruction;
                }

                return instruction;
            }

            return null;
        }

        public List<Instruction> GetChoices()
        {
            List<Instruction> choices = new();

            while (position < program.Instructions.Count)
            {
                Instruction instruction = program.Instructions[position];

                if (instruction.Type != InstructionType.Choice)
                {
                    break;
                }

                choices.Add(instruction);
                position++;
            }

            return choices;
        }

        public void Choose(string target)
        {
            JumpToLabel(target);
        }

        private void JumpToLabel(string name)
        {
            for (int i = 0; i < program.Instructions.Count; i++)
            {
                if (program.Instructions[i].Type == InstructionType.Label &&
                    program.Instructions[i].Target == name)
                {
                    position = i + 1;
                    return;
                }
            }

            Debug.LogError("Label not found: " + name);
        }

        public Instruction GetCurrentInstruction()
        {
            if (position >= program.Instructions.Count)
            {
                return null;
            }

            return program.Instructions[position];
        }

        public bool HasNext()
        {
            return position < program.Instructions.Count;
        }
    }
}
