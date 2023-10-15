using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations;

namespace _Tasks._3D_Assignment.Scripts
{
    // " It ain't much, but it's honest work" - Random Guy 
    public class AnimationManager : MonoBehaviour
    {
        private Animation _animation;
        public Animation Animation
        {
            get { return _animation; }
            set { _animation = value; }
        }
        private Vector3 currentPos;

        private AnimationClip move;
        private AnimationCurve position;
        private void Start()
        {
            move = new AnimationClip();
            currentPos = transform.position;
            _animation = gameObject.AddComponent<Animation>();
            Init_Animation_walking();
            Init_Animation_Fall();
            Init_Animation_StandUp();
            Init_Animation_Run();
            StartCoroutine(Animation_Controller());
        }
        private void Update()
        {
            
        }

        public void Playwalk()
        {
            _animation.Play("walk");
            
        }
        public void Play_Fall()
        {
            _animation.Play("fall");
        }
        public void Play_Stand_Up()
        {
            _animation.Play("standup");
        }
        public void Play_Run()
        {
         
            _animation.Play("run");
         
        }
        
        private void Init_Animation_walking()
        {
            
            // Create new animation clip for robot model 
            AnimationClip walk = new AnimationClip();
            AnimationCurve full = Init_Animation(
                3, 
                new float [3] { 0f/2, 2f/2, 4f/2 }, 
                new float[3] { 0f, -5f, 0f });
            walk.SetCurve("",typeof(Transform),"localEulerAngles.z",full);
            
            AnimationCurve lefthand = Init_Animation(3, 
                new float [3] { 0f/2, 2f/2, 4f/2 }, 
                new float[3] { -10f, 0f, -10f });
            walk.SetCurve("UpperBody/LShoulder/LShoulderJoint",typeof(Transform),"localEulerAngles.y",lefthand);
            
            AnimationCurve righthand = Init_Animation(
                3, 
                new float [3] { 1f/2, 3f/2, 5f/2 }, 
                new float[3] { -10f, 0f, -10f });
            walk.SetCurve("UpperBody/RShoulder/RShoulderJoint",typeof(Transform),"localEulerAngles.y",righthand);
            
            
            AnimationCurve leftelbow = Init_Animation(
                3, 
                new float [3] { 1f/2, 3f/2, 5f/2 }, 
                new float[3] { 0f, -90f, 0f });
            walk.SetCurve("UpperBody/LShoulder/LShoulderJoint/LUpperArm/LElbowJoint",typeof(Transform),"localEulerAngles.y",leftelbow);

            AnimationCurve rightelbow = Init_Animation(
                3,
                new float[3] { 1f / 3, 3f / 3, 5f / 3 },
                new float[3] { 0f, -90f, 0f }
            );
            walk.SetCurve("UpperBody/RShoulder/RShoulderJoint/RUpperArm/RElbowJoint",typeof(Transform),"localEulerAngles.y",rightelbow);

            AnimationCurve lefthighJoint = Init_Animation(
                3,
                new float[3] { 0f / 2, 2f / 2, 4f / 2 },
                new float[3] { -180f, -200f, -180f }
                );
            walk.SetCurve("LowerBody/LThighJoint",typeof(Transform),"localEulerAngles.y",lefthighJoint);

            AnimationCurve rightthighJoint = Init_Animation(
                3,
                new float[3] { 1f / 2, 3f / 2, 5f / 2 },
                new float[3] { -180f, -200f, -180f }
                );
            walk.SetCurve("LowerBody/RThighJoint", typeof(Transform), "localEulerAngles.y",rightthighJoint);
            
            AnimationCurve rightleg = Init_Animation(
                3,
                new float[3] { 0f / 2, 2f / 2, 4f / 2 },
                new float[3] { 0f, 40f, 0f }
            );
            walk.SetCurve("LowerBody/RThighJoint/RightLeg", typeof(Transform), "localEulerAngles.y",rightleg);
            
            AnimationCurve leftleg = Init_Animation(
                3,
                new float[3] { 1f / 2, 3f / 2, 5f / 2 },
                new float[3] { 0f, 40f, 0f }
            );
            walk.SetCurve("LowerBody/LThighJoint/LeftLeg", typeof(Transform), "localEulerAngles.y",leftleg);
            
            
            // set up and name the animation walk
            walk.legacy = true;
            walk.wrapMode = WrapMode.Loop;

            _animation.AddClip(walk, "walk");
        }
        
