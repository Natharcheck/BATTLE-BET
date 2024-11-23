using System;
using System.Collections.Generic;
using Network;
using UnityEngine;

namespace Characters
{
    public abstract class Character : MonoBehaviour
    {
        public enum Behavior : int
        {
            Idle = 1,
            Move, 
            Attack,
            Ability,
            Dead
        }
        public int ConnectionId { get; set; }
        public int BehaviorsCount { get; private set; }
        public int Angle { get; set; }
        
        public HealthView healthView;
        
        private Dictionary<Type, CharacterBehavior> _behaviorsMap;
        private CharacterBehavior _behaviorCurrent;
        private Animator _animator;
        
        private void Start()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            _animator = GetComponentInChildren<Animator>();
            _behaviorsMap = new Dictionary<Type, CharacterBehavior>();
            
            _behaviorsMap[typeof(CharacterBehaviorIdle)] = new CharacterBehaviorIdle()
            {
                ActionEnter = EnterIdle,
            };
            
            _behaviorsMap[typeof(CharacterBehaviorMove)] = new CharacterBehaviorMove()
            {
                ActionUpdated = Move,
            };
            
            _behaviorsMap[typeof(CharacterBehaviorAttack)] = new CharacterBehaviorAttack()
            {
                ActionEnter = EnterAttack
            };
            
            BehaviorsCount = _behaviorsMap.Count;
            
            NetworkSend.BehaviorPlayer((int)Behavior.Idle);
        }
        
        public virtual void EnterIdle()
        {
            
        }
        
        public virtual void Move()
        {
            
        }
        
        public virtual void EnterAttack()
        {
            
        }
        
        protected CharacterBehavior GetBehavior<T>() where T : CharacterBehavior
        {
            var type = typeof(T);

            if (_behaviorsMap.ContainsKey(type))
                return _behaviorsMap[type];   

            Debug.Log($"Behavior not found {type}");
            return null;
        }

        public void SetBehaviorReceiveId(int behaviorId)
        {
            switch (behaviorId)
            {
                case 1 : SetBehavior(GetBehavior<CharacterBehaviorIdle>()); break;
                case 2 : SetBehavior(GetBehavior<CharacterBehaviorMove>()); break;
                case 3 : SetBehavior(GetBehavior<CharacterBehaviorAttack>()); break;
                case 4 : SetBehavior(GetBehavior<CharacterBehaviorIdle>()); break; //Поменять на абилити
                
                default: SetBehavior(GetBehavior<CharacterBehaviorIdle>()); break;
            }
        }
        
        protected void SetBehavior(CharacterBehavior newBehavior)
        {
            if (_behaviorCurrent != null)
                _behaviorCurrent.Exit();
        
            _behaviorCurrent = newBehavior; 
            _behaviorCurrent.Enter();
        }
        
        private void FixedUpdate()
        {
            if (_behaviorCurrent != null)
                _behaviorCurrent.Updated();
        }
        
        protected void AddBehavior<T>() where T : CharacterBehavior, new()
        {
            var type = typeof(T);
            
            if (_behaviorsMap.Count != 0)
            {
                if (GetBehavior<T>() is T)
                {
                    Debug.LogError($"Duplicate behavior: {type}");
                    return;
                }
            }

            _behaviorsMap[type] = new T()
            {
                
            };
            
            BehaviorsCount = _behaviorsMap.Count;
            
            Debug.Log($"Add behavior: {type}");
        }
    }
}