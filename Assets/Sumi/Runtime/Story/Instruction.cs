using UnityEngine;

namespace Sumi
{
    public class Instruction
    {
        public InstructionType Type;
        public string Speaker;
        public string Text;
        public string Target;

        public Instruction(
            InstructionType type,
            string speaker =  null,
            string text = null,
            string target = null)
        {
            Type = type;
            Speaker = speaker;
            Text = text;
            Target = target;
        }
    }
}
