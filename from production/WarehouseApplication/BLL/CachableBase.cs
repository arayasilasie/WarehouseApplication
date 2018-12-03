using System;
using System.Web.Caching;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace WarehouseApplication.BLL
{
    public class CacheManager<T>
    {
        public delegate List<T> AllItemsEnumerator();
        public delegate T ItemLocator(string id);
        public delegate string ItemIdentifier(T item);

        public enum CacheDurability
        {
            Short,
            Moderate,
            Long,
            Indefinite
        }

        private Cache cache = HttpContext.Current.Cache;
        private bool isAllItemCache = false;
        private string className;
        private CacheDurability durability;
        private AllItemsEnumerator allItemsEnumerator = null;
        private ItemLocator itemLocator = null;
        private ItemIdentifier itemIdentifier = null;

        public CacheManager(string className, ItemLocator itemLocator, ItemIdentifier itemIdentifier)
            : this(className, itemLocator, itemIdentifier, CacheDurability.Moderate)
        {
        }

        public CacheManager(string className, ItemLocator itemLocator, ItemIdentifier itemIdentifier, CacheDurability durability)
        {
            this.className = className;
            this.itemLocator = itemLocator;
            this.itemIdentifier = itemIdentifier;
            this.durability = durability;
            Log(className, CacheLogType.EmptyCacheStarted, durability.ToString());
        }

        public CacheManager(string className, AllItemsEnumerator allItemsEnumerator, ItemIdentifier itemIdentifier) :
            this(className, allItemsEnumerator, itemIdentifier, CacheDurability.Long)
        {
        }

        public CacheManager(string className, AllItemsEnumerator allItemsEnumerator, ItemIdentifier itemIdentifier, CacheDurability durability)
        {
            isAllItemCache = true;
            this.className = className;
            this.allItemsEnumerator = allItemsEnumerator;
            this.itemIdentifier = itemIdentifier;
            this.durability = durability;
            TimeSpan duration = TimeSpan.Parse(ConfigurationManager.AppSettings[Enum.GetName(typeof(CacheDurability), durability)]);
            List<T> item = allItemsEnumerator();
            if (durability != CacheDurability.Indefinite)
            {
                cache.Insert(className, item, null, DateTime.UtcNow.Add(duration), Cache.NoSlidingExpiration);
            }
            else
            {
                cache.Insert(className, item, null, Cache.NoAbsoluteExpiration, duration,
                    delegate(string key, CacheItemUpdateReason reason, out object cachedItem, out CacheDependency dependancy, out DateTime absoluteTime, out TimeSpan newDuration)
                    {
                        cachedItem = cache[key];
                        dependancy = null;
                        absoluteTime = Cache.NoAbsoluteExpiration;
                        newDuration = duration;
                        Log(className, CacheLogType.AllItemCacheRefreshed, duration.ToString());
                    });
            }
            Log(className, CacheLogType.AllItemCacheStarted, durability.ToString());
        }

        public T GetItem(string id)
        {
            if (!isAllItemCache)
            {
                T item = (T)cache[ItemCacheId(id)];
                if (item != null)
                {
                    Log(className, CacheLogType.SingleItemRead, id);
                    return item;
                }
                item = itemLocator(id);
                if (item == null)
                {
                    throw new Exception(string.Format("Item not found: The requested item (id={0}) is not part of this lookup", id));
                }
                string cacheId = itemIdentifier(item);
                if (id != cacheId)
                {
                    throw new ArgumentException(string.Format("The fetched item can not be evaluated to yield the required id ({0})", id), "identifier");
                }
                TimeSpan duration = TimeSpan.Parse(ConfigurationManager.AppSettings[Enum.GetName(typeof(CacheDurability), durability)]);

                Log(className, CacheLogType.SingleItemCached, id);
                if (durability != CacheDurability.Indefinite)
                {
                    cache.Insert(ItemCacheId(id), item, null, DateTime.UtcNow.Add(duration), Cache.NoSlidingExpiration);
                }
                else
                {
                    cache.Insert(ItemCacheId(id), item, null, Cache.NoAbsoluteExpiration, duration,
                        delegate(string key, CacheItemUpdateReason reason, out object cachedItem, out CacheDependency dependancy, out DateTime absoluteTime, out TimeSpan newDuration)
                        {
                            Log(className, CacheLogType.SingleItemCacheRefreshed, id);
                            cachedItem = cache[key];
                            dependancy = null;
                            absoluteTime = Cache.NoAbsoluteExpiration;
                            newDuration = duration;
                        });
                }
                return item;
            }
            else
            {
                try
                {
                    List<T> items = (List<T>)cache[className];
                    if (items != null)
                    {
                        Log(className, CacheLogType.SingleItemRead, id);
                    }
                    else
                    {
                        Log(className, CacheLogType.AllItemCacheRefreshed, durability.ToString());
                        Log(className, CacheLogType.SingleItemRead, durability.ToString());
                        TimeSpan duration = TimeSpan.Parse(ConfigurationManager.AppSettings[Enum.GetName(typeof(CacheDurability), durability)]);
                        items = allItemsEnumerator();
                        cache.Insert(className, items, null, DateTime.UtcNow.Add(duration), Cache.NoSlidingExpiration);
                    }
                    return (from t in (List<T>)cache[className] where id == itemIdentifier(t) select t).First();
                }
                catch
                {
                    throw new Exception(string.Format("Item not found: The requested item (id={0}) is not part of this lookup", id));
                }
            }
        }

        public List<T> GetAllItems()
        {
            if (isAllItemCache)
                return (List<T>)cache[className];
            IDictionaryEnumerator cacheEnumerator = cache.GetEnumerator();
            List<T> values = new List<T>();
            cacheEnumerator.Reset();
            while (cacheEnumerator.MoveNext())
            {
                if((cacheEnumerator.Key.ToString().Length > className.Length+3) && (cacheEnumerator.Key.ToString().Substring(0, className.Length + 3) ==
                    className + "/-\\"))
                {
                    values.Add((T)cacheEnumerator.Value);
                }
            }
            return values;
        }

        public void Remove(string id)
        {
            string cacheId = isAllItemCache ? id : ItemCacheId(id);
            cache.Remove(cacheId);
        }

        private void Log(string cacheName, CacheLogType logType, string detail)
        {
            //if (File.Exists("C:\\CacheLog\\log.txt"))
            //{
            //    using (StreamWriter wr = File.AppendText("C:\\CacheLog\\log.txt"))
            //    {
            //        wr.WriteLine("{0:hh-mm-ss} - Cache : {1}\t\t {2}\t\t{3}", DateTime.Now, cacheName, logType, detail);
            //    }
            //}
        }

        private string ItemCacheId(string id)
        {
            return string.Format("{0}/-\\{1}",className, id);
        }

        private enum CacheLogType
        {
            AllItemCacheStarted,
            EmptyCacheStarted,
            AllItemCacheExpired,
            AllItemCacheRefreshed,
            SingleItemCacheRefreshed,
            SingleItemCached,
            CachedItemEnumerated,
            SingleItemRead
        }
    }
}
