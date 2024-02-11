using System;
using System.IO;
using System.Linq;

namespace Digital_Design_Test_Task
{
    public class Program
    {
        public static void Main (string [] arg)
        {
            string[] words;
            string wordTemp;
            char[] stopsimvol = {' ', '-', '*', ',', '.', '[', ']', '!', '?', '(', ')',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '"', ':', ';'};
            Dictionary<string, int> dict_words = new Dictionary<string, int>();
            int value;
           
            // читаем файл полностью в виде массива строк
            string [] lines = File.ReadAllLines("voyna-i-mir-tom-1.txt");
            
            // обработка каждой строки
            foreach(string ln in lines)
            {
                words = ln.Split(' ');
                
                // обработка каждого текстового элемента, полученного после 
                // разбиения строки с разделителем пробела 
                foreach(string word in words)
                {
                    wordTemp = word.Trim(stopsimvol).ToLower();
                    if (!(wordTemp == ""))
                    {
                        if (dict_words.ContainsKey(wordTemp))
                        {
                            value = dict_words[wordTemp];
                            dict_words[wordTemp] = value + 1;
                        } 
                        else
                        {
                            dict_words.Add(wordTemp, 1);
                        }
                    }                    
                }
            }
                        
            // сортировка списка (пузырьковая, оптимальные способы сортировки 
            // здесь не рассматривал; также не рассматривал встроенные 
            // функции сортировки списков)
            List<KeyValuePair<string, int>> list_words = dict_words.ToList();
            KeyValuePair<string, int> temp;
            for(int i = 0; i < list_words.Count - 1; i++)
            {
                for (int j = i + 1; j < list_words.Count; j++)
                {
                    if (list_words[i].Value < list_words[j].Value)
                    {
                        temp = list_words[i];
                        list_words[i] = list_words[j];
                        list_words[j] = temp;
                    }
                }
            }
            
            // преобразование отсортированного списка пар в массив строк для 
            // дальнейшей записи в файл
            string[] array_out = new string[list_words.Count];
            for (int i = 0; i < list_words.Count; i++)
            {
                array_out[i] = $"{list_words[i].Key}  {list_words[i].Value}";                
            }              
            
            // отсортированный список переносим в текстовый файл
            System.IO.File.WriteAllLines("voyna-i-mir_out.txt", array_out);
        }  
    }
} 