        private void Init_Animation_Run()
        {
               
            // Create new animation clip for robot model 
            AnimationClip run = new AnimationClip();
            
            // AnimationCurve position = Init_Animation(
            //     2,
            //     new float[2] { 0f / 2, 5f / 2 },
            //     new float[2] { transform.position.x, transform.position.x + 8}
            // );
            // run.SetCurve("",typeof(Transform),"localPosition.x",position);
            
            // init the body part animation curve
            AnimationCurve full = Init_Animation(
                3, 
                new float [3] { 0f/2, 2f/2, 4f/2 }, 
                new float[3] { 0f, -15f, 0f });
            run.SetCurve("",typeof(Transform),"localEulerAngles.z",full);
            
            AnimationCurve lefthand = Init_Animation(3, 
                new float [3] { 0f/2, 2f/2, 4f/2 }, 
                new float[3] { -25f, 0f, -25f });
            run.SetCurve("UpperBody/LShoulder/LShoulderJoint",typeof(Transform),"localEulerAngles.y",lefthand);
            
            AnimationCurve righthand = Init_Animation(
                3, 
                new float [3] { 1f/2, 3f/2, 5f/2 }, 
                new float[3] { -25f, 0f, -25f });
            run.SetCurve("UpperBody/RShoulder/RShoulderJoint",typeof(Transform),"localEulerAngles.y",righthand);
            
            
            AnimationCurve leftelbow = Init_Animation(
                3, 
                new float [3] { 1f/2, 3f/2, 5f/2 }, 
                new float[3] { 0f, -100f, 0f });
            run.SetCurve("UpperBody/LShoulder/LShoulderJoint/LUpperArm/LElbowJoint",typeof(Transform),"localEulerAngles.y",leftelbow);

            AnimationCurve rightelbow = Init_Animation(
                3,
                new float[3] { 1f / 3, 3f / 3, 5f / 3 },
                new float[3] { 0f, -100f, 0f }
            );
            run.SetCurve("UpperBody/RShoulder/RShoulderJoint/RUpperArm/RElbowJoint",typeof(Transform),"localEulerAngles.y",rightelbow);

            AnimationCurve lefthighJoint = Init_Animation(
                3,
                new float[3] { 0f / 2, 2f / 2, 4f / 2 },
                new float[3] { -180f, -225f, -180f }
                );
            run.SetCurve("LowerBody/LThighJoint",typeof(Transform),"localEulerAngles.y",lefthighJoint);

            AnimationCurve rightthighJoint = Init_Animation(
                3,
                new float[3] { 1f / 2, 3f / 2, 5f / 2 },
                new float[3] { -180f, -225f, -180f }
                );
            run.SetCurve("LowerBody/RThighJoint", typeof(Transform), "localEulerAngles.y",rightthighJoint);
            
            AnimationCurve rightleg = Init_Animation(
                3,
                new float[3] { 0f / 2, 2f / 2, 4f / 2 },
                new float[3] { 0f, 41f, 0f }
            );
            run.SetCurve("LowerBody/RThighJoint/RightLeg", typeof(Transform), "localEulerAngles.y",rightleg);
            
            AnimationCurve leftleg = Init_Animation(
                3,
                new float[3] { 1f / 2, 3f / 2, 5f / 2 },
                new float[3] { 0f, 41f, 0f }
            );
            run.SetCurve("LowerBody/LThighJoint/LeftLeg", typeof(Transform), "localEulerAngles.y",leftleg);
            
            
            // set up and name the animation run
            run.legacy = true;
            run.wrapMode = WrapMode.Loop;
            _animation.AddClip(run,"run");
        }

        public void Init_Animation_Fall()
        {
            // Create new animation clip for robot model 
            AnimationClip fall = new AnimationClip();

            // AnimationCurve position = Init_Animation(
            //     2,
            //     new float[2] { 0f / 2, 5f / 2 },
            //     new float[2] { transform.position.y, transform.position.y - 2}
            // );
            // fall.SetCurve("",typeof(Transform),"localPosition.y",position);
            
            // init the body part animation curve
            AnimationCurve full = Init_Animation(
                2, 
                new float [2] { 0f/2, 8f/2 }, 
                new float[2] { 0f, -90f});
            fall.SetCurve("",typeof(Transform),"localEulerAngles.z",full);
            
            AnimationCurve lefthand = Init_Animation(
                2, 
                new float [2] { 0f/2, 8f/2 }, 
                new float[2] { -10f, -160f});
            fall.SetCurve("UpperBody/LShoulder/LShoulderJoint",typeof(Transform),"localEulerAngles.y",lefthand);
            
            AnimationCurve righthand = Init_Animation(
                2, 
                new float [2] { 0f/2,8f/2 }, 
                new float[2] { -10f, -160f});
            fall.SetCurve("UpperBody/RShoulder/RShoulderJoint",typeof(Transform),"localEulerAngles.y",righthand);
            
            
            AnimationCurve leftelbow = Init_Animation(
                2, 
                new float [2] { 1f/2, 8f/2}, 
                new float[2] { 0f, 10f });
            fall.SetCurve("UpperBody/LShoulder/LShoulderJoint/LUpperArm/LElbowJoint",typeof(Transform),"localEulerAngles.y",leftelbow);

            AnimationCurve rightelbow = Init_Animation(
                2,
                new float[2] { 1f / 2, 8f / 2 },
                new float[2] { 0f, 10f }
            );
            fall.SetCurve("UpperBody/RShoulder/RShoulderJoint/RUpperArm/RElbowJoint",typeof(Transform),"localEulerAngles.y",rightelbow);

            AnimationCurve rightleg = Init_Animation(
                2,
                new float[2] { 0f / 2, 8f / 2 },
                new float[2] { 0f, 40f }
            );
            fall.SetCurve("LowerBody/RThighJoint/RightLeg", typeof(Transform), "localEulerAngles.y",rightleg);
            
            AnimationCurve leftleg = Init_Animation(
                2,
                new float[2] { 1f / 2, 8f / 2},
                new float[2] { 0f, 40f }
            );
            fall.SetCurve("LowerBody/LThighJoint/LeftLeg", typeof(Transform), "localEulerAngles.y",leftleg);
            
            
            // set up and name the animation fall
            fall.legacy = true;
            
            _animation.AddClip(fall,"fall");
        }

