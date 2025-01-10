using UnityEngine;

namespace JDoddsNAIT.ThemedUI
{
    public enum PaletteIndex { Default = 0, Name = 1, Custom = 2 }

    public interface IPaletteIndexer<T>
    {
        public PaletteIndex PaletteIndex { get; }
        public T CustomValue { get; }
        public T Get(Palette<T> palette);
    }


    [System.Serializable]
    public class IntIndexer<T> : IPaletteIndexer<T>
    {
        [SerializeField] private PaletteIndex _index;
        [SerializeField] private int _paletteIndex = 0;
        [SerializeField] private T _customValue;

        PaletteIndex IPaletteIndexer<T>.PaletteIndex => _index;

        T IPaletteIndexer<T>.CustomValue => _customValue;

        T IPaletteIndexer<T>.Get(Palette<T> palette)
        {
            return palette.AdditionalValues.Length > 0 ? palette.AdditionalValues[_paletteIndex] : palette.Default;
        }
    }

    [System.Serializable]
    public class NamedIndexer<T> : IPaletteIndexer<Named<T>>
    {
        [SerializeField] private PaletteIndex _index;
        [SerializeField] private string _name;
        [SerializeField] private Named<T> _customValue;

        PaletteIndex IPaletteIndexer<Named<T>>.PaletteIndex => _index;

        Named<T> IPaletteIndexer<Named<T>>.CustomValue => _customValue;

        Named<T> IPaletteIndexer<Named<T>>.Get(Palette<Named<T>> palette)
        {
            return palette.Find(_name);
        }
    }
}