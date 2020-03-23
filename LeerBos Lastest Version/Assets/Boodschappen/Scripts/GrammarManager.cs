using System.Collections.Generic;
using UnityEngine;

public class GrammarManager : MonoBehaviour
{
    //Replaces the last letter of a word with either a d or a t
    private string ReplaceDT(string word)
    {
        string lastletter = word.Substring(word.Length - 1, 1);
        word = word.Substring(0, word.Length - 1);
        if (lastletter == "d")
        {
            return word + "t";
        }
        else //if (lastletter == "t")
        {
            return word + "d";
        }
    }

    //changes the first vowel into a double vowel and vice versa
    private string ReplaceDoubleLetters(string word)
    {
        for (int i = 0; i < word.Length; i++)
        {
            if (i != word.Length - 1)
            {
                string sub1 = word.Substring(i, 1);

                if (sub1 == "a" || sub1 == "i" || sub1 == "o" || sub1 == "e" || sub1 == "u") 
                {
                    string sub2 = word.Substring(i + 1, 1);

                    if (sub1 == sub2)
                    {
                        return word.Substring(0, i) + word.Substring(i + 1);
                    }
                    else
                    {
                        return word.Substring(0, i) + sub1 + word.Substring(i);
                    }
                }
            }
        }

        throw new System.NotImplementedException();
    }

    string GetGrammarMethod(int index, string word)
    {
        switch(index)
        {
            case 0:
                return ReplaceDT(word);
            case 1:
                return ReplaceDoubleLetters(word);
            default:
                throw new System.NotImplementedException();
        }
    }

    //Returns a list of 1 correct answer and and a number of wrong answers
    public List<string> MessUpGrammar(string word, int amount)
    {
        List<bool> grammar = Analyse(word);
        List<string> words = new List<string>();

        //Add the correct word
        words.Add(word);
        amount--;

        for (int i = 0; i < amount; i++)
        {
            //Check if there's any misspellings possible
            if (grammar.Contains(true))
            {
                int index = 0;
                bool active = true;

                //Select a random possible misspelling
                while (active)
                {
                    index = Random.Range(0, grammar.Count);
                    if (grammar[index] == true )
                    {
                        grammar[index] = false;
                        active = false;
                    }
                }

                words.Add(GetGrammarMethod(index, word));     
            }
            //If there's no misspelling possible, scramble the letters of the word
            else
            {
                words.Add(Scramble(word));
            }
        }

        return words;
    }

    //Analyses the word grammatically, and returns a list of which rules it can find
    public List<bool> Analyse(string word)
    {
        List<bool> grammar = new List<bool>();

        //Check if the word ends on d or t
        string lastletter = word.Substring(word.Length - 1, 1);
        if (lastletter == "d" || lastletter == "t")
        {
            grammar.Add(true);
        }
        else
        {
            grammar.Add(false);
        }

        bool doubleletters = false;
        for (int i = 0; i < word.Length; i++)
        {
            if (i != word.Length -1)
            {
                string sub1 = word.Substring(i, 1);
                if (sub1 == "a" || sub1 == "i" || sub1 == "o" || sub1 == "e" || sub1 == "u" || sub1 == "A" || sub1 == "I" || sub1 == "O" || sub1 == "E" || sub1 == "U")
                {
                    doubleletters = true;
                    break;
                }
            }
        }

        if (doubleletters == true)
        {
            grammar.Add(true);
        }
        else
        {
            grammar.Add(false);
        }

        return grammar;
    }

    //Scrambles the word if there's no grammar option available
    string Scramble(string word)
    {
        //Uncapitalizes the first letter
        word = word.ToLower();

        string scrambledword = "";
        for (int i = 0; i < word.Length; i++)
        {
            int rnd = Random.Range(0, word.Length - 1);

            scrambledword += word.Substring(rnd, 1);
        }

        //Capitalizes the first letter
        char[] a = scrambledword.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }
}