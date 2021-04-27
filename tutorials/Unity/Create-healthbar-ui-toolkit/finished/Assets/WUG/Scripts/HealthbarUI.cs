using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UIElements;
using Assets.WUG;

namespace Assets.WUG.Scripts
{
    public class HealthbarUI : MonoBehaviour
    {
        [Tooltip("The world space position that the UI should render itself")]
        public Transform TransformToFollow;

        private VisualElement m_Bar;
        private Camera m_MainCamera;

        private VisualElement[] m_Hearts;

        private void Start()
        {
            m_MainCamera = Camera.main;
            m_Bar = GetComponent<UIDocument>().rootVisualElement.Q("Container");
            m_Hearts = m_Bar.Children().ToArray(); //new

            SetPosition();

        }

        private void LateUpdate()
        {
            if (TransformToFollow != null)
            {
                SetPosition();
            }
        }

        /// <summary>
        /// Set the position of the UI object
        /// </summary>
        public void SetPosition()
        {
            Vector2 newPosition = RuntimePanelUtils.CameraTransformWorldToPanel(m_Bar.panel, TransformToFollow.position, m_MainCamera);

            m_Bar.transform.position = newPosition.WithNewX(newPosition.x - m_Bar.layout.width / 2);
        }

        public void AnimateBar(bool increaseHealth)
        {

            if (increaseHealth)
            {
                VisualElement nextHeart = m_Hearts.Where(x => !x.visible).FirstOrDefault();
                nextHeart.style.visibility = Visibility.Visible;

            }
            else
            {
                VisualElement nextHeart = m_Hearts.Where(x => x.visible).LastOrDefault();
                nextHeart.style.visibility = Visibility.Hidden;

            }
        }
    }
}
