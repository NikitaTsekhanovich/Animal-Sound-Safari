using UnityEngine;

namespace AnimalsData
{
    [CreateAssetMenu(fileName = "AnimalData", menuName = "Animal Data/ Animal")]
    public class AnimalData : ScriptableObject
    {
        [SerializeField] private int _index;
        [SerializeField] private Sprite _spriteIcon;
        [SerializeField] private Sprite _spriteImage;
        [SerializeField] private AudioClip _sound;
        [SerializeField] private string _description;
        [SerializeField] private string _descriptionTitle;
        [SerializeField] private string _name;

        public int Index => _index;
        public Sprite SpriteIcon => _spriteIcon;
        public Sprite SpriteImage => _spriteImage;
        public AudioClip Sound => _sound;
        public string Description => _description;
        public string DescriptionTitle => _descriptionTitle;
        public string Name => _name;
    }
}

