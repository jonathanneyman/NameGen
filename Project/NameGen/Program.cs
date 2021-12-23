using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NameGen;

public class Program
{
    private static char[] _consonants = new[] { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z' };
    private static char[] _vowels = new[] { 'a', 'e', 'i', 'o', 'u', 'y' };
    
    private static Random _random = new Random();

    private static List<string> _suggestions = new List<string>();

    private static string GetConsonant()
    {
        return _consonants[_random.Next(0, _consonants.Length)].ToString();
    }

    private static string GetVowel()
    {
        return _vowels[_random.Next(0, _vowels.Length)].ToString();
    }

    private static string GenerateSyllable(bool cv)
    {
        return cv ? GetConsonant() + GetVowel() : GetVowel() + GetConsonant();
    }

    private static string GenerateName(int syllables)
    {
        var o = string.Empty;

        while (syllables > 0)
        {
            var cv = (_random.Next(0, 10) % 2) == 0;
            o += GenerateSyllable(cv);
            syllables--;
        }

        return o;
    }
        
    private static void Main(string[] args)
    {
        var syllables = 2;
        bool continious = false;
        bool pregen = false;
        bool debug = false;

        if (args.Length > 0)
            int.TryParse(args[0], out syllables);

        if (args.Contains("-c"))
            continious = true;

        if (args.Contains("-d"))
            debug = true;
        
        if (args.Contains("-pg"))
            pregen = true;

        var possibilities = (int)Math.Pow(_consonants.Length * _vowels.Length, syllables);

        Console.WriteLine();
        
        if (pregen)
            Console.WriteLine("Generating names with {0} possibilities", possibilities);
        else
            Console.WriteLine("Generating name with {0} possibilities", possibilities);

        Console.WriteLine();
        
        if (continious)
        {
            string suggestion = string.Empty;
            
            var sw = new Stopwatch();
            
            if (pregen)
            {
                var suggesstions = new string[possibilities];
            
                if (debug)
                    sw.Start();

                for (int i = 0; i < possibilities; i++)
                {
                    suggestion = GenerateName(syllables);
                
                    while (suggesstions.Contains(suggestion))
                        suggestion = GenerateName(syllables);
                
                    suggesstions[i] = suggestion;
                }

                if (debug)
                {
                    sw.Stop();
            
                    Console.WriteLine();
                    
                    Console.WriteLine("Generated {0} names in {1}ms", suggesstions.Length, sw.ElapsedMilliseconds);

                }

                Console.WriteLine();

                foreach (string s in suggesstions)
                {
                    Console.WriteLine(s);

                    string? r = Console.ReadLine();
                    
                    if (r == "q")
                        break;
                }
            }
            else
            {
                if (debug)
                    sw.Start();
                
                suggestion = GenerateName(syllables);
                _suggestions.Add(suggestion);
                
                if (debug)
                {
                    sw.Stop();
            
                    Console.WriteLine();

                    Console.WriteLine("Generated name in {0}ms", sw.ElapsedMilliseconds);
                        
                    Console.WriteLine();

                }
                
                Console.WriteLine(suggestion);
                
                
                while (Console.ReadLine() != "q")
                {
                    if (debug)
                        sw.Start();
                    
                    suggestion = GenerateName(syllables);
                    _suggestions.Add(suggestion);
                
                    while (_suggestions.Contains(suggestion))
                        suggestion = GenerateName(syllables);
                    
                    if (debug)
                    {
                        sw.Stop();
            
                        Console.WriteLine();

                        Console.WriteLine("Generated name in {0}ms", sw.ElapsedMilliseconds);
                        
                        Console.WriteLine();

                    }
                    Console.WriteLine(suggestion);
                }
            }
        }
        else
        {
            Console.WriteLine(GenerateName(syllables));
        }
    }
}
