using System;
using System.Collections.Generic;

namespace PubSub
{
    /// <summary>
    /// Example:
    ///     string test = " world";
    ///     // let's define a subscriber
    ///     Action<object> hi = (ob) => { Console.WriteLine("hi" + ob); };
    ///     // let's subscribe
    ///     Pub.Sub["hello"] = hi;
    ///     // lets publish test on hello channel
    ///     Pub.Sub["hello"](test);
    ///     // now let's remove our subscriber
    ///     Pub.Sub.Remove("hello", hi);
    ///     // EASY
    /// </summary>
    public class Pub
    {
        private static Dictionary<string, List<Action<object>>> actions = new Dictionary<string, List<Action<object>>>();

        /// <summary>
        /// Subscribe a to a channel with a new Action, or publish an object to a channel
        /// Pub.Sub["channel"] = (ob) { /* subscribe */ };
        /// Pub.Sub["channel"]("publish");
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Action<object> this[string key]
        {
            get
            {
                var actionList = new List<Action<object>>();
                if (!actions.TryGetValue(key, out actionList)) {
                    actionList = new List<Action<object>>();
                }
                Action<object> ret = (obj) => {
                    actionList.ForEach(a => a(obj));
                };
                return ret;
            }
            set
            {
                var actionList = new List<Action<object>>();
                if (!actions.TryGetValue(key, out actionList)) {
                    actions[key] = new List<Action<object>>();
                }
                actions[key].Add(value);
            }
        }

        public bool Remove(string key, Action<object> action)
        {
            if (actions.ContainsKey(key)) {
                return actions[key].Remove(action);
            }
            return false;
        }

        public bool RemoveAll(string key, Action<object> action)
        {
            if (actions.ContainsKey(key)) {
                actions[key].Clear();
                return true;
            }
            return false;
        }
        public static Pub Sub
        {
            get
            {
                if (instance == null) {
                    lock (syncRoot)
                    {
                        if (instance == null) { instance = new Pub(); }
                    }
                }

                return instance;
            }
        }

        private static volatile Pub instance;
        private static object syncRoot = new Object();

        private Pub() { }
    }
}
