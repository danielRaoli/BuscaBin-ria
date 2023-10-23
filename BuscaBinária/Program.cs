using System;
using System.Net;

namespace BuscaBinária
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = new int[] { 10, 5, 8, 12, 25, 6, 9 };


            var orderNumbers = SelectionSort(numbers);
            foreach (var number in orderNumbers)
            {
                Console.WriteLine(number);
            }

            int valueSearch = BinarySearch(orderNumbers, 12);

            if (valueSearch != 1)
            {
                Console.WriteLine("O valor foi encontrado");
            }
            else
            {
                Console.WriteLine("O valor não está contido na lista");
            }

            string htmlStringNumbers = String.Join("-",orderNumbers);
            string prefix = "http://localhost:8080/";

            using (HttpListener listener = new HttpListener())
            {
                listener.Prefixes.Add(prefix);
                listener.Start();
                Console.WriteLine($"Servidor web iniciado em {prefix}");

                while (true)
                {
                    HttpListenerContext context = listener.GetContext();


                    HttpListenerResponse response = context.Response;
                    string resposta = $"<html><body>{htmlStringNumbers}</body></html>";
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(resposta);

                    response.ContentLength64 = buffer.Length;
                    Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    output.Close();
                }


            }
        }


        public static int BinarySearch(int[] numbers, int number)
        {
            int finalPos = numbers.Length - 1;
            int initialPos = 0;
            while (initialPos <= finalPos)
            {

                int midValue = (initialPos + finalPos) / 2;
                if (numbers[midValue] == number)
                {
                    return number;
                }
                else if (numbers[midValue] < number)
                {
                    initialPos = midValue + 1;
                }
                else
                {
                    finalPos = midValue - 1;
                }
            }

            return -1;
        }

        public static int[] SelectionSort(int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    int min = i;
                    if (numbers[j] < numbers[min])
                    {
                        min = j;
                    }
                    int x = numbers[min];
                    numbers[min] = numbers[i];
                    numbers[i] = x;
                }
            }
            return numbers;
        }
    }
}
