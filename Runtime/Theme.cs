using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace JDoddsNAIT.ThemedUI
{
    [CreateAssetMenu(fileName = nameof(Theme), menuName = "JDoddsNAIT/New UI Theme")]
    public class Theme : ScriptableObject
    {
        [Header("Colours")]
        [SerializeField] private Palette<Named<Color>> _colorPalette = new();
        [Header("Fonts")]
        [SerializeField] private Palette<Named<TMP_FontAsset>> _fontPalette = new();
        [Header("Sprites")]
        [SerializeField] private Palette<Named<Sprite>> _spritePalette = new();
        [Header("Sounds")]
        [SerializeField] private Palette<Named<AudioClip>> _sounds = new();

        public Palette<Named<Color>> ColorPalette { get => _colorPalette; set => _colorPalette = value; }
        public Palette<Named<TMP_FontAsset>> FontPalette { get => _fontPalette; set => _fontPalette = value; }
        public Palette<Named<Sprite>> SpritePalette { get => _spritePalette; set => _spritePalette = value; }
        
        public event System.Action<Theme> OnModified;

        private void OnValidate()
        {
            OnModified?.Invoke(this);
        }
    }
}