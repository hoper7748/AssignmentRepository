using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookAppsPxPAssignment.Character.States
{
    public class MoveState : State
    {
        // ����� ������ ��� �߰���
        // �ƴ� ��� Ž���� �Ѵ�.
        public MoveState(StateMachine _stateMachine) : base(_stateMachine)
        {
            
        }

        public override void OnEnter()
        {
            base.OnEnter();
            SetPath();
            _stateMachine.SetAnimationBoolean("Walk");
        }

        public override void OnExit()
        {
            base.OnExit();
            _stateMachine.SetAnimationBoolean("Walk", false);

        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (_stateMachine.Path != null && _stateMachine.targetIndex < _stateMachine.Path.Length)
            {
                if (_stateMachine.isChasing)
                {
                    Chasing();
                }
                else if (!_stateMachine.isChasing)
                {
                    MoveingPoint();
                }
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
                Debug.LogWarning($"Not Have A Path");
                //Debug.LogWarning($"Not Have A Path / path: {_stateMachine.Path} / length: {_stateMachine.targetIndex} / {_stateMachine.Path.Length}");
                return;
            }
        }

        private void SetPath()
        {
            // Ÿ���� ������ ���ο� ��θ� ����
            if (_stateMachine.Target == null)
                pathFinding.PathRequestManager.RequestPath(new pathFinding.PathRequest(_stateMachine.Transform.position, pathFinding.Grid.Instance.GetRandPoint(_stateMachine.Transform.position), _stateMachine.GetPath));
            // Ÿ���� ������ �ش� ��η� �̵�.
            else
                pathFinding.PathRequestManager.RequestPath(new pathFinding.PathRequest(_stateMachine.Transform.position, _stateMachine.Target.transform.position, _stateMachine.GetPath));
        }

        public void Chasing()
        {
            // ���� ���� ������ ���� �� ���� �̵�.
            float distance = Vector3.Distance(_stateMachine.Transform.position, _stateMachine.Target.transform.position);
            if(distance < _stateMachine.Character.AttackRange
                || _stateMachine.targetIndex >= _stateMachine.Path?.Length)
            {
                // ���� ���� ����
                _stateMachine.ChangeState(_stateMachine.AttackState);

            }
            if (FollowPath())
            {

            }
        }

        private void MoveingPoint()
        {
            if (FollowPath())
            {
                // �̵� �� ���� ã�Ҵ°��� ���� �ǹ�
                _stateMachine.SearchEnemy();
                if (_stateMachine.Target != null)
                    _stateMachine.ChangeState(_stateMachine.IdleState);
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
                return;
            }
        }

        private bool FollowPath()
        {
            Vector3 currentPoint = _stateMachine.Path[_stateMachine.targetIndex];
            if (Mathf.Abs(Vector2.Distance(_stateMachine.Transform.position, currentPoint)) < 0.1f)
            {
                _stateMachine.targetIndex++;
                if (_stateMachine.targetIndex >= _stateMachine.Path.Length)
                    return false;

                currentPoint = _stateMachine.Path[_stateMachine.targetIndex];
            }
            _stateMachine.Transform.position = Vector3.MoveTowards(_stateMachine.Transform.position, currentPoint, 2f * Time.deltaTime);

            // �����̴� ���� �ٶ󺸱�
            Vector3 direction = (_stateMachine.Transform.position - currentPoint).normalized;

            _stateMachine.Transform.localScale = direction.x < 0 ? new Vector3(-1, 1, 1) : Vector3.one;  
                
            return true;
        }

    }

}