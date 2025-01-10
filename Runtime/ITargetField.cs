using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JDoddsNAIT.ThemedUI
{
    public interface ITargetField
    {
        public void ApplyTheme(Theme theme);
    }

    [System.Serializable]
    public abstract class TargetField<T> : ITargetField where T : Object
    {
        [SerializeField, HideInInspector] protected string _name;
        [SerializeField] protected T _target;

        public Object Target { get => _target; private set => _target = value as T; }

        public virtual void ApplyTheme(Theme theme)
        {
            //Debug.Log(Target.name);
            if (_target != null)
            {
                _name = _target.name;
            }
        }
    }

    [System.Serializable]
    public class TargetGraphic : TargetField<Graphic>
    {
        [SerializeField] private NamedIndexer<Color> _color;

        public override void ApplyTheme(Theme theme)
        {
            base.ApplyTheme(theme);
            var x = theme.ColorPalette[_color];
            if (x != null && Target != null)
                _target.color = x.Value;
        }
    }

    [System.Serializable]
    public class TargetText : TargetField<TextMeshProUGUI>
    {
        [SerializeField] private NamedIndexer<TMP_FontAsset> _font;

        public override void ApplyTheme(Theme theme)
        {
            base.ApplyTheme(theme);
            var x = theme.FontPalette[_font];
            if (x is not null && Target != null)
                _target.fontSharedMaterial = x.Value.material;
        }
    }

    [System.Serializable]
    public class TargetImage : TargetField<Image>
    {
        [SerializeField] private NamedIndexer<Sprite> _sprite;

        public override void ApplyTheme(Theme theme)
        {
            base.ApplyTheme(theme);
            var x = theme.SpritePalette[_sprite];
            if (x is not null && Target != null)
                _target.sprite = x.Value;
        }
    }
}