        public void Init_Animation_StandUp()
        {
             // Create new animation clip for robot model 
            AnimationClip standup = new AnimationClip();

            // AnimationCurve position = Init_Animation(
            //     2,
            //     new float[2] { 0f / 2, 5f / 2 },
            //     new float[2] { transform.position.y, transform.position.y +2}
            // );
            // standup.SetCurve("",typeof(Transform),"localPosition.y",position);
            
            // init the body part animation curve
            AnimationCurve full = Init_Animation(
                2, 
                new float [2] { 0f/2, 8f/2 }, 
                new float[2] { -90f, 0f});
            standup.SetCurve("",typeof(Transform),"localEulerAngles.z",full);
            
            AnimationCurve lefthand = Init_Animation(
                2, 
                new float [2] { 0f/2, 8f/2 }, 
                new float[2] { -160f, -10f});
            standup.SetCurve("UpperBody/LShoulder/LShoulderJoint",typeof(Transform),"localEulerAngles.y",lefthand);
            
            AnimationCurve righthand = Init_Animation(
                2, 
                new float [2] { 0f/2,8f/2 }, 
                new float[2] { -160f, -10f});
            standup.SetCurve("UpperBody/RShoulder/RShoulderJoint",typeof(Transform),"localEulerAngles.y",righthand);
            
            
            AnimationCurve leftelbow = Init_Animation(
                2, 
                new float [2] { 1f/2, 8f/2}, 
                new float[2] { 10f, 0f });
            standup.SetCurve("UpperBody/LShoulder/LShoulderJoint/LUpperArm/LElbowJoint",typeof(Transform),"localEulerAngles.y",leftelbow);

            AnimationCurve rightelbow = Init_Animation(
                2,
                new float[2] { 1f / 2, 8f / 2 },
                new float[2] { 10f, 0f }
            );
            standup.SetCurve("UpperBody/RShoulder/RShoulderJoint/RUpperArm/RElbowJoint",typeof(Transform),"localEulerAngles.y",rightelbow);

            AnimationCurve rightleg = Init_Animation(
                2,
                new float[2] { 0f / 2, 8f / 2 },
                new float[2] { 40f, 0f }
            );
            standup.SetCurve("LowerBody/RThighJoint/RightLeg", typeof(Transform), "localEulerAngles.y",rightleg);
            
            AnimationCurve leftleg = Init_Animation(
                2,
                new float[2] { 1f / 2, 8f / 2},
                new float[2] { 40f, 0f }
            );
            standup.SetCurve("LowerBody/LThighJoint/LeftLeg", typeof(Transform), "localEulerAngles.y",leftleg);
            
            
            // set up and name the animation standup
            standup.legacy = true;
            _animation.AddClip(standup,"standup");
        }

        public AnimationCurve Init_Animation( int numberofKey, float[] time, float[] value )
        {
    
            AnimationCurve animationCurve = new AnimationCurve();
            for (int i = 0; i < numberofKey; i++)
            {
                animationCurve.AddKey(new Keyframe( time[i],value[i]));
            }
            return animationCurve;
        }
        public IEnumerator Animation_Controller()
        {
            yield return new WaitForSeconds(0.5f);
            Playwalk();
            
            yield return new WaitForSeconds(10f);
            Play_Fall();
            
            yield return new WaitForSeconds(6f);
            Play_Stand_Up();
            
            yield return new WaitForSeconds(5f);
            Play_Run();
            
        }

        public void MoveRobot(float value)
        {
       
            
        }
        
    }
}