using UnityEngine;

namespace GameWork.Unity.Engine.UI.Components
{
    [RequireComponent(typeof(RectTransform))]
    [ExecuteInEditMode]
    public class ResizeToFitChildren : MonoBehaviour
    {
        [SerializeField]
        private bool _resizeX = true;
        [SerializeField]
        private bool _resizeY = true;
        [SerializeField]
        private Vector2 _padding;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            Resize();
        }

        private void Resize()
        {
            Vector3 min, max;

            GetChildBoundsExtremes(out min, out max);

            var sizeDelta = _rectTransform.sizeDelta;

            if (_resizeX)
            {
                sizeDelta.x = max.x - min.x;
            }

            if (_resizeY)
            {
                sizeDelta.y = max.y - min.y;
            }

            _rectTransform.sizeDelta = sizeDelta + _padding;
        }

        private void GetChildBoundsExtremes(out Vector3 min, out Vector3 max)
        {
            min = new Vector3();
            max = new Vector3();

            var firstIteration = true;

            foreach (RectTransform childTransform in _rectTransform)
            {
                var childMin = childTransform.anchoredPosition - (childTransform.sizeDelta / 2f);
                var childMax = childTransform.anchoredPosition + (childTransform.sizeDelta / 2f);

                if (firstIteration)
                {
                    min = childMin;
                    max = childMax;

                    firstIteration = false;
                }
                else
                {
                    if (childMin.x < min.x)
                    {
                        min.x = childMin.x;
                    }

                    if (childMin.y < min.y)
                    {
                        min.y = childMin.y;
                    }

                    if (childMax.x > max.x)
                    {
                        max.x = childMax.x;
                    }

                    if (childMax.x > max.x)
                    {
                        max.x = childMax.x;
                    }
                }
            }
        }
    }
}
