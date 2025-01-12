using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JDoddsNAIT.ThemedUI
{
    [RequireComponent(typeof(RectTransform))]
    public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        enum ConstrainMode { Always, EndDragOnly }

        private RectTransform _parentRectTransform;

        [Tooltip("The target Rect Transform. Defaults to this Rect Transform")]
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private bool _constrainToParentRect;
        [SerializeField] private ConstrainMode _constrainMode;

        private Rect _rect, _parentRect;
        private RectTransform Target => _rectTransform != null
            ? _rectTransform
            : transform as RectTransform;

        private void MoveToMouse(PointerEventData mouse)
        {
            if (!enabled)
                return;

            Target.anchoredPosition += mouse.delta;

            if (_constrainToParentRect && _constrainMode is ConstrainMode.Always)
            {
                ConstrainRect();
            }
        }

        private void ConstrainRect()
        {
            if (!_constrainToParentRect || !enabled)
                return;

            if (Target.parent is not RectTransform parentRectTransform)
                return;

            _parentRectTransform = parentRectTransform;
            _parentRect = _parentRectTransform.rect;

            _rect = Target.rect;
            _rect.position += Target.anchoredPosition;

            if (_rect.xMin < _parentRect.xMin)
            {
                Target.anchoredPosition -= (_rect.xMin - _parentRect.xMin) * Vector2.right;
            }
            else if (_rect.xMax > _parentRect.xMax)
            {
                Target.anchoredPosition -= (_rect.xMax - _parentRect.xMax) * Vector2.right;
            }

            if (_rect.yMin < _parentRect.yMin)
            {
                Target.anchoredPosition -= (_rect.yMin - _parentRect.yMin) * Vector2.up;
            }
            else if (_rect.yMax > _parentRect.yMax)
            {
                Target.anchoredPosition -= (_rect.yMax - _parentRect.yMax) * Vector2.up;
            }

            Target.ForceUpdateRectTransforms();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_constrainToParentRect && _constrainMode is ConstrainMode.EndDragOnly)
            {
                ConstrainRect();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            MoveToMouse(eventData);
        }
    }
}