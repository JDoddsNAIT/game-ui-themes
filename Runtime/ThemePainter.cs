using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace JDoddsNAIT.ThemedUI
{
    [ExecuteAlways]
    public class ThemePainter : MonoBehaviour
    {
        [SerializeField] private Theme _theme;
        [Tooltip("")]
        [SerializeField] private bool _autoUpdate;
        [Space]
        [SerializeField] private List<TargetGraphic> _targetGraphics;
        [Space]
        [SerializeField] private List<TargetImage> _targetImages;
        [Space]
        [SerializeField] private List<TargetText> _targetTexts;

        private Theme _previousTheme;

        private IEnumerable<ITargetField> _targets;

        private void OnEnable()
        {
            if (_theme)
                _theme.OnModified += ApplyTheme;
        }

        private void OnDisable()
        {
            if (_theme)
                _theme.OnModified -= ApplyTheme;
        }

        private void OnValidate()
        {
            if (_theme)
            {
                _theme.OnModified += ApplyTheme;
            }


            if (_autoUpdate)
            {
                ApplyTheme();
            }
        }

        [ContextMenu("Apply Theme")]
        public void ApplyTheme() => ApplyTheme(_theme);
        public void ApplyTheme(Theme theme)
        {
            if (_theme && _theme == theme)
            {
                _targets = GetAllTargets();
                foreach (var target in _targets)
                {
                    target?.ApplyTheme(_theme);
                }
            }
            else
            {
                if (_theme)
                {
                    theme.OnModified -= ApplyTheme;
                }
                else
                {
                    Debug.Log($"No theme provided.");
                }
            }
        }

        private IEnumerable<ITargetField> GetAllTargets()
        {
            return _targetGraphics.OfType<ITargetField>()
                .Concat(_targetTexts.OfType<ITargetField>())
                .Concat(_targetImages.OfType<ITargetField>());
        }
    }

}