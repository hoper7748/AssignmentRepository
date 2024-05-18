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
- A*알고리즘의 길찾기 기능: 2D 게임이라는 특징 이용해 타일맵과 같은 느낌을 주고 싶었습니다. 그래서 선택한 길찾기 알고리즘이 A*Algorithm이고 이를 실현하기 위해 필드를 규격화 해 줄 Grid.cs를 만들었습니다.
  + 필드의 크기를 지정하면 크기에 비례하여 노드를 규격화 하고 이동할 좌표의 데이터가 필요하면 해당 좌표의 정보를 가져올 수 있게 grid[x, y]배열에 기록합니다. 
  <pre><code>
        void CreateGrid()
        {
            grid = new Node[gridSizeX, gridSizeY];
            Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                    bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));

                    int movementPenalty = 0;

                    // raycast

                    Ray ray = new Ray(worldPoint + Vector3.forward * 50, Vector3.back);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, walkableMask))
                    {
                        walkableRegionsDictionary.TryGetValue(hit.collider.gameObject.layer, out movementPenalty);
                    }
                    if (!walkable)
                        movementPenalty += obstacleProximityPenalty;

                    grid[x, y] = new Node(walkable, worldPoint, x, y, movementPenalty);
                }
            }
            BlurPenaltyMap(3);
        }
  </code></pre>
  + 길을 찾을 때는 PathRequestManager의 RequestPath() 매서드를 호출하여 길을 찾습니다. 길찾기의 경우 A*기반이므로 (+1, 0) (-1, 0)등의 직선 방향은 가중치 10을, (-1, +1) (+1, -1) 등의 대각선 방향은 가중치 14를 주어 목표로 향할때의 가장 적은 가중치를 찾아 노드를 Stack에 기록하고 Pop하여 길을 반환합니다.
  <pre><code>
        public static void RequestPath(PathRequest request)
        {
            ThreadStart threadStart = delegate
            {
                instance.pathfinding.FindPath(request, instance.FinishedProcessingPath);
            };
            threadStart.Invoke();
        }
  </code></pre>
  <pre><code>
        public void FindPath(PathRequest request, Action<PathResult> callback)
        {
            Vector3[] wayPoint = new Vector3[0];
            bool pathSuccess = false;

            Node startNode = grid.NodeFromWorldPoint(request.pathStart);
            Node targetNode = grid.NodeFromWorldPoint(request.pathEnd);

            if (startNode.walkable && targetNode.walkable)
            {
                Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
                HashSet<Node> closeSet = new HashSet<Node>();

                openSet.Add(startNode);

                while (openSet.Count > 0)
                {
                    Node currentNode = openSet.RemoveFirst();

                    closeSet.Add(currentNode);

                    if (currentNode == targetNode)
                    {
                        pathSuccess = true;
                        break;
                    }

                    foreach (Node neighbour in grid.GetNeighbours(currentNode))
                    {
                        if (!neighbour.walkable || closeSet.Contains(neighbour))
                            continue;
                        
                        var checkX = neighbour.gridX;
                        var checkY = neighbour.gridY;

                        if (checkX  >= 100 && checkX < 0 && checkY >= 100 && checkY < 0) continue;

                        // 대각선 이동 시, 해당 이동 항향 ex) -1, -1 위치의 경우 (0, -1), (-1, 0)위치 둘 다 열려있는지 체크 해야함.
                        int gCostDistance = GetDistance(currentNode, neighbour);
                        int newMovementCostToNeighbour = currentNode.gCost + gCostDistance + neighbour.movementPenalty;
                        if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                        {
                            neighbour.gCost = newMovementCostToNeighbour;
                            neighbour.hCost = GetDistance(neighbour, targetNode);
                            neighbour.parent = currentNode;

                            if (!openSet.Contains(neighbour))
                                openSet.Add(neighbour);
                            else
                                openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }
                                                         
            if(pathSuccess)
            {
                wayPoint = RetracePath(startNode, targetNode);
                pathSuccess = wayPoint.Length > 0;
            }
            callback(new PathResult(wayPoint, pathSuccess, request.callback));
        }
  </code></pre>
- 인게임의 모든 캐릭터는 Character Class를 상속받고, Playable 캐릭터와 Monster로 나눈 뒤 하위 클래스를 나눕니다.
  
![Character Dialog](https://github.com/hoper7748/CookAppsTest/assets/50869470/67de987e-cca8-40ff-8759-4877315f0c57)

- StateMachine으로 AI의 State를 바꿔가면서 로직을 수행합니다. 위의 길찾기 로직 또한 State 내부에서 실행됩니다.
- 공격과 스킬의 경우 IdleState가 진행중일 때, 별도의 쿨다운을 갖게 하였고 각 쿨다운이 0이 될 때 해당하는 State로 이동하여 로직을 진행하도록 했습니다.
- 공격과 스킬 쿨다운이 동시에 0가 되었을 경우 스킬을 우선적으로 발동하도록 구현했습니다.

<pre><code>
  public override void OnUpdate()
  {
     base.OnUpdate();
     // 시간경과에 따라 탐색
     _attackCoolDown -= Time.deltaTime;
     _skillCoolDown -= Time.deltaTime;
    ...
  }
</code></pre>

- 아래의 사진을 보면 Flip의 X축 반전을 체크했을 때, 부자연스럽게 반전되는 이슈가 있어 방향을 바꿔즐 때마다 캐릭터 자체의 Local Scale을 -1 / 1로 변환해주는 방식을 채택했습니다.
  덕분에 부드러운 방향 전환이 가능해졌습니다.
![Untitled (4)](https://github.com/hoper7748/AssignmentRepository/assets/50869470/49b54227-e2e1-4e70-9614-22e6009d25e9)

- 좌측부터 Scale 1, -1 / rotate Y 0, 180 / Flip x true , false

<pre><code>
  
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
  Vector3 direction = (_stateMachine.Transform.position - currentPoint).normalized;
  _stateMachine.Transform.localScale = direction.x < 0 ? new Vector3(-1, 1, 1) : Vector3.one;  
  return true;
}
</code></pre>
