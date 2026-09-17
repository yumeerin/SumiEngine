using UnityEngine;
using System.Collections.Generic;

namespace Sumi
{
    public class SuiLexer
    {
        private string source;
        private int position;
        private List<SuiToken> tokens = new();

        public List<SuiToken> Lex(string source)
        {
            this.source = source;
            position = 0;
            tokens.Clear();

            while (!IsAtEnd())
            {
                SkipWhitespace();

                if (IsAtEnd())
                {
                    break;
                }

                if (CurrentCharacter() == '\n')
                {
                    tokens.Add(new SuiToken(
                        SuiTokenType.NewLine,
                        "\n"
                    ));

                    Advance();
                    continue;
                }

                if (CurrentCharacter() == '"')
                {
                    string text = ReadString();

                    tokens.Add(new SuiToken(
                        SuiTokenType.String,
                        text
                    ));

                    continue;
                }

                string word = ReadWord();

                if (word.Length > 0)
                {
                    tokens.Add(new SuiToken(
                        SuiTokenType.Word,
                        word
                    ));
                }

                if (!IsAtEnd() && CurrentCharacter() == ':')
                {
                    tokens.Add(new SuiToken(
                        SuiTokenType.Colon,
                        ":"
                    ));

                    Advance();
                }
                if (!IsAtEnd() &&
                    CurrentCharacter() == '-'
                && position + 1 < source.Length
                && source[position +1] == '>')
                {
                    tokens.Add(new SuiToken(
                        SuiTokenType.Arrow,
                        "->"
                    ));

                    Advance();
                    Advance();
                }
            }

            return tokens;
        }

        private char CurrentCharacter()
        {
            return source[position];
        }

        private void Advance()
        {
            position++;
        }

        private bool IsAtEnd()
        {
            return position >= source.Length;
        }

        private string ReadWord()
        {
            string word = "";

            while (!IsAtEnd() &&
                CurrentCharacter() != ' ' &&
                CurrentCharacter() != '\n' &&
                CurrentCharacter() != ':' &&
                CurrentCharacter() != '-')
            {
                word += CurrentCharacter();
                Advance();
            }

            return word;
        }

        private void SkipWhitespace()
        {
            while (!IsAtEnd() &&
                CurrentCharacter() == ' ')
            {
                Advance();
            }
        }

        private string ReadString()
        {
            Advance();

            string text = "";

            while (!IsAtEnd() &&
                CurrentCharacter() != '"')
            {
                text += CurrentCharacter();
                Advance();
            }

            if (!IsAtEnd())
            {
                Advance();
            }

            return text;
        }
    }
}
