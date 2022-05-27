
using System;                // Console
using System.IO;             // FileStream, FileReader

class FileDetails
{
    static void Summarize(char[] contents)
    {
        int vowels = 0;
        int consonants = 0;
        int lines = 0;
        foreach (char current in contents)
        {
            if (Char.IsLetter(current))
            {
                if ("AEIOUaeiou".IndexOf(current) != -1)
                    vowels++;
                else
                    consonants++;
            }
            else if (current == '\n')
            {
                lines++;
            }
        }
        Console.WriteLine("Total no of characters: {0}", contents.Length);
        Console.WriteLine("Total no of vowels: {0}", vowels);
        Console.WriteLine("Total no of consonants: {0}", consonants);
        Console.WriteLine("Total no of lines: {0}", lines);
    }
    static void Main(string[] args)
    {
        string fileName = args[0];
        FileStream stream = new FileStream(fileName, FileMode.Open);
        StreamReader reader = new StreamReader(stream);
        //    //reader.ReadToEnd();
        //    -reader = new StreamReader(stream);
        //    //StreamReader reader = args[0];
        int size = (int)stream.Length;
        char[] contents = new char[size];
        for (int i = 0; i < size; i++)
        {
            contents[i] = (char)reader.Read();
        }
        reader.Close();
        Summarize(contents);
    }
}

    //foreach (char ch in contents)
    //{
    //    Console.Write(ch);
    //}

//Console.WriteLine(args.Length);
//foreach(string arg in args)
//{
//Console.WriteLine(arg);
//}