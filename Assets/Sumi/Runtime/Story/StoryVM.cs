using UnityEngine;

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

        public void ExecuteNext()
        {
            if (position >= program.Instructions.Count)
            {
              return;
            }
            Instruction instruction = program.Instructions[position];

            Debug.Log(instruction.Speaker + ": " + instruction.Text);

            position++;
        }
        public bool HasNext()
        {
            return position < program.Instructions.Count;
        }
    }
}
