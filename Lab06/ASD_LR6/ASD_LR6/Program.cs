using System;
using static System.Console;

namespace ASD_LR6
{
    class Program
    {
        struct Queue
        {
            public int head;
            public int tail;
            public int size;
            public int[] Arr;

            public Queue(int head, int tail, int size,int[] Arr)
            {
                this.head = head;
                this.tail = tail;
                this.size = size;
                this.Arr = Arr;
            }

            public Queue InitQueue(int El, int S)
            {
                this.Arr = new int[S];
                this.Arr[0] = El;
                this.tail = 0;
                this.head = 0;
                this.size = 1;

                return this;

            }
            public void WT() => WriteLine(this.tail);
            
            public Queue enqueue(int El)
            {
                if (this.Arr[(this.tail + 1) % this.Arr.Length] == 0)
                {
                    this.tail = (this.tail + 1) % this.Arr.Length;
                    this.Arr[tail] = El;
                    this.size++;
                }
                else
                {
                    WriteLine("Черга переповнена\nЧерга збiльшена, продовжiть введення даних ");
                    IncreaseLength();
                    
                }                              
                return this;

            }
            public Queue dequeue()
            {
                this.Arr[head] = 0;
                this.head = (this.head + 1) % this.Arr.Length;

                this.size--;
                return this;
            }

            public bool isQueueEmpty()
            {
                if (this.size > 0)
                    return false;
                else
                    return true;                      
            }

            public void GetSize() => WriteLine(this.size);

            public void WriteQueue()
            {
                WriteLine("Поточна черга: ");
                foreach (int El in this.Arr)
                {
                    Write(El + " ");
                }
                WriteLine();
            }

            public void Head() => WriteLine(this.head);

            public Queue IncreaseLength()
            {
                int[] NewArr = new int[this.Arr.Length + 5];
                if (this.tail > this.head)
                {
                    for(int i = 0; i < this.Arr.Length; i++)
                    {
                        NewArr[i] = this.Arr[i];
                    }                    
                }
                else
                {
                    for(int i = 0; i <= tail; i++)
                    {
                        NewArr[i] = this.Arr[i];
                    }
                    for(int j = NewArr.Length -1; j-5 >= head; j--)
                    {
                        NewArr[j] = this.Arr[j - 5];
                    }
                    this.head = this.head + 5;
                }

                this.Arr = NewArr;
                return this;
            }
        }
        static void Main(string[] args)
        {
            Queue Q = new Queue();
            WriteLine(" 1- контрольний приклад, інше - ручний режим");
            int choise = Convert.ToInt32(ReadLine());
            if (choise == 1)
            {
                Q = Q.InitQueue(1, 5);
                Q.WriteQueue();
                WriteLine("4");
                Q.enqueue(4);
                Q.WriteQueue();
                WriteLine("6");
                Q.enqueue(6);
                Q.WriteQueue();
                WriteLine("2");
                Q.enqueue(2);
                Q.WriteQueue();
                WriteLine("0");
                for (int i = 0; i < 3; i++)
                {
                    Q.dequeue();
                    Q.WriteQueue();
                }
                WriteLine("3");
                Q.enqueue(3);
                Q.WriteQueue();
                WriteLine("7");
                Q.enqueue(7);
                Q.WriteQueue();
                WriteLine("5");
                Q.enqueue(5);
                Q.WriteQueue();
                WriteLine("3");
                Q.enqueue(3);
                Q.WriteQueue();
                WriteLine("1");
                Q.enqueue(1);
                Q.WriteQueue();
                WriteLine("4");
                Q.enqueue(4);
                Q.WriteQueue();
                WriteLine("2");
                Q.enqueue(2);
                Q.WriteQueue();
                WriteLine("0");
                for (int i = 0; i < 3; i++)
                {
                    Q.dequeue();
                    Q.WriteQueue();
                }
                WriteLine("0");
                for (int i = 0; i < 3; i++)
                {
                    Q.dequeue();
                    Q.WriteQueue();
                }
                WriteLine("0");
                for (int i = 0; i < 3; i++)
                {
                    Q.dequeue();
                    Q.WriteQueue();
                }



            }
            else
            {
                WriteLine("Введiть перший елемент");
                char inputC = Convert.ToChar(ReadLine());
                if (Char.IsDigit(inputC))
                {
                    Q = Q.InitQueue(inputC  - 48, 5);
                    Q.WriteQueue();
                    while (Q.size > 0)
                    {
                        inputC = Convert.ToChar(ReadLine());
                        if (Char.IsDigit(inputC))
                        {
                            int input = Convert.ToInt32(inputC - 48);

                            if (input != 0)
                            {
                                Q.enqueue(input);
                                Q.WriteQueue();
                            }
                            else
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    Q.dequeue();
                                    Q.WriteQueue();
                                }
                            }
                        }
                        else
                            WriteLine("Некоректне значення");
                    }
                }
                else
                    WriteLine("Некоректне значення");
            }

        }
    }
}
