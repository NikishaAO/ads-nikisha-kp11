 using System;

namespace ASDl7
{
    class Program
    {
        struct Key
        {
            string firstName;
            string lastName;

            public Key(string firstName, string lastName)
            {
                this.firstName = firstName;
                this.lastName = lastName;
            }

            public string getFN() => this.firstName;
            public string getLN() => this.lastName;
        }
        
        struct Value
        {
            int patientID;
            string familyDoctor;
            string address;

            public Value(int patientID, string familyDoctor, string address)
            {
                this.patientID = patientID;
                this.familyDoctor = familyDoctor;
                this.address = address;
            }

            public int getID => this.patientID;
            public string getDoctor => this.familyDoctor;
            public string getAddress => this.address;
        }
        struct Entry
        {
            Key key;
            Value Value;
            bool isRemoved;

            public Entry(Key key, Value Value, bool isRemoved)
            {
                this.key = key;
                this.Value = Value;
                this.isRemoved = isRemoved;
            }

            public bool isRem() => this.isRemoved;
            public Key getKey() => this.key;
            public Value GetValue() => this.Value;

        }
        class Hashtable
        {
            Entry[] table;
            float loadness;
            int size;

            public Hashtable() { }

            public Hashtable(Entry[] table, int loadness, int size)
            {
                this.table = table;
                this.loadness = loadness;
                this.size = size;
            }

            public Hashtable initHashtable()
            {
                this.size = 0;
                this.loadness = 0;
                this.table = new Entry[10007];

                return this;
            }

            public long hashCode(Key key)
            {
                long Hash = 0;
                int n = (key.getFN().Length + key.getLN().Length);
                foreach (char Ch in key.getFN())
                {
                    n--;
                    Hash += (Convert.ToInt64(Ch) - 64) * Convert.ToInt64(Math.Pow(28, n));
                }
                foreach (char Ch in key.getLN())
                {
                    n--;
                    Hash += (Convert.ToInt64(Ch) - 64) * Convert.ToInt64(Math.Pow(28, n));
                }

                Hash = Hash % this.table.Length;
                return Hash;
            }
            public Hashtable insertEntry(Key key, Value value)
            {
                int c = 1;
                if (this.table[this.hashCode(key)].isRem())
                {
                    this.table[this.hashCode(key)] = new Entry(key, value, false);
                    this.size++;
                    this.loadness = size / this.table.Length;
                }
                else
                {
                    while (this.table[this.hashCode(key) + c].isRem())
                    {
                        c *= c;
                    }
                    this.table[this.hashCode(key) + c] = new Entry(key, value, false);
                    this.size++;
                    this.loadness = size / this.table.Length;

                }


                return this;
            }
            public Hashtable insertEntry(Key key, Value value, Hashtable2 HT2)
            {
                int c = 1;
            
               if (this.table[this.hashCode(key)].isRem())
                {
                    this.table[this.hashCode(key)] = new Entry(key, value, false);
                    this.size++;
                    this.loadness = size / this.table.Length;
                }
                else
                {
                    while (this.table[this.hashCode(key) + c].isRem())
                    {
                        c *= c;
                    }
                    this.table[this.hashCode(key) + c] = new Entry(key, value, false);
                    this.size++;
                    this.loadness = size / this.table.Length;

                }

                if (this.loadness > 0.5)
                    this.rehashing();
                HT2.insertEntry(new Entry2(value.getDoctor, key));

                return this;
            }

