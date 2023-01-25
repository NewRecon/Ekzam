using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ekzam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionaries dictionaries = new Dictionaries();
            dictionaries.Run();
        }
    }
    class Dictionaries
    {
        List<MyDictionaryClass> DictionaryList = new List<MyDictionaryClass>();
        string _path;
        void ReadDirectory()
        {
            _path = _path.Trim('"', '\\');
            string[] allfiles = Directory.GetFiles($@"{_path}", "*.txt");
            for (int i=0;i<allfiles.Length;i++)
            {
                Console.WriteLine(allfiles[i]); 
                if (allfiles[i].Contains("-"))
                {
                    allfiles[i] = allfiles[i].Substring(0, allfiles[i].Length-4);
                    MyDictionaryClass md = new MyDictionaryClass(allfiles[i].Substring(allfiles[i].LastIndexOf(@"\")+1),
                        _path);
                    md.ReadWordInFile();
                    
                    DictionaryList.Add(md);
                }
            }
        }
        public void Run()
        {
            Console.WriteLine("\tСловари\n");
            Console.WriteLine("Введите рабочую директорию: ");
            _path = Console.ReadLine();
            ReadDirectory();
            ShowMainMenu();
        }
        void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("\tСловари\n");
            Console.WriteLine("1. Добавить");
            Console.WriteLine("2. Вывести");
            Console.WriteLine("3. Изменить");
            Console.WriteLine("4. Удалить");
            Console.WriteLine("0. Выход");
            Console.Write("\nВведите команду: ");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    ShowAddMenu();
                    break;
                case "2":
                    Console.Clear();
                    ShowShowMenu();
                    break;
                case "3":
                    Console.Clear();
                    ShowChangeMenu();
                    break;
                case "4":
                    Console.Clear();
                    ShowDelMenu();
                    break;
                case "0":
                    return;
            }
        }
        void ShowAddMenu()
        {
            Console.WriteLine("1. Добавить словарь");
            Console.WriteLine("2. Добавить слово в словарь");
            Console.WriteLine("3. Добавить перевод в словарь");
            Console.WriteLine("0. Назад в главное меню");
            Console.Write("\nВведите команду: ");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("\tДобавить словарь\n");
                    AddDictionary();
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("\tДобавить слово в словарь\n");
                    AddWord();
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("\tДобавить перевод в словарь\n");
                    AddTranslate();
                    break;
            }
            ShowMainMenu();
        }
        void ShowShowMenu()
        {
            Console.WriteLine("1. Вывести список словарей");
            Console.WriteLine("2. Открыть словарь");
            Console.WriteLine("3. Перевести слово");
            Console.WriteLine("0. Назад в главное меню");
            Console.Write("\nВведите команду: ");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("\tВывести список словарей\n");
                    ShowDictionaryList();
                    Console.ReadKey();
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("\tОткрыть словарь\n");
                    ShowDictionary();
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("\tПеревести слово\n");
                    Translate();
                    break;
            }
            ShowMainMenu();
        }
        void ShowChangeMenu()
        {

            Console.WriteLine("1. Изменить слово в словаре");
            Console.WriteLine("2. Изменить перевод в словаре");
            Console.WriteLine("0. Назад в главное меню");
            Console.Write("\nВведите команду: ");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("\tИзменить слово в словаре\n");
                    // РЕАЛИЗОВАТЬ
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("\tИзменить перевод в словаре\n");
                    // РЕАЛИЗОВАТЬ
                    break;
            }
            ShowMainMenu();
        }
        void ShowDelMenu()
        {
            Console.WriteLine("1. Удалить слово в словаре");
            Console.WriteLine("2. Удалить перевод в словаре");
            Console.WriteLine("0. Назад в главное меню");
            Console.Write("\nВведите команду: ");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("\tУдалить слово в словаре\n");
                    // РЕАЛИЗОВАТЬ
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("\tУдалить перевод в словаре\n");
                    // РЕАЛИЗОВАТЬ
                    break;
            }
            ShowMainMenu();
        }
        void AddDictionary()
        {
            Console.WriteLine("Введите тип словаря: ");
            string type = Console.ReadLine();
            type = type.Trim();
            try
            {
                foreach (var e in DictionaryList)
                    if (e._type == type) throw new Exception();
                Console.Clear();
                Console.WriteLine("Введите директорию, в которую будут сохраняться словари: ");
                string path = Console.ReadLine();
                path = path.Trim(new char[] { ' ', '"' });
                path = path.TrimEnd('\\');
                DictionaryList.Add(new MyDictionaryClass(type, $@"{path}"));
            }
            catch
            {
                Console.WriteLine($"{type} - данный словарь уже существует");
                Console.ReadKey();
            }
        }
        void ShowDictionaryList()
        {
            try
            {
                if (DictionaryList.Count == 0)
                    throw new Exception();
                for (int i = 0; i < DictionaryList.Count(); i++)
                {
                    Console.WriteLine(i + ". " + DictionaryList[i]._type);
                }
            }
            catch
            {
                Console.WriteLine("Список словарей пуст");
                Console.ReadKey();
            }
        }
        void AddWord()
        {
            ShowDictionaryList();
            Console.WriteLine("Введите номер словаря: ");
            string bufString = Console.ReadLine();
            int type = Int32.Parse(bufString);
            Console.Clear();
            Console.WriteLine("Введите слово: ");
            string word = Console.ReadLine();
            try
            {
                if (DictionaryList[type].myDictionary.ContainsKey(word)) throw new Exception();
                Console.Clear();
                Console.WriteLine(word);
                Console.WriteLine("Введите перевод: ");
                string translate = Console.ReadLine();
                DictionaryList[type].AddWord(word, translate);
            }
            catch
            {
                Console.Clear();
                Console.WriteLine($"{word} - данное слово уже добавлено в словарь");
                Console.ReadKey();
            }
}
        void AddTranslate() // позволяет добавлять слова уже существующие, если оно единственное
        {
            ShowDictionaryList();
            Console.WriteLine("Введите номер словаря: ");
            int type = Int32.Parse(Console.ReadLine());
            Console.Clear();
            List<string> tmpWordList = DictionaryList[type].GetWords();
            for (int i= 0;i < tmpWordList.Count; i++)
            {
                Console.WriteLine(i +". "+ tmpWordList[i]);
            }
            Console.WriteLine("Введите номер слова: ");
            int word = Int32.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine(tmpWordList[word]);
            Console.WriteLine("\nВведите перевод: ");
            string translate = Console.ReadLine();
            DictionaryList[type].AddTranslate(tmpWordList[word], translate);
        }
        void ShowDictionary()
        {
            ShowDictionaryList();
            Console.WriteLine("Введите номер словаря: ");
            int type = Int32.Parse(Console.ReadLine());
            Console.Clear();
            byte[] buf = File.ReadAllBytes(DictionaryList[type]._path);
            string bufString = Encoding.UTF8.GetString(buf);
            Console.WriteLine(bufString);
            Console.ReadKey();
        }
        void Translate()
        {
            ShowDictionaryList();
            Console.WriteLine("Введите номер словаря: ");
            int type = Int32.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Введите слово: ");
            string word = Console.ReadLine();
            DictionaryList[type].ShowTranslate(word);
            Console.WriteLine("Введите 1, чтобы сохранить перевод в отдельный файл");
            Console.WriteLine("Введите 0, чтобы выйти в главное меню");
            if (Console.ReadLine() == "1")
            {
                Console.Clear();
                Console.WriteLine("Введите директорию, в которую хотите сохранить перевод: ");
                string path = Console.ReadLine();
                path = path.Trim(new char[] { ' ', '"' });
                path = path.TrimEnd('\\');
                DictionaryList[type].ExportTranslateToFile(path, word);
                Console.Clear();
                Console.WriteLine("Перевод экспортирован");
                Console.ReadKey();
            }
        }
    }

    class MyDictionaryClass
    {
        char[] symbolForTrim = new char[] { ' ', '!', '?', '.', ',' };
        public string _type { get; }
        public string _path;
        public Dictionary<string, List<string>> myDictionary = new Dictionary<string, List<string>>();
        public MyDictionaryClass(string type, string path)
        {
            _type = type;
            _path = path + $"\\{type}.txt";
            using (FileStream fs = new FileStream(_path, FileMode.Append)) { }
        }
        public void ReadWordInFile()
        {
            byte[] buf = File.ReadAllBytes(_path);
            string bufString = Encoding.UTF8.GetString(buf);
            List<string> words = bufString.Split('\n').ToList();
            for (int i = 0; i < words.Count(); i++)
            {
                words[i] += "\n";
                Console.WriteLine(words[i].Substring(0, words[i].IndexOf('-') - 1));
                myDictionary.Add(words[i].Substring(0, words[i].IndexOf('-') - 1), words[i].Substring(words[i].IndexOf('-') + 2).Split(',').ToList());
            }
        }
        void SaveDictionaryToFile(string word)
        {
            using (FileStream fs = new FileStream(_path, FileMode.Append))
            {
                byte[] ba = Encoding.UTF8.GetBytes(ToString(word));
                fs.Write(ba, 0, ba.Length);
            }
        }
        void AddTranslateToFile(string word, string translate)
        {
            byte[] buf = File.ReadAllBytes(_path);
            string bufString = Encoding.UTF8.GetString(buf);
            int index = bufString.IndexOf(word);
            string bufSubString = bufString.Substring(0, index + word.Length + 2 );
            bufString = bufString.Substring(index + word.Length + 3 );
            bufString = bufSubString + " " + translate + ", " + bufString;
            using (FileStream fs = new FileStream(_path, FileMode.Create))
            {
                byte[] ba = Encoding.UTF8.GetBytes(bufString);
                fs.Write(ba, 0, ba.Length);
            }
        }
        void RemoveTranslateInFile(string translate) // тест
        {
            byte[] buf = File.ReadAllBytes(_path);
            string bufString = Encoding.UTF8.GetString(buf);
            int index = bufString.IndexOf(translate);
            string bufSubString = bufString.Substring(0, index-1);
            bufString = bufString.Substring(index + translate.Length + 1);
            using (FileStream fs = new FileStream(_path, FileMode.Create))
            {
                byte[] ba = Encoding.UTF8.GetBytes(bufString);
                fs.Write(ba, 0, ba.Length);
            }
        }
        void RemoveWordInFile()
        {
            // реализовать
        }
        string Normalize(string word)
        {
            word = word.Trim(symbolForTrim);
            word = word.ToLower();
            return word;
        }
        string ToString(string word)
        {
            string buf = word + " - ";
            foreach (var e in myDictionary[word])
                buf += e.ToString() + ", ";
            buf = buf.Trim(new char[] { ',', ' ' }) + ";\n";
            return buf;
        }
        public void AddWord(string word, string translate)
        {
            word = Normalize(word);
            translate = Normalize(translate);
            myDictionary.Add(word, new List<string> { translate });
            SaveDictionaryToFile(word);
            Console.Clear();
            Console.WriteLine("Слово добавлено");
            Console.ReadKey();
        }
        public void RemoveWord(string word)
        {
            word = Normalize(word);
            try
            {
                myDictionary[word].Clear();
                myDictionary.Remove(word);
            }
            catch
            {
                Console.Clear();
                Console.WriteLine($"{word} - данное слово отсутствует в словаре");
                Console.ReadKey();
            }
        }
        public void RemoveTranslate(string word, string translate)
        {
            word = Normalize(word);
            translate = Normalize(translate);
            try
            {
                if (myDictionary[word].Count == 1)
                    throw new Exception($"Нельзя удалить последний перевод для слова \"{word}\"");
                if (!myDictionary[word].Contains(translate))
                    throw new Exception($"{translate} - данный перевод отсутствует для слова \"{word}\"");
                myDictionary[word].Remove(translate);
                RemoveTranslateInFile(translate);
            }
            catch (KeyNotFoundException)
            {
                Console.Clear();
                Console.WriteLine($"{word} - данное слово отсутствует в словаре");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }
        public void AddTranslate(string word, string translate)
        {
            word = Normalize(word);
            translate = Normalize(translate);
            try
            {
                if (myDictionary[word].Contains(translate))
                    throw new Exception();
                myDictionary[word].Add(translate);
                AddTranslateToFile(word, translate);
                Console.Clear();
                Console.WriteLine("Перевод добавлен");
            }
            catch (KeyNotFoundException)
            {
                Console.Clear();
                Console.WriteLine($"{word} - данное слово отсутствует в словаре");
            }
            catch
            {
                Console.Clear();
                Console.WriteLine($"{translate} - данный перевод уже добавлен в словарь");
            }
            Console.ReadKey();
        }
        public void ChangeWord(string oldWord, string newWord)
        {
            oldWord = Normalize(oldWord);
            newWord = Normalize(newWord);
            var oldValue = myDictionary[oldWord];
            myDictionary.Remove(oldWord);
            myDictionary.Add(newWord, oldValue);
        }
        public void ChangeTranslate(string word, string oldTranslate, string newTranslate)
        {
            word = Normalize(word);
            oldTranslate = Normalize(oldTranslate);
            newTranslate = Normalize(newTranslate);
            for (int i = 0; i < myDictionary[word].Count; i++)
                if (myDictionary[word].Contains(oldTranslate))
                {
                    myDictionary[word][i] = newTranslate;
                    break;
                }
        }
        public void ExportTranslateToFile(string path, string word)
        {
            word = Normalize(word);
            try
            {
                if (!myDictionary.ContainsKey(word)) throw new Exception();
                path += $"\\{word}.txt";
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    byte[] ba = Encoding.UTF8.GetBytes(ToString(word));
                    fs.Write(ba, 0, ba.Length);
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine($"{word} - данное слово отсутствует в словаре");
                Console.ReadKey();
            }
        }
        public void ShowTranslate(string word)
        {
            word = Normalize(word);
            Console.Clear();
            try
            {
                foreach (var e in myDictionary[word])
                    Console.WriteLine(e);
            }
            catch
            {
                Console.WriteLine($"{word} - данное слово отсутствует в словаре");
                Console.ReadKey();
            }
        }
        public List<string> GetWords()
        {
            List<string> words = new List<string>();
            foreach (var e in myDictionary)
            {
                words.Add(e.Key);
            }
            return words;
        }
    }
}