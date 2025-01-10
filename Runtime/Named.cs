using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace JDoddsNAIT.ThemedUI
{
    /// <summary>
    /// A variable of type <typeparamref name="TVar"/> with a name attached.
    /// </summary>
    /// <remarks>
    /// Unique names are not enforced.
    /// </remarks>
    /// <typeparam name="TVar"></typeparam>
    [System.Serializable]
    public class Named<TVar>
    {
        [SerializeField] private string _name;
        [SerializeField] private TVar _value;

        public string Name { get => _name; private set => _name = value; }
        public TVar Value { get => _value; set => _value = value; }

        public static implicit operator TVar(Named<TVar> value) => value.Value;
    }

    public static class SearchForName
    {
        public static IEnumerable<Named<T>> FindAllWithName<T>(this IEnumerable<Named<T>> collection, string name)
        {
            return from Named<T> item in collection
                   where item.Name == name
                   select item;
        }

        public static Named<T> FindFirstWithName<T>(this IEnumerable<Named<T>> collection, string name)
        {
            return collection.FindAllWithName(name).FirstOrDefault();
        }

        public static Named<T> Find<T>(this Palette<Named<T>> palette, string name)
        {
            static IEnumerable<Named<T>> getAllContents(Palette<Named<T>> palette)
            {
                for (int i = 0; i < palette.AdditionalValues.Length + 1; i++)
                {
                    yield return palette[i];
                }
            }

            return getAllContents(palette).FindFirstWithName(name);
        }
    }
}