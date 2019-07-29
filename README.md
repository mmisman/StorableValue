StorableValue is a set of interfaces that helps you save, load and revert various types of values in a simple way.

## Features

- bool, int, float, string, rect, rectInt, Vector2, Vector2Int, Vector3, Vector3Int, Vector4, Quaternion 형식을 지원합니다.
- PlayerPrefs을 통해 값을 저장하고 불러옵니다.
- PlayerPrefs을 확장하여 다양한 형식(bool, Rect, Vector3 등)의 저장 기능을 지원합니다.
- 필드/프로퍼티로 생성 시, getterFunc와 setterFunc를 정의할 수 있습니다(프로퍼티 생성 시 get, set 메소드와 같은 기능).
- Changed, Saved, Loaded, Reverted 이벤트를 제공합니다.
- Save(), Load(), Revert() 메소드를 제공합니다.
- StorableXXX에서 XXX로 묵시적 형변환이 가능합니다.
- 모든 StorableValue는 유니티 에디터와 시리얼라이즈되기 때문에 인스펙터에서 값을 확인할 수 있습니다.
- 내부 Key, Value 필드는 인스펙터를 통해 언제든 값을 변경할 수 있습니다.
- 내부 DefaultValue 필드는 시리얼라이즈된 플레이 전 초기값을 보여주며 수정할 수 없다. Key, Value 필드는 수정 가능합니다.
- SavedValue는 PlayerPrefs에 저장된 값을 보여주며, 저장되어 있는 경우 파란색, 그렇지 않은 경우 빨간색으로 필드가 표시됩니다.
- SavedValue는 시리얼라이즈되지 않는다. 그냥 PlayerPrefs에 저장된 내용을 보여줄 뿐이다. 에디터 또는 런타임시에 저장값이 바뀔때마다 씬이 더티가 되는 것을 막기 위함이다.
- 초기화는 유니티 시리얼라이즈를 이용하거나 new 연산자를 이용할 수 있습니다.

## 참고하면 좋은 내용

- struct이 아닌 class로 구현되어 있습니다.

## struct이 아닌 class로 구현된 이유

- 생성되는 경우보다 대입/참조하는 경우가 많아 struct보다 class가 퍼포먼스 면에서 유리합니다.
- 하나의 인스턴스를 여러 곳에서 공유해서 사용하므로 값이 아닌 참조가 복사되는 class가 사용하기 편합니다.
- 내부에 참조 형식 필드(키)를 가지고 있어 여러 곳에서 사용할 수록 가비지 컬랙션에 부정적으로 작용합니다.

## StorableUiComponent

StorableValue 자체만으로도 저장 기능이 매우 편리해지지만, 변수를 선언하는 시점에 변수의 저장 여부를 정해야 한다는 부담이 있습니다. 이는 반대로, 저장할 줄 몰랐던 변수가 저장할 필요가 생겼을 때, 변수 선언부의 형식을 변경해야하고, 그 결과 변수가 사용되는 코드의 많은 부분을 함께 수정해야 한다는 걸 의미합니다. 예를 들어, 최초 게임의 개발 시에 게임플레이의 제한 시간을 60초로 설정했으나 난이도가 너무 쉽다고 판단되어 난이도를 조절 가능하도록 저장/불러오기/초기화 기능을 추가하고 싶을 때, 게임플레이의 제한 시간을 StorableValue 형식으로 변경하고 이와 관련된 코드의 모든 부분을 변경해야만 합니다. 이러한 코드의 디자인은 StorableValue를 가지는 클래스가 책임질 필요 없는 필드의 저장 기능까지 클래스에 포함시키고, 그 결과 코드의 변화에 민감하게 반응하게 됩니다. 이는 객체 지향 프로그래밍의 주요 원칙인 단일 책임 원칙과 개방-폐쇄 원칙을 어기는 디자인입니다. 이 문제를 해결하기 위해 필드와 저장 기능을 분리하여 모듈화 한 패턴을 사용할 수 있도록 한 것이 StorableReference입니다.

일반 프로퍼티를 유니티 UI의 SelectableComponent와 연결하여 저장/불러오기/초기화를 가능하게 합니다.

## StorableReference

일반 프로퍼티를 별도의 StorableValue와 연결합니다.