            public Hashtable removeEntry(Key key, Hashtable2 HT2)
            {
                int c = 1;
                if (this.table[this.hashCode(key)].getKey().getFN() == key.getFN() && this.table[this.hashCode(key)].getKey().getLN() == key.getLN())
                {
                    this.table[this.hashCode(key)] = new Entry(key, this.table[this.hashCode(key)].GetValue(), true);
                    HT2.removeEntry(new Entry2(this.table[this.hashCode(key)].GetValue().getDoctor, key));
                }
                else
                {
                    while (this.table[this.hashCode(key) + c].getKey().getFN() != key.getFN() && this.table[this.hashCode(key) + c].getKey().getLN() != key.getLN())
                    {
                        c *= c;
                    }
                    this.table[this.hashCode(key) + c] = new Entry(key, this.table[this.hashCode(key) + c].GetValue(), true);
                    HT2.removeEntry(new Entry2(this.table[this.hashCode(key) + c].GetValue().getDoctor, key));
                }

                return this;
            }

            public void findEntry(Key key)
            {
                int c = 1;

                if (this.table[this.hashCode(key)].getKey().getFN() == key.getFN() && this.table[this.hashCode(key)].getKey().getLN() == key.getLN())
                {
                    Console.WriteLine();
                    Console.Write(":" + this.table[this.hashCode(key)].GetValue().getID + " " + this.table[this.hashCode(key)].GetValue().getDoctor + " " + this.table[this.hashCode(key) + c].GetValue().getAddress);
                    if (this.table[this.hashCode(key)].isRem())
                        Console.Write("(is Removed)");
                }
                else
                {
                    while (this.table[this.hashCode(key) + c].getKey().getFN() != key.getFN() && this.table[this.hashCode(key) + c].getKey().getLN() != key.getLN() && this.table[this.hashCode(key)].isRem())
                    {
                        c *= c;
                    }
                    Console.WriteLine();
                    Console.Write(":" + this.table[this.hashCode(key) + c].GetValue().getID + " " + this.table[this.hashCode(key) + c].GetValue().getDoctor + " " + this.table[this.hashCode(key) + c].GetValue().getAddress);
                    if (this.table[this.hashCode(key) + c].isRem())
                        Console.Write("(is Removed)");
                }

            }

            public Hashtable rehashing()
            {
                Entry[] NewTable = new Entry[this.table.Length * 2];
                Hashtable NewHashtable = new Hashtable(NewTable, 0, 0);

                foreach (Entry Element in this.table)
                {
                    if (!Element.isRem())
                        NewHashtable.insertEntry(Element.getKey(), Element.GetValue());
                }

                return NewHashtable;
            }

        }
        struct Entry2
        {
            public string key;
            public Key Value;
            public Entry2(string key,Key value) 
            {
                this.key = key;
                this.Value = value;
            }
        }
        class Node
        {
            public Entry2 data;
            public Node next;
            public Node(Entry2 data)
            {
                this.data = data;
            }
            public Node(Entry2 data, Node next)
            {
                this.data = data;
                this.next = next;
            }
        }
        class LList
        {
            public Node head;
            public int size;
            public LList(Entry2 data)
            {
                head = new Node(data);
                size = 0;
            }
            public void AddLast(Entry2 data)
            {
                Node current = head;
                while (current.next != null)
                {
                    current = current.next;
                }
                Node NewNode = new Node(data);
                current.next = NewNode;
            }
            public void DeleteFromPosition(int position)
            {
                Node current = head;
                for (int i = 0; i < position - 2; i++)
                {
                    current = current.next;
                }
                current.next = current.next.next;
            }
            public void DeleteFirst()
            {

                Node NewHead = head.next;
                Node current = head;

                while (current.next != null) 
                {
                    current = current.next;
                }
               
                head = NewHead;
            }
            public void DeleteLast()
            {
                Node current = head;
                while (current.next.next != null)
                {
                    current = current.next;
                }
                current.next = null;
            }
        }
        class Hashtable2
        {
            public LList[] List;
            public Hashtable2 initHashtable()
            {
                this.List = new LList[10007];

                return this;
            }
            public long hashCode(string key)
            {
                string Doc = key;
                long Hash = 0;
                int n = key.Length;
                foreach (char Ch in key)
                {
                    n--;
                    Hash += (Convert.ToInt64(Ch) - 64) * Convert.ToInt64(Math.Pow(28, n));
                }


                Hash = Hash % this.List.Length;
                return Hash;
            }

