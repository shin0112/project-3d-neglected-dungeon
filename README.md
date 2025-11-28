# project-3d-neglected-dungeon

## Scripts

### Manager

> 매니저 Scripts
>
> partial 클래스 & internal 생성자로 `Managers`에서만 초기화 가능하도록 설정

<table style="width: 720px; border-collapse: collapse;">
  <thead>
    <tr>
      <th style="width: 200px; border: 1px solid #ccc; padding: 6px;">Name</th>
      <th style="width: 520px; border: 1px solid #ccc; padding: 6px;">Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/00_Manager/Managers.cs">Managers.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        모든 매니저를 관리하는 클래스
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/00_Manager/DungeonManager.cs">DungeonManager.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        던전 정보 및 흐름을 관리하는 매니저
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/00_Manager/ObjectPoolManager.cs">ObjectPoolManager.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        오브젝트 풀을 관리하는 매니저
      </td>
    </tr>
  </tbody>
</table>

### Common

> 공통으로 사용되는 Scripts

<table style="width: 720px; border-collapse: collapse;">
  <thead>
    <tr>
      <th style="width: 200px; border: 1px solid #ccc; padding: 6px;">Name</th>
      <th style="width: 520px; border: 1px solid #ccc; padding: 6px;">Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/01_Common/Define.cs">Define.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        상수 관리 파일
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/01_Common/Enum.cs">Enum.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        열거형 관리 파일
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/01_Common/IAttackable.cs">IAttackable.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        데미지 입을 수 있는 객체를 묶기 위한 인터페이스
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/01_Common/IPoolable.cs">IPoolable.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        오브젝트 풀에 담길 객체를 위한 인터페이스
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/01_Common/NavigationController.cs">NavigationController.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        AI Nav 컨트롤
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/01_Common/StateMachine.cs">StateMachine.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        상태 머신을 적용한 클래스
      </td>
    </tr>
  </tbody>
</table>

### Player

> 플레이어와 관련된 Scripts

<table style="width: 720px; border-collapse: collapse;">
  <thead>
    <tr>
      <th style="width: 200px; border: 1px solid #ccc; padding: 6px;">Name</th>
      <th style="width: 520px; border: 1px solid #ccc; padding: 6px;">Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/02_Player/Player.cs">Player.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        플레이어 데이터를 컨트롤하기 위한 일종의 컨테이너
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/02_Player/PlayerAnimationData.cs">PlayerAnimationData.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        플레이어 애니메이션에 적용할 데이터
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/02_Player/PlayerAnimationEventRelay.cs">PlayerAnimationEventRelay.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        플레이어 애니메이션 이벤트 전달자
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/02_Player/PlayerCondition.cs">PlayerCondition.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        플레이어 상태를 관리하기 위한 컨테이너
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/02_Player/PlayerController.cs">PlayerController.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        CharacterController를 사용해 플레이어의 이동 및 회전을 관리하는 클래스
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/02_Player/PlayerStateMachine.cs">PlayerStateMachine.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        플레이어 움직임 상태 관리 정보를 저장하는 컨테이너 스크립트
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/02_Player/PlayerWallet.cs">PlayerWallet.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        플레이어 움직임 상태 관리 정보를 저장하는 컨테이너 스크립트
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/02_Player/TargetController.cs">TargetController.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        플레이어가 공격할 타겟을 스캔 및 계산해 지정하는 클래스
      </td>
    </tr>
  </tbody>
</table>

#### State

> 플레이어 상태 관리를 위한 Scripts

