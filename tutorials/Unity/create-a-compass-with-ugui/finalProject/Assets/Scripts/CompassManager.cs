using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.WUG.Scripts
{
    public class CompassManager : MonoBehaviour
    {
        public static CompassManager Instance;

        public RawImage CompassImage;
        public RectTransform CompassObjectivesParent;
        public GameObject CompassObjectivePrefab;
        private readonly List<CompassObjective> _currentObjectives = new List<CompassObjective>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private IEnumerator Start()
        {
            WaitForSeconds updateDelay = new WaitForSeconds(1);

            while (enabled)
            {
                SortCompassObjectives();
                yield return updateDelay;
            }
        }

        private void SortCompassObjectives()
        {
            if (PlayerController.Instance == null)
            { return; }

            CompassObjective[] orderedObjectives = _currentObjectives.Where(o => o.WorldGameObject != null).OrderByDescending(o => Vector3.Distance(PlayerController.Instance.transform.position, o.WorldGameObject.position)).ToArray();
            for (int i = 0; i < orderedObjectives.Length; i++)
            {
                orderedObjectives[i].UpdateUiIndex(i);
            }
        }

        /// <summary>
        /// Called at end of frame -> update compass orientation and all objective positions
        /// </summary>
        private void LateUpdate() => UpdateCompassHeading();

        /// <summary>
        /// Updates the orientation of the compass heading image
        /// </summary>
        private void UpdateCompassHeading()
        {
            if (PlayerController.Instance == null)
            { return; }

            //get compass offset position
            Vector2 compassUvPosition = Vector2.right * (PlayerController.Instance.transform.rotation.eulerAngles.y / 360);

            //set if flipped, interpolate if not
            CompassImage.uvRect = new Rect(compassUvPosition, Vector2.one);
        }

        /// <summary>
        /// Call with game object to create compass objective
        /// </summary>
        public void AddObjectiveForObject(GameObject compassObjectiveGameObject, Color color, Sprite sprite) =>
            _currentObjectives.Add(Instantiate(CompassObjectivePrefab, CompassObjectivesParent, false)
                .GetComponent<CompassObjective>().Configure(compassObjectiveGameObject, color, sprite));

        public void RemoveCompassObjective(CompassObjective compassObjective)
        {
            _currentObjectives.Remove(compassObjective);
            Destroy(compassObjective.gameObject);
        }
    }
}
