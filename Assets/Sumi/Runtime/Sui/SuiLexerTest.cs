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

            Debug.Log("First instruction:");
            Debug.Log(instructions[0].Type);
            Debug.Log(instructions[0].Target);

            Debug.Log("Second instruction:");
            Debug.Log(instructions[1].Type);
            Debug.Log(instructions[1].Speaker);
            Debug.Log(instructions[1].Text);

            Debug.Log("Third instruction:");
            Debug.Log(instructions[2].Type);
            Debug.Log(instructions[2].Target);
        }
    }
}
