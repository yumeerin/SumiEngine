using UnityEngine;
using System.Collections.Generic;

namespace Sumi
{
    public class SuiLexerTest : MonoBehaviour
    {
        [SerializeField] private TextAsset story;

        private void Start()
        {
            SuiLexer lexer = new SuiLexer();

            string source = story.text;

            List<SuiToken> tokens = lexer.Lex(source);

            foreach (SuiToken token in tokens)
            {
                Debug.Log(token.Type + " : " + token.Value);
            }

            SuiParser parser = new SuiParser(tokens);

            List<Instruction> instructions = parser.Parse();

            Debug.Log("Instruction count: " + instructions.Count);

            foreach (Instruction instruction in instructions)
            {
                Debug.Log(
                    instruction.Type +
                    " | " +
                    instruction.Text +
                    " | " +
                    instruction.Target
                );
            }
        }
    }
}
