using System.Collections.Generic;

public static class DictionaryExtension
{
    public static Tvalue TryGet<Tkey,Tvalue>(this Dictionary<Tkey, Tvalue> dic, Tkey key)
    {
        Tvalue value;
        dic.TryGetValue(key, out value);
        return value;
    }

}
