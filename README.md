# CookAppsTest

# 프로젝트 소개
Cook Apps PxP Studio의 과제를 수행한 프로젝트입니다.

## 개발 버전
- Unity 2022.3.24f1

## 프로그램 설명
- 앱을 실행하면 게임이 실행됩니다.
- 전체적인 게임 진행은 자동으로 이루어지며 플레이어의 조작은 UI 조작 밖에 없습니다.
- 조작이 가능한 UI는 크게 두가지 입니다.
  1. 캐릭터 슬롯 -> 좌측 하단에 존재하며 4개의 각 직업군을 비추는 UI입니다. 해당 UI를 클릭하면 그 캐릭터를 카메라가 따라갑니다.
  2. 상점 버튼 -> 우측 상단에 존재하는 버튼입니다. 누르면 상점 UI가 등장하며 각 캐릭터의 능력치를 구매할 수 있습니다.
     - 구매할 수 있는 능력치는 다음과 같습니다.
     - 회복 포션 : 체력을 모두 회복시켜줍니다.
     - 최대 체력 : 최대 체력을 늘려줍니다. 현재 체력에는 영향을 끼치지 않습니다.
     - 공격력 : 공격력을 높여줍니다.

- 몬스터를 10마리 쓰러뜨리면 기존 몬스터보다 강력한 보스몬스터가 등장합니다.
  - 보스 몬스터가 등장하면 보스를 쓰러뜨리기 전까지 몬스터가 등장하지 않으며 보스 몬스터를 쓰러뜨리면 몬스터가 추가로 등장합니다.
- 플레이어가 모두 사망하게 될 경우, 게임 종료 UI가 등장하며 Restart를 누르면 게임을 재시작하고 Exit를 누르면 앱이 종료됩니다. (5초 동안 조작이 없으면 Restart가 자동으로 실행됩니다.)
- 
## 개발된 기능
※ 문서에 기재된 [필수 구현 요소]의 경우 모두 구현했으며 [가산점 항목]의 경우 스킬 이펙트 구현 항목을 제외한 모든 항목을 수행했음을 알립니다.

- 캐릭터 슬롯 버튼: 좌측 아래에 존재하는 버튼이며 누를 경우 해당 캐릭터를 카메라가 따라가게 되며 이는 시네머신 Virtual Camera의 MoveToTopOfPrioritySubqueue() 함수를 사용하여 구현하였습니다.
  <pre><code>
  public void ChangeCamera()
  {
     virCamera.MoveToTopOfPrioritySubqueue();
     Debug.Log("Camera Change");
  }

  </code></pre>
- A*알고리즘의 길찾기 기능: 2D 게임이라는 특징 이용해 타일맵과 같은 느낌을 주고 싶었습니다. 때문에 A*Algorithm을 응용하여 길찾기 기능을 제작하였으며, 인게임 AI들은 이에 기반하여 목표지점까지 찾아갑니다.
  <pre><code>
 private void SetPath()
 {
 // 타겟이 없으면 새로운 경로를 세팅
 if (_stateMachine.Target == null)
     pathFinding.PathRequestManager.RequestPath(new pathFinding.PathRequest(_stateMachine.Transform.position, pathFinding.Grid.Instance.GetRandPoint(_stateMachine.Transform.position), _stateMachine.GetPath));
 // 타겟이 잇으면 해당 경로로 이동.
 else
     pathFinding.PathRequestManager.RequestPath(new pathFinding.PathRequest(_stateMachine.Transform.position, _stateMachine.Target.transform.position, _stateMachine.GetPath));
 }
  </code></pre>
- 
