using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pair<KEY, VALUE> where KEY : IComparable
{
    public KEY key;
    public VALUE value;
}

[Serializable]
public class Map<KEY, VALUE> where KEY : IComparable
{
    [SerializeField] private List<Pair<KEY, VALUE>> pairs;
    
    public Map()
    {
        pairs = new List<Pair<KEY, VALUE>>();
    }

    public VALUE[] Values()
    {
        VALUE[] ret = new VALUE[pairs.Count];

        for (int i = 0; i < pairs.Count; ++i)
        {
            ret[i] = pairs[i].value;
        }

        return ret;
    }

    public VALUE this[KEY key]
    {
        get
        {
            bool found = false;
            VALUE ret = default(VALUE);

            int index = getKeyIndex(key);
            if (index != -1)
            {
                ret = pairs[index].value;
                found = true;
            }

            if (!found)
                Debug.LogError("Invalid Key: " + key.ToString());

            return ret;
        }
    }

    public VALUE[] getValues(KEY key, int numValues)
    {
        VALUE[] values = new VALUE[numValues];

        int index = getKeyIndex(key);

        if (index == -1 || (index + numValues - 1) >= pairs.Count)
            Debug.LogError("Invalid Key: " + key.ToString());

        // Get Sequential Values
        for (int i = index; i < index + numValues; ++i)
        {
            int j = i - index;
            values[j] = pairs[i].value;
        }

        return values;
    }



    public List<VALUE> GetValues(params KEY[] keys)
    {
        return new List<VALUE>(getValues(keys));
        /*
        VALUE[] ret = new VALUE[keys.Length];

        // Find Values for All Keys Provided and Stored in Same Order as keys
        for (int i = 0; i < keys.Length; ++i)
        {
            ret[i] = this[keys[i]];
        }

        return new List<VALUE> (ret);
        */
    }




    public VALUE[] getValues(params KEY[] keys)
    {
        VALUE[] ret = new VALUE[keys.Length];

        // Find Values for All Keys Provided and Stored in Same Order as keys
        for (int i = 0; i < keys.Length; ++i)
        {
            ret[i] = this[keys[i]];
        }

        return ret;
    }

    private int getKeyIndex(KEY key)
    {
        for (int i = 0; i < pairs.Count; ++i)
            if (pairs[i].key.CompareTo(key) == 0)
                return i;

        return -1;
    }
}