<table style="width: 720px; border-collapse: collapse;">
  <thead>
    <tr>
      <th style="width: 200px; border: 1px solid #ccc; padding: 6px;">Name</th>
      <th style="width: 520px; border: 1px solid #ccc; padding: 6px;">Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/02_Player/State/PlayerBaseState.cs">PlayerBaseState.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        플레이어 애니메이션 상태 기본 스크립트
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/02_Player/State/PlayerGroundState.cs">- PlayerGroundState.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        플레이어 애니메이션 Ground SubStateMachine 관리(PlayerBaseState 상속)
      </td>
    </tr>
    <tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/02_Player/State/PlayerIdleState.cs">-- PlayerIdleState.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        플레이어 Idle 애니메이션 상태 (PlayerGroundState 상속)
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/02_Player/State/PlayerChaseState.cs">-- PlayerChaseState.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        플레이어 Move(추적) 애니메이션 상태 (PlayerGroundState 상속)
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/02_Player/State/PlayerAttackState.cs">- PlayerAttackState.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        플레이어 애니메이션 Attack SubStateMachine 관리 및 Attack 애니메이션 상태 (PlayerBaseState 상속)
      </td>
    </tr>
  </tbody>
</table>

## Monster

> 몬스터 Scripts

<table style="width: 720px; border-collapse: collapse;">
  <thead>
    <tr>
      <th style="width: 200px; border: 1px solid #ccc; padding: 6px;">Name</th>
      <th style="width: 520px; border: 1px solid #ccc; padding: 6px;">Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/03_Monster/Monster.cs">Monster.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        몬스터 베이스 클래스
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/03_Monster/MonsterAnimationData.cs">MonsterAnimationData.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        몬스터 애니메이션 컨트롤 시 필요한 Parameter 및 Hash 관리
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/03_Monster/MonsterSpawner.cs">MonsterSpawner.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        몬스터 풀을 관리하는 스포너
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/03_Monster/MonsterStateMachine.cs">MonsterStateMachine.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        몬스터 애니메이션 상태 머신
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/03_Monster/SkeletonWarrior.cs">SkeletonWarrior.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        몬스터: 스켈레톤 전사 (Monster 상속)
      </td>
    </tr>
  </tbody>
</table>

## Dungeon

> 던전 자동 생성을 위한 Scripts

<table style="width: 720px; border-collapse: collapse;">
  <thead>
    <tr>
      <th style="width: 200px; border: 1px solid #ccc; padding: 6px;">Name</th>
      <th style="width: 520px; border: 1px solid #ccc; padding: 6px;">Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/04_Dungeon/MapGenerator.cs">MapGenerator.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        Grid 기반 절차적 던전 생성기
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/04_Dungeon/RoomConnectors.cs">RoomConnectors.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        Room 간 연결을 위한 door position 저장 클래스
      </td>
    </tr>
  </tbody>
</table>

## Item

> 아이템 관련 Scripts

<table style="width: 720px; border-collapse: collapse;">
  <thead>
    <tr>
      <th style="width: 200px; border: 1px solid #ccc; padding: 6px;">Name</th>
      <th style="width: 520px; border: 1px solid #ccc; padding: 6px;">Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/05_Item/ItemSlot.cs">ItemSlot.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        아이템 공용 UI 요소를 관리하는 아이템 슬롯 추상 클래스
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/05_Item/EquipmentSlot.cs">EquipmentSlot.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        장비 타입 정보를 보관하며 장비 슬롯 전용 클릭 로직을 구현하는 클래스 (ItemSlot 상속)
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/05_Item/EquipmentController.cs">EquipmentController.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        EquipmentSlot을 관리하는 장비 슬롯 컨트롤러
      </td>
    </tr>
  </tbody>
</table>

## View

> View Scripts

<table style="width: 720px; border-collapse: collapse;">
  <thead>
    <tr>
      <th style="width: 200px; border: 1px solid #ccc; padding: 6px;">Name</th>
      <th style="width: 520px; border: 1px solid #ccc; padding: 6px;">Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/10_View/CurDungeonView.cs">CurDungeonView.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        현재 던전 상태 정보 View
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/10_View/DungeonButtonView.cs">DungeonButtonView.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        던전 선택(버튼) 창 View
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/10_View/EquipmentItemView.cs">EquipmentItemView.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        장비 아이템 장착 View
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/10_View/HeaderView.cs">HeaderView.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        재화 정보를 담고 있는 헤더 View
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/10_View/ProfileView.cs">ProfileView.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        프로필 View
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/10_View/StatView.cs">StatView.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        현재 스탯 View
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/10_View/ViewInterface.cs">ViewInterface.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        view 용 interface 
      </td>
    </tr>
  </tbody>
