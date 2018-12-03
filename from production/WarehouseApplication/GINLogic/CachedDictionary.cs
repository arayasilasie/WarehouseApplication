using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;

namespace WarehouseApplication.GINLogic
{
    public class CachedDictionary<T> : IDictionary<object, string>
    {
        public delegate string Translate(T value);
        public delegate T GetValue(object key);
        public delegate List<KeyValuePair<object, T>> GetLookup();
        private Dictionary<object, T> store = new Dictionary<object, T>();

        private Translate translator;
        private GetValue valueFinder;
        private GetLookup lookupFetcher = null;
        private bool lookupFetched = false;

        public CachedDictionary(Translate translator, GetValue valueFinder)
        {
            this.translator = translator;
            this.valueFinder = valueFinder;
        }

        public CachedDictionary(Dictionary<object, T> store, Translate translator, GetValue valueFinder)
        {
            this.store = store;
            this.translator = translator;
            this.valueFinder = valueFinder;
        }

        public CachedDictionary(Translate translator, GetValue valueFinder, GetLookup lookupFetcher)
        {
            this.translator = translator;
            this.valueFinder = valueFinder;
            this.lookupFetcher = lookupFetcher;
        }

        public Dictionary<object, T> Store
        {
            get { return store; }
        }

        #region IDictionary<object,string> Members

        public void Add(object key, string value)
        {
            throw new InvalidOperationException("Can't add entries");
        }

        public bool ContainsKey(object key)
        {
            if (!store.ContainsKey(key))
            {
                T value = valueFinder(key);
                if (value == null)
                {
                    return false;
                }
                store.Add(key, value);
                return true;
            }
            else
            {
                return true;
            }
        }

        public ICollection<object> Keys
        {
            get
            {
                if (lookupFetched || (lookupFetcher == null))
                {
                    return store.Keys;
                }
                lookupFetched = true;
                List<KeyValuePair<object, T>> theLookup = lookupFetcher();
                foreach (KeyValuePair<object, T> kvp in theLookup)
                {
                    store[kvp.Key] = kvp.Value;
                }
                return store.Keys;
            }
        }

        public bool Remove(object key)
        {
            return store.Remove(key);
        }

        public bool TryGetValue(object key, out string value)
        {
            T tValue = default(T);
            bool hasValue = store.TryGetValue(key, out tValue);
            value = translator(tValue);
            return hasValue;
        }

        public ICollection<string> Values
        {
            get
            {
                if (lookupFetched || (lookupFetcher == null))
                {
                    List<string> strValues = new List<string>();
                    foreach (T tValue in store.Values)
                    {
                        strValues.Add(translator(tValue));
                    }
                    return strValues;
                }
                lookupFetched = true;
                List<KeyValuePair<object, T>> theLookup = lookupFetcher();
                foreach (KeyValuePair<object, T> kvp in theLookup)
                {
                    store[kvp.Key] = kvp.Value;
                }
                //recursively call the same property. Note lookupFetched == true, guarantying backtracking
                return Values;
            }
        }

        public string this[object key]
        {
            get
            {
                if (ContainsKey(key))
                {
                    return translator(store[key]);
                }
                else
                {
                    throw new ArgumentException("No object found", "key");
                }
            }
            set
            {
                throw new InvalidOperationException("Can't set the value of an entry");
            }
        }

        #endregion

        #region ICollection<KeyValuePair<object,string>> Members

        public void Add(KeyValuePair<object, string> item)
        {
            throw new InvalidOperationException("Can't add entries");
        }

        public void Clear()
        {
            store.Clear();
        }

        public bool Contains(KeyValuePair<object, string> item)
        {
            throw new InvalidOperationException("operation not supported");
        }

        public void CopyTo(KeyValuePair<object, string>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentException("Array can't be null", "array");
            }
            if (arrayIndex < 0)
            {
                throw new ArgumentException("ArrayIndex can't be negative", "arrayIndex");
            }
            KeyValuePair<object, T>[] tKVPs = new KeyValuePair<object, T>[array.Length];
            ((IDictionary<object, T>)store).CopyTo(tKVPs, arrayIndex);
            for (int i = 0; i < tKVPs.Length; i++)
            {
                array[i] = new KeyValuePair<object, string>(tKVPs[i].Key, translator(tKVPs[i].Value));
            }
        }

        public int Count
        {
            get { return store.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(KeyValuePair<object, string> item)
        {
            throw new InvalidOperationException("Can't remove entries");
        }

        #endregion

        #region IEnumerable<KeyValuePair<object,string>> Members

        public IEnumerator<KeyValuePair<object, string>> GetEnumerator()
        {
            List<KeyValuePair<object, string>> enumerator = new List<KeyValuePair<object, string>>();
            foreach (KeyValuePair<object, T> tKVP in store)
            {
                enumerator.Add(new KeyValuePair<object, string>(tKVP.Key, translator(tKVP.Value)));
            }
            return enumerator.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            ArrayList list = new ArrayList();
            foreach (KeyValuePair<object, T> tKVP in store)
            {
                list.Add(new KeyValuePair<object, string>(tKVP.Key, translator(tKVP.Value)));
            }
            return list.GetEnumerator();
        }

        #endregion


    }
}
