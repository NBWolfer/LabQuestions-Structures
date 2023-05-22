using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAttempts
{
    public class Hashtable
    {
        public class HashTable
        {
            private const int Size = 100;
            private LinkedList<KeyValuePair>[] buckets;

            public HashTable()
            {
                buckets = new LinkedList<KeyValuePair>[Size];
            }

            public void Add(string key, object value)
            {
                int index = GetIndex(key);
                if (buckets[index] == null)
                {
                    buckets[index] = new LinkedList<KeyValuePair>();
                }

                LinkedList<KeyValuePair> bucket = buckets[index];

                foreach (KeyValuePair pair in bucket)
                {
                    if (pair.Key == key)
                    {
                        throw new ArgumentException("An item with the same key already exists in the hashtable.");
                    }
                }

                bucket.AddLast(new KeyValuePair(key, value));
            }

            public object Get(string key)
            {
                int index = GetIndex(key);
                LinkedList<KeyValuePair> bucket = buckets[index];

                if (bucket != null)
                {
                    foreach (KeyValuePair pair in bucket)
                    {
                        if (pair.Key == key)
                        {
                            return pair.Value;
                        }
                    }
                }

                return null;
            }

            private int GetIndex(string key)
            {
                int hash = key.GetHashCode();
                int index = hash % Size;
                return index;
            }

            private class KeyValuePair
            {
                public string Key { get; }
                public object Value { get; }

                public KeyValuePair(string key, object value)
                {
                    Key = key;
                    Value = value;
                }
            }
        }

    }
}