            public Hashtable2 insertEntry(Entry2 Entry)
            {
                if (this.List[this.hashCode(Entry.key)] != null && this.List[this.hashCode(Entry.key)].size >= 5)
                {
                    Console.WriteLine("Вичерпано лiмiт цього лiкаря");
                    for (int i = 0; i < this.List.Length; i++)
                    {
                        if (List[i] != null && List[i].size < 5)
                        {
                            Console.WriteLine("Вiльний лiкар - {0}", List[i].head.data.key);
                            break;
                        }
                    }
                    Console.WriteLine("Введiть iм'я знайденого лiкаря або нове");
                }
                else
                if (this.List[this.hashCode(Entry.key)] == null)
                {
                    this.List[this.hashCode(Entry.key)] = new LList(Entry);
                    this.List[this.hashCode(Entry.key)].size++;

                }
                else
                {
                    this.List[this.hashCode(Entry.key)].AddLast(Entry);
                    this.List[this.hashCode(Entry.key)].size++;
                }

                return this;
            }
            public Hashtable2 removeEntry(Entry2 entry)
            {
                Node current = this.List[this.hashCode(entry.key)].head;
                for (int i = 0; i < this.List[this.hashCode(entry.key)].size; i++)
                {
                    if ((current.data.Value.getFN() == entry.Value.getFN() && current.data.Value.getLN() == entry.Value.getLN()) || current.next == null)
                    {
                        if (i == 0)
                        {
                        this.List[this.hashCode(entry.key)].DeleteFirst();
                        this.List[this.hashCode(entry.key)].size--;
                        }
                        else
                        {
                            this.List[this.hashCode(entry.key)].DeleteFromPosition(i );
                            this.List[this.hashCode(entry.key)].size--;
                        }

                        break;
                    }

                    current = current.next;
                }
                return this;
            }
            public void findFamilyDoctorPatients(string key)
            {
                if(this.List[this.hashCode(key)].head.data.key == key)
                {
                    Node current = this.List[this.hashCode(key)].head;

                    do
                    {
                        Console.Write("{0} {1}\t", current.data.Value.getFN(), current.data.Value.getLN());
                        current = current.next;

                    }
                    while (current != null);

                    Console.WriteLine();
                }
            }
        }
        
