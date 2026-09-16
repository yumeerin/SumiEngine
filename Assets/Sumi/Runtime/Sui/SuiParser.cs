using UnityEngine;
using System.Collections.Generic;

namespace Sumi
{
    public class SuiParser
    {
        private List<SuiToken> tokens;
        private int position;

        public SuiParser(List<SuiToken> tokens)
        {
            this.tokens = tokens;
            position = 0;
        }

        private SuiToken CurrentToken()
        {
            return tokens[position];
        }

        private void Advance()
        {
            position++;
        }

        private bool HasMoreTokens()
        {
            return position < tokens.Count;
        }

        private SuiToken Expect(SuiTokenType type)
        {
            SuiToken token = CurrentToken();

            if (token.Type != type)
            {
                Debug.LogError("Unexpected token: " + token.Value);
                return null;
            }

            Advance();

            return token;
        }

        private Instruction ParseDialogue()
        {
            SuiToken speaker = Expect(SuiTokenType.Word);
            SuiToken colon = Expect(SuiTokenType.Colon);
            SuiToken newLine = Expect(SuiTokenType.NewLine);
            SuiToken text = Expect(SuiTokenType.String);

            return new Instruction(
                InstructionType.Dialogue,
                speaker.Value,
                text.Value
            );
        }

        private Instruction ParseLabel()
        {
            SuiToken name = Expect(SuiTokenType.Word);

            return new Instruction(
                InstructionType.Label,
                target: name.Value
            );
        }
        private Instruction ParseJump()
        {
            SuiToken name = Expect(SuiTokenType.Word);

            return new Instruction(
                InstructionType.Jump,
                target: name.Value
            );
        }

        public List<Instruction> Parse()
        {
            List<Instruction> instructions = new();

            while (HasMoreTokens())
            {
                SuiToken token = CurrentToken();

                if (token.Type == SuiTokenType.Word && token.Value == "say")
                {
                    Advance();

                    Instruction instruction = ParseDialogue();
                    instructions.Add(instruction);
                }
                else if (token.Type == SuiTokenType.Word && token.Value == "label")
                {
                    Advance();

                    Instruction instruction = ParseLabel();
                    instructions.Add(instruction);
                }
                else if (token.Type == SuiTokenType.Word && token.Value == "jump")
                {
                    Advance();

                    Instruction instruction = ParseJump();
                    instructions.Add(instruction);
                }
                else
                {
                    Advance();
                }
            }

            return instructions;
        }
    }
}
