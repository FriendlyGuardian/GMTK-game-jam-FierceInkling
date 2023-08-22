using UnityEngine;
using Random = UnityEngine.Random;

namespace TarodevController {
    /// <summary>
    /// This is a pretty filthy script. I was just arbitrarily adding to it as I went.
    /// You won't find any programming prowess here.
    /// This is a supplementary script to help with effects and animation. Basically a juice factory.
    /// </summary>
    public class PlayerAnimator : MonoBehaviour {
        [SerializeField] private Animator _anim;
        [SerializeField] private AudioSource _source;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private ParticleSystem _jumpParticles, _launchParticles;
        [SerializeField] private ParticleSystem _moveParticles, _landParticles;
        [SerializeField] private AudioClip[] _footsteps;
        [SerializeField] private float _maxTilt = .1f;
        [SerializeField] private float _tiltSpeed = 1;
        [SerializeField, Range(1f, 3f)] private float _maxIdleSpeed = 2;
        [SerializeField] private float _maxParticleFallSpeed = -40;
        private bool isMoving = false;
        private IPlayerController _player;
        private bool _playerGrounded;
        private ParticleSystem.MinMaxGradient _currentGradient;
        private Vector2 _movement;
        public PlayerController PC;
        private bool groundedOLD = false;

        private void Start()
        {
            _moveParticles.Stop(_moveParticles);
        }
        void Awake() => _player = GetComponentInParent<IPlayerController>();

        void Update()
        {
            particleUpdate();
            if (_player == null) return;
           

            if (Input.GetKey(KeyCode.D))
            {

                //transform.Translate(Vector3.right * Time.deltaTime * sideMoveSpeed, Space.World);

                if (!Input.GetKey(KeyCode.A)) _anim.Play("swim right");

            }
            if (Input.GetKey(KeyCode.A))
            {
                // transform.Translate(Vector3.left * Time.deltaTime * sideMoveSpeed, Space.World);

                if (!Input.GetKey(KeyCode.D)) _anim.Play("swim left");
            }
            if (!Input.GetKey(KeyCode.D) & !Input.GetKey(KeyCode.A)) _anim.Play("idle");



        }
        private void particleUpdate()
        {
            if (!isMoving) {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    isMoving = true;
                    startMoveParticle();
                }
            }
            else
            {
             if (!Input.GetKey(KeyCode.A) & !Input.GetKey(KeyCode.D))
                {
                    isMoving = false;
                    stopMoveParticle();
                }
            }
          if (!PC.Grounded && isMoving)
            {
                isMoving = false;
                stopMoveParticle();
            }
            if (PC.Grounded && !groundedOLD) _landParticles.Play(_landParticles);
            if (!PC.Grounded && groundedOLD) _landParticles.Play(_landParticles);
            groundedOLD = PC.Grounded;

        }
        
        void startMoveParticle()
        {
            _moveParticles.Play(_moveParticles);
        }
        void stopMoveParticle()
        {
            _moveParticles.Stop(_moveParticles);
        }
        
    }
}