        static void Main(string[] args)
        {
            int ID = 41800;

            Hashtable HT = new Hashtable();
             HT.initHashtable();

            Hashtable2 HT2 = new Hashtable2();
            HT2.initHashtable();
            Console.WriteLine("1 - контрольний приклад, 2 - додати елемент, 3 - видалити елемент, 4 - знайти всiх пацiєнтів лiкаря, 0 - вихiд");

            int insert = Convert.ToInt32(Console.ReadLine());

            while(insert != 0)
            {
                if(insert == 1)
                {
                    Key key = new Key("Andrii", "Nikisha");
                    Key key2 = new Key("Marho", "Balandiuk");
                    Key key3 = new Key("Vlad", "Nikitin");
                    Key key4 = new Key("Vlad", "Tylik");
                    Key key5 = new Key("Maria", "Batseva");
                    Key key6 = new Key("Diana", "Buriak");
                    Key key7 = new Key("Kurt", "Cobain");

                    Console.WriteLine("\n" + key.getFN() + " " + key.getLN() + " Loev");
                    HT.insertEntry(key, new Value(ID, "Loev", "Dunaiska 47"), HT2);
                    HT.findEntry(key);
                    Console.WriteLine();
                    ID++;
                    Console.WriteLine("\n" + key2.getFN() + " " + key2.getLN() + " Loev");
                    HT.insertEntry(key2, new Value(ID, "Loev", "Shevchenka 22"), HT2);
                    HT.findEntry(key2);
                    Console.WriteLine();
                    ID++;
                    Console.WriteLine("\n" + key3.getFN() + " " + key3.getLN() + " Loev");
                    HT.insertEntry(key3, new Value(ID, "Loev", "Blidogo 32"), HT2);
                    HT.findEntry(key3);
                    Console.WriteLine();
                    ID++;
                    Console.WriteLine("\n" + key4.getFN() + " " + key4.getLN() + " Loev");
                    HT.insertEntry(key4, new Value(ID, "Loev", "Lennona 1"), HT2);
                    HT.findEntry(key4);

                    Console.WriteLine();
                    ID++;
                    Console.WriteLine("\n" + key5.getFN() + " " + key5.getLN() + " Loev");
                    HT.insertEntry(key5, new Value(ID, "Loev", "Zelena 12"), HT2);
                    HT.findEntry(key5);
                    Console.WriteLine();
                    ID++;
                    Console.WriteLine("\n" + key7.getFN() + " " + key7.getLN() + " Kozak");
                    HT.insertEntry(key7, new Value(ID, "Kozak", "Zatyshna 21"), HT2);
                    HT.findEntry(key7);
                    Console.WriteLine();
                    ID++;
                    Console.WriteLine("\n" + key6.getFN() + " " + key6.getLN() + " Loev");

                    HT.insertEntry(key6, new Value(ID, "Loev", "Velyka 2"), HT2);
                    ID++;
                    Console.WriteLine();
                    ID++;
                    Console.WriteLine("\n" + key6.getFN() + " " + key6.getLN() + " Kozak");
                    HT.insertEntry(key6, new Value(ID, "Kozak", "Velyka 2"), HT2);
                    HT.findEntry(key6);
                    Console.WriteLine();
                    ID++;

                    Console.WriteLine("\n Remove:" + key.getFN() + " " + key.getLN());
                    HT.removeEntry(key,HT2);
                    HT.findEntry(key);
                    Console.WriteLine();

                    Console.WriteLine("Find all patients Loev");
                    HT2.findFamilyDoctorPatients("Loev");
                    Console.WriteLine("\n Remove:" + key3.getFN() + " " + key3.getLN());

                    HT.removeEntry(key3, HT2);
                    HT.findEntry(key3);
                    Console.WriteLine();
                    Console.WriteLine("Find all patients Loev");

                    HT2.findFamilyDoctorPatients("Loev");
                    insert = 0;
                }
                if (insert == 2)
                {
                    Console.WriteLine("Введiть iм'я пацiєнта");
                    string name = Console.ReadLine();
                    Console.WriteLine("Введiть прiзвище пацiєнта");
                    string surname = Console.ReadLine();
                    Console.WriteLine("Введiть адресу пацiєнта");
                    string adress = Console.ReadLine();
                    Console.WriteLine("Введiть прiзвище лiкаря");
                    string  Doc = Console.ReadLine();

                    Key insertkey = new Key(name, surname);
                    HT.insertEntry(insertkey, new Value(ID, Doc, adress),HT2);
                    ID++;
                    Console.WriteLine("Нового пацiєнта додано");

                }
                if (insert == 3)
                {
                    Console.WriteLine("Введiть iм'я пацiєнта");
                    string name = Console.ReadLine();
                    Console.WriteLine("Введiть прiзвище пацiєнта");
                    string surname = Console.ReadLine();
                    Key insertkey = new Key(name, surname);
                    HT.removeEntry(insertkey, HT2);
                    Console.WriteLine("Пацiєнта видалено");
                }
                if (insert == 4)
                {
                    Console.WriteLine("Введiть прiзвище лiкаря");
                    string Doc = Console.ReadLine();
                    HT2.findFamilyDoctorPatients(Doc);

                }

                insert = Convert.ToInt32(Console.ReadLine());

            }

      
           
        }
    }
    }
