using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JDoddsNAIT.ThemedUI
{
    public class Paginator : MonoBehaviour
    {
        protected enum PageBehavior { SetActive, Animation, None }

        [SerializeField, Min(0)] private int _currentPageIndex;
        [SerializeField] private List<GameObject> _pages;
        [SerializeField] protected PageBehavior _pageBehavior;
        [SerializeField] private bool _sendMessage;
        [SerializeField] private string _methodName;
        [Space]
        [SerializeField] private UnityEvent<int> _onPageSet;

        public List<GameObject> Pages { get => _pages; protected set => _pages = value; }
        public int CurrentPageIndex
        {
            get => _currentPageIndex; set
            {
                if (_currentPageIndex != value)
                {
                    int delta = value - _currentPageIndex;
                    _currentPageIndex = (int)Mathf.Repeat(_currentPageIndex, Pages.Count);
                    SetPage(_pages[value], delta);
                }
            }
        }

        /// <summary>
        /// Increments the current page index by <paramref name="count"/>
        /// </summary>
        /// <param name="count"></param>
        public void Increment(int count)
        {
            CurrentPageIndex += count;
        }

        protected virtual void SetPage(GameObject page, int delta)
        {
            switch (_pageBehavior)
            {
                case PageBehavior.SetActive:
                    foreach (var pg in _pages)
                    {
                        pg.SetActive(pg == page);
                    }
                    break;
                case PageBehavior.Animation:
                    throw new System.NotImplementedException();
                    //break;
                default:
                    break;
            }

            if (_sendMessage)
            {
                page.SendMessage(_methodName);
            }

            _onPageSet.Invoke(delta);
        }
    }
}
