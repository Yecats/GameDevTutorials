using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerDetail : MonoBehaviour
    {
        [Header("General Properties")]
        public Collider MyCollider;
        public GameObject SelectedHighlight;

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                //toggle whether the character is selected
                _isSelected = value;
                //toggle the sprite object based on selected state
                SelectedHighlight.SetActive(_isSelected);
            }
        }
        void Awake()
        {
            //get a reference to the collider if not set via the inspector
            if (MyCollider == null)
            {
                MyCollider = GetComponentInChildren<Collider>();
            }
            //Get a reference to the sprite if not set via the inspector
            if (SelectedHighlight == null)
            {
                SelectedHighlight = GetComponentInChildren<SpriteRenderer>(true).gameObject;
            }
        }

        void Start()
        {
            //Add itself to the list of Characters which will be iterated through when detecting selection
            PartyManager.Characters.Add(this);
        }

    }
}
