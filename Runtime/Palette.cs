using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDoddsNAIT.ThemedUI
{
    [System.Serializable]
    public class Palette<T>
    {
        [SerializeField] private T _default;
        [SerializeField] private T[] _additionalValues;

        public T Default { get => _default; set => _default = value; }
        public T[] AdditionalValues { get => _additionalValues; set => _additionalValues = value; }

        public T this[int i]
        {
            get
            {
                if (i < 0 || i > AdditionalValues.Length)
                {
                    throw new System.IndexOutOfRangeException();
                }

                return i == 0 ? Default : AdditionalValues[i - 1];
            }
        }

        public T this[IPaletteIndexer<T> indexer]
        {
            get
            {
                return indexer.PaletteIndex switch
                {
                    PaletteIndex.Default => Default,
                    PaletteIndex.Name => indexer.Get(this),
                    PaletteIndex.Custom => indexer.CustomValue,
                    _ => throw new System.NotImplementedException(),
                };
            }
        }
    }
}