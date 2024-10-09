using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsData
{
    public class AnimalInfoItem : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private GameObject _animalInfoBlock;
        [SerializeField] private Image _animalImage;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _descriptionTitle;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _imagePlayButton;
        [SerializeField] private Sprite _spriteStartPlayButton;
        [SerializeField] private Sprite _spriteStopPlayButton;
        [SerializeField] private Slider _musicSlider;    
        [SerializeField] private Image _favoriteImageButton;
        [SerializeField] private Sprite _spriteChosenFavoriteButton;
        [SerializeField] private Sprite _spriteNotChosenFavoriteButton;  
        private AudioClip _sound;
        private float _timeSound;
        private bool _isDragging = false;
        private int _indexAnimal;

        private void OnEnable()
        {
            AnimalItem.OnOpenInfoAnimal += InitAnimalInfo;
        }

        private void OnDisable()
        {
            AnimalItem.OnOpenInfoAnimal -= InitAnimalInfo;
        }

        private void InitAnimalInfo(AnimalData animalData)
        {
            _animalInfoBlock.SetActive(true);
            _animalImage.sprite = animalData.SpriteImage;
            _title.text = animalData.Name;
            _descriptionTitle.text = animalData.DescriptionTitle;
            _description.text = animalData.Description;
            _sound = animalData.Sound;
            _indexAnimal = animalData.Index;
            UpdateFavoriteState();
        }

        public void InitSound()
        {
            if (_audioSource.clip == null)
            {
                _audioSource.clip = _sound;
                _musicSlider.maxValue = _audioSource.clip.length;
                _musicSlider.onValueChanged.AddListener(OnSliderValueChanged);
                PlaySound();
            }
            else
            {
                if (!_audioSource.isPlaying)
                {
                    PlaySound();
                    
                }
                else
                {
                    StopSound();
                }
            }
        }

        public void ResetSound()
        {
            _audioSource.Stop();
            _timeSound = 0;
            _audioSource.clip = null;
            _imagePlayButton.sprite = _spriteStartPlayButton;
            _musicSlider.value = 0;
        }

        private void PlaySound()
        {
            _audioSource.time = _timeSound;
            _audioSource.Play();
            _imagePlayButton.sprite = _spriteStopPlayButton;
        }

        private void StopSound()
        {
            _timeSound = _audioSource.time;
            _audioSource.Stop();
            _imagePlayButton.sprite = _spriteStartPlayButton;
        }

        private void Update()
        {
            if (!_isDragging && _audioSource.isPlaying)
            {
                _musicSlider.value = _audioSource.time;
            }
            if (_audioSource.clip?.length <= _audioSource.time)
            {
                _imagePlayButton.sprite = _spriteStartPlayButton;
            }
        }

        public void OnSliderValueChanged(float value)
        {
            if (_isDragging)
            {
                _audioSource.time = value;
            }
        }

        public void OnSliderDragStart()
        {
            _isDragging = true;
        }

        public void OnSliderDragEnd()
        {
            _isDragging = false;

            if (_audioSource.clip?.length > _musicSlider.value)
                _audioSource.time = _musicSlider.value;
        }

        private void UpdateFavoriteState()
        {
            var isChosenAnimal = PlayerPrefs.GetInt($"{AnimalDataKeys.IsFavoriteAnimalKey}{_indexAnimal}");

            if (isChosenAnimal == (int)TypeChosenItem.IsNotChosen)
            {
                _favoriteImageButton.sprite = _spriteNotChosenFavoriteButton;
            }
            else if (isChosenAnimal == (int)TypeChosenItem.IsChosen)
            {
                _favoriteImageButton.sprite = _spriteChosenFavoriteButton;
            }
        }

        public void SetFavoriteAnimal()
        {
            var isChosenAnimal = PlayerPrefs.GetInt($"{AnimalDataKeys.IsFavoriteAnimalKey}{_indexAnimal}");

            if (isChosenAnimal == (int)TypeChosenItem.IsNotChosen)
            {
                _favoriteImageButton.sprite = _spriteChosenFavoriteButton;
                PlayerPrefs.SetInt($"{AnimalDataKeys.IsFavoriteAnimalKey}{_indexAnimal}", (int)TypeChosenItem.IsChosen);
            }
            else if (isChosenAnimal == (int)TypeChosenItem.IsChosen)
            {
                _favoriteImageButton.sprite = _spriteNotChosenFavoriteButton;
                PlayerPrefs.SetInt($"{AnimalDataKeys.IsFavoriteAnimalKey}{_indexAnimal}", (int)TypeChosenItem.IsNotChosen);
            }
        }
    }
}

