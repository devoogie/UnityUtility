
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public partial class Util
{
    public static GameObject FindChild(GameObject go, string name = null, bool isRecursive = false)
    {
        var transform = FindChild(go, name, isRecursive);
        return transform?.gameObject ?? null;
    }
    public static T FindChild<T>(GameObject go, string name = null, bool isRecursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (isRecursive)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                var transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    var component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }

            }
        }
        else
        {
            T[] components = go.GetComponentsInChildren<T>(true);
            foreach (T component in components)
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

}

public static partial class Util
{
    #region Array
    public static T Random<T>(this T[] array)
    {
        if (array.IsNullOfEmpty() == false)
            return array[UnityEngine.Random.Range(0, array.Length)];
        return default(T);
    }
    // public static T Repeat<T>(this T[] array, int index)
    // {
    //     if (array.IsNullOfEmpty())
    //         return default(T);
    //     return array[UnityEngine.Mathf.Repeat(index, array.Length - 1).ToIntRound()];
    // }
    public static void Shuffle<T>(this T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            T originValue = array[i];
            int randomIndex = UnityEngine.Random.Range(i, array.Length);
            array[i] = array[randomIndex];
            array[randomIndex] = originValue;
        }
    }

    public static T Last<T>(this T[] array)
    {
        if (array.IsNullOfEmpty())
            return default(T);

        return array[array.Length - 1];
    }
    public static T First<T>(this T[] list)
    {
        if (list.IsNullOfEmpty())
            return default(T);

        return list[0];
    }
    #endregion
    #region List 
    public static T Random<T>(this List<T> list, bool isRemove = false)
    {
        if (list.IsNullOfEmpty() == false)
        {
            int randomIndex = UnityEngine.Random.Range(0, list.Count);
            T t = list[randomIndex];
            if (isRemove)
                list.RemoveAt(randomIndex);
            return t;
        }
        return default(T);
    }
    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T originValue = list[i];
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = originValue;
        }

    }
    public static T Last<T>(this List<T> list)
    {
        if (list.IsNullOfEmpty())
            return default(T);

        return list[list.Count - 1];
    }
    public static T First<T>(this List<T> list)
    {
        if (list.IsNullOfEmpty())
            return default(T);

        return list[0];
    }
    public static T Clamp<T>(this List<T> list, int index)
    {
        int count = list.Count - 1;
        index = Mathf.Clamp(index, 0, count - 1);

        return list[index];
    }
    public static void LeaveDebugLog<T>(this List<T> list)
    {
        string contents = "";
        foreach (var item in list)
        {
            contents += item.ToString() + "\n";
        }
        Debug.Log(contents);
    }

    #endregion

    public static bool IsNullOfEmpty(this ICollection collection)
    {
        return collection == null || collection.Count <= 0;
    }
    public static bool IsValid(this ICollection collection, int index)
    {
        return collection.IsNullOfEmpty() == false && index < collection.Count && index >= 0;
    }

    public static bool IsPassRate(float pivot)
    {
        if (pivot >= 1)
            return true;
        return UnityEngine.Random.Range(0, 1f) < pivot;
    }
    public static bool IsPassRate(int value, int max)
    {
        if (value >= max)
            return true;
        return UnityEngine.Random.Range(0, max) < value;
    }
    public static bool TryAddOrUpdateValue<TKey, TValue>(this Dictionary<TKey, TValue> variable, TKey key, TValue value)
    {
        bool result;
        try
        {
            if (variable.ContainsKey(key))
            {
                variable[key] = value;
                result = true;
            }
            else
                result = variable.TryAddValue(key, value);
        }
        catch
        {
            result = false;
        }

        return result;
    }

    public static bool TryAddValue<TKey, TValue>(this Dictionary<TKey, TValue> variable, TKey key, TValue value)
    {
        bool result;
        try
        {
            variable.Add(key, value);
            result = true;
        }
        catch
        {
            result = false;
        }
        return result;
    }
    public static int RandomIndex(params float[] ranges)
    {
        float max = 0;
        for (int i = 0; i < ranges.Length; i++)
            max += ranges[i];
        float randomValue = UnityEngine.Random.Range(0, max);
        for (int i = 0; i < ranges.Length; i++)
        {
            randomValue -= ranges[i];
            if (randomValue <= 0)
                return i;
        }
        return -1;
    }
    public static T RandomValue<T>(params T[] candidate)
    {
        int randomIndex = UnityEngine.Random.Range(0, candidate.Length);
        return candidate[randomIndex];
    }
    public static T DeepClone<T>(this T origin) where T : class
    {
        using (MemoryStream stream = new MemoryStream())
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, origin);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)binaryFormatter.Deserialize(stream);
        }
    }
    public static string ToJson(this object data)
    {
        var dataJson = JsonUtility.ToJson(data);
        return dataJson;
    }
    public static T FromJson<T>(this string data)
    {
        return JsonUtility.FromJson<T>(data);
    }
    public static T ToEnum<T>(this string valueToParse)
    {
        T returnValue = default(T);
        if (valueToParse.IsNullOfEmpty())
            return returnValue;
        string value = Util.StringAppend(valueToParse[0].ToString().ToUpper(), valueToParse.Substring(1));
        if (System.Enum.IsDefined(typeof(T), value))
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            returnValue = (T)converter.ConvertFromString(value);
        }
        return returnValue;
    }
    public static bool IsWaitTime(this float time)
    {
        return !IsPassTime(time);
    }
    public static bool IsPassTime(float startTime, float duration)
    {
        return Time.time - startTime > duration;
    }
    public static bool IsPassTime(this float time)
    {
        return Time.time > time;
    }


    // public static List<T> RandomRange<T>(this List<T> variable, int max, bool isCopy = false)
    // {
    //     List<T> list = variable;
    //     if (isCopy)
    //     {
    //         list = new List<T>();
    //         list.AddRange(variable);
    //     }
    //     list.Shuffle();
    //     int count = max;
    //     if (list.Count < max)
    //         count = 0;
    //     list.RemoveRange(0, list.Count - count);
    //     return list;
    // }
    // public static List<T> RandomRange<T>(this List<T> variable, int max, Predicate<T> match)
    // {
    //     List<T> list = variable.FindAll(match);
    //     list.Shuffle();
    //     int count = max;
    //     if (list.Count < max)
    //         count = 0;
    //     list.RemoveRange(0, list.Count - count);
    //     return list;
    // }

    public static bool ToBool(this int pivot)
    {
        return pivot == 1 ? true : false;
    }
    public static int ToInt(this bool pivot)
    {
        return pivot ? 1 : 0;
    }
    public static string ToJsonList<T>(this List<T> list)
    {
        JsonParseList<T> parseList = new JsonParseList<T>();
        parseList.data = list;

        return parseList.ToJson();
    }

    public static List<T> FromJsonList<T>(this string data)
    {
        JsonParseList<T> parseList = JsonUtility.FromJson<JsonParseList<T>>(data);

        return parseList.data;
    }
    public static int ToFloor(this float target)
    {
        return Mathf.FloorToInt(target);
    }
    public static int ToCeil(this float target)
    {
        return Mathf.CeilToInt(target);
    }
    public static int ToRound(this float target)
    {
        return Mathf.RoundToInt(target);
    }
}

[System.Serializable]
public class JsonParseList<T>
{
    public List<T> data = new List<T>();
    /// <summary>
    /// From https://answers.unity.com/questions/460727/how-to-serialize-dictionary-with-unity-serializati.html
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [System.Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<TKey> keys = new List<TKey>();

        [SerializeField]
        private List<TValue> values = new List<TValue>();

        // save the dictionary to lists
        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();
            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        // load dictionary from lists
        public void OnAfterDeserialize()
        {
            this.Clear();

            if (keys.Count != values.Count)
                throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

            for (int i = 0; i < keys.Count; i++)
                this.Add(keys[i], values[i]);
        }
    }
}
