using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TagKid.Lib.Cache
{
    public abstract class CacheBase<TKey, TValue>
    {
        private readonly Hashtable _hashtable = new Hashtable();

        public TValue this[TKey key]
        {
            get
            {
                if (_hashtable.Count == 0)
                    Load();

                if (_hashtable.ContainsKey(key))
                    return (TValue)_hashtable[key];
                return default(TValue);
            }
            set
            {
                if (_hashtable.ContainsKey(key))
                    _hashtable[key] = value;
                else
                    _hashtable.Add(key, value);
            }
        }

        public void Clear()
        {
            _hashtable.Clear();
        }

        public void Remove(TKey key)
        {
            if (_hashtable.ContainsKey(key))
                _hashtable.Remove(key);
        }

        public IEnumerable<TValue> Find(params TKey[] keys)
        {
            return _hashtable.Keys.Cast<TKey>().Where(keys.Contains).Select(k => this[k]);
        }

        protected abstract void Load();
    }
}