</table>

## Presenter

> Presenter Scripts

<table style="width: 720px; border-collapse: collapse;">
  <thead>
    <tr>
      <th style="width: 200px; border: 1px solid #ccc; padding: 6px;">Name</th>
      <th style="width: 520px; border: 1px solid #ccc; padding: 6px;">Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/11_Presenter/CurDungeonPresenter.cs">CurDungeonPresenter.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        현재 던전과 관련된 데이터를 연결하는 Presenter
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/11_Presenter/EquipmentItemPresenter.cs">EquipmentItemPresenter.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        장비 장착 아이템과 관련된 데이터를 연결하는 Presenter
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/11_Presenter/HeaderPresenter.cs">HeaderPresenter.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        UI 상단 재화 뷰(HeaderView)와 관련된 데이터를 연결하는 Presenter
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/11_Presenter/ProfilePresenter.cs">ProfilePresenter.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        ProfileView와 관련 데이터를 연결하는 Presenter
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/11_Presenter/StatPresenter.cs">StatPresenter.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        StatView와 스탯 관련 데이터를 연결하는 Presenter
      </td>
    </tr>
  </tbody>
</table>

## Scriptable Object

> SO Scripts

<table style="width: 720px; border-collapse: collapse;">
  <thead>
    <tr>
      <th style="width: 200px; border: 1px solid #ccc; padding: 6px;">Name</th>
      <th style="width: 520px; border: 1px solid #ccc; padding: 6px;">Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/10_ScriptableObjects/00_Scripts/DungeonData.cs">DungeonData.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        던전에 포함되는 스테이지와 그 스테이지에 소환될 Entity와 관련된 정보<br/>
        던전 - 스테이지 - 소환 몬스터 (일반, 보스) 정보
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/10_ScriptableObjects/00_Scripts/MonsterData.cs">MonsterData.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        몬스터 타입, 스탯 정보
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/10_ScriptableObjects/00_Scripts/ObstacleData.cs">ObstacleData.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        파괴 가능한 장애물 정보
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/10_ScriptableObjects/00_Scripts/PlayerStateData.cs">PlayerStateData.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        플레이어 애니메이션 상태 정보
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/10_ScriptableObjects/00_Scripts/Stat/MonsterStatData.cs">MonsterStatData.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        몬스터 스탯 정보
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/10_ScriptableObjects/00_Scripts/Stat/PlayerStatData.cs">PlayerStatData.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        플레이어 스탯 정보
      </td>
    </tr>
  </tbody>
</table>

## Utils

> Helper Scripts

<table style="width: 720px; border-collapse: collapse;">
  <thead>
    <tr>
      <th style="width: 200px; border: 1px solid #ccc; padding: 6px;">Name</th>
      <th style="width: 520px; border: 1px solid #ccc; padding: 6px;">Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/99_Utils/CustomException.cs">CustomException.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        커스텀 에러 모음
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/99_Utils/Extenstions.cs">Extenstions.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        C# 확장 클래스 모음
      </td>
    </tr>
    <tr>
      <td style="border: 1px solid #ccc; padding: 6px;">
        <a href="./Assets/01_Scripts/99_Utils/Logger.cs">Logger.cs</a>
      </td>
      <td style="border: 1px solid #ccc; padding: 6px;">
        커스텀 로거
      </td>
    </tr>
  </tbody>
</table>

## License

일부 리소스는 외부 라이선스를 포함합니다.
자세한 내용은 [30_Externals/LICENSE_Assets.txt](Assets\30_Externals\ASSETS_LICENSE.txt)를 참고하세요.
