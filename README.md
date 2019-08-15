# StorableValue

StorableValue helps you easily save, load and revert various types of values.

## Installation

Find the `Packages/manifest.json` file in your project and add the following line inside of the `depencencies` block:

```json
"com.mmisman.storable-value": "https://github.com/mmisman/StorableValue.git"
```

Done!

### Requirements

- Unity 2019.1 or later
- Git

## Features

유니티 프로젝트를 제작하다보면, 런타임 시 초기 상태나 특정 시점으로 돌아갈 수 있는 변수를 사용해야 하는 경우가 흔히 있습니다. 이때는 보통 저장, 불러오기, 초기화 기능을 변수와 별도로 구현하게 되는데, 이럴 경우 관리가 어려울 뿐더러 귀찮습니다. StorableValue는 변수와 그 변수의 저장, 불러오기, 초기화 등과 관련한 유용한 기능을 하나의 클래스에 모아 사용하기 쉬운 인터패이스를 제공합니다.

### Save and Load

StorableValue는 유니티의 PlayerPrefs를 이용하여 다양한 형식의 값을 저장하고 불러옵니다. 내부적으로 int, float, string 밖에 지원하지 않는 PlayerPrefs를 확장하여 구현하였지만, 사용자는 이와 관련한 귀찮은 일을 신경쓰지 않아도 됩니다. 저장, 불러오기 기능을 사용하고 싶다면, 원하는 시점에 `Save()`, `Load()` 메소드를 호출하기만 하면 됩니다. 지원하는 형식은 bool, int, float, string, Rect, RectInt, Vector2, Vector2Int, Vector3, Vector3Int, Vector4, Quaternion으로, Storable 뒤에 형식명을 Pascal case로 붙여주면 됩니다.

아래 예제는 저장 가능한 float 형식의 volume 인스턴스를 선언하고, 플레이가 시작되면 이미 저장된 값을 한번 불러옵니다. 만약 이전에 저장된 값이 없다면 변화 없이 현재 값이 그대로 유지됩니다. 플레이 중 위 화살표가 눌리면 볼륨을 키우고 저장, 아래 화살표가 눌리면 볼륨을 줄이고 저장합니다. `SavedValue` 프로퍼티로 현재 PlayerPrefs에 저장된 값을 확인할 수 있습니다.

```c#
using UnityEngine;
using Mmisman.StorableValue; // StorableValue를 사용하기 위한 네임스페이스

public class VolumeController : MonoBehaviour
{
  StorableFloat volume = new StorableFloat("volume", .7f);

  void Start()
  {
    volume.Load();
  }

  void Update()
  {
    if (Input.GetKeyUp(KeyCode.UpArrow))
    {
      volume.Value += .1f;
      volume.Save();
    }
    else if (Input.GetKeyUp(KeyCode.DownArrow))
    {
      volume.Value -= .1f;
      volume.Save();
    }
    else if (Input.GetKeyUp(KeyCode.Space))
    {
      Debug.Log(string.Format("Saved value: {0}", volume.SavedValue));
    }
  }
}
```

위 예제에서 저장 가능한 float 형식의 volume 인스턴스를 선언할 때, 키-값 쌍을 제공하여 정의하였습니다. 저장/불러오기 기능을 위해서 키 인자는 필수로 제공되어야 하지만, 값 인자는 생략될 수 있습니다. 그러면 해당 형식의 기본값으로 초기화됩니다. 참고로 키 인자는 비어있거나 null일 수 없습니다.

```c#
StorableFloat volume = new StorableFloat("volume"); // 기본값인 0f로 초기화
```

### Revert

StorableValue는 값의 초기화 기능도 제공합니다. `Revert()` 메소드를 호출하면 빌드 전(에디터에서는 플레이 모드 전)의 세팅값으로 돌아갑니다. `DefaultValue` 프로퍼티로 초기값을 확인할 수 있습니다.

```c#
void Update()
{
  if (Input.GetKeyUp(KeyCode.R))
  {
    volume.Revert();	// volume.Value를 초기값인 .7f로 되돌립니다.
  }
  else if (Input.GetKeyUp(KeyCode.Space))
  {
    Debug.Log(string.Format("Default value: {0}", volume.DefaultValue));
  }
}
```

### Unity Serialization

모든 StorableValue는 필드의 `public` 선언 또는 `SerializeField` 어트리뷰트를 통해 유니티 에디터와 직렬화(Serialization)할 수 있습니다. 즉, 유니티 에디터를 통해 StorableValue를 관리할 수 있습니다.

```c#
public StorableFloat bgmVolume; // 유니티 직렬화에 의해 자동으로 인스턴스 생성
[SerializeField] StorableFloat sfxVolume = new StorableFloat("sfxVolum", .7f);
```

직렬화된 변수를 초기화하지 않으면, 유니티 직렬화에 의해 자동으로 인스턴스가 생성됩니다. 이때 키 필드는 변수명과 동일하게 설정되는데, 왠만하면 사용 의도에 맞게 인스펙터에서 변경해서 사용하길 추천합니다. 인스펙터에서 키 필드은 비어있을 수 없습니다. 만약 그렇다면, 유니티 에디터에 의해 변수명과 동일한 값으로 자동 설정됩니다.

[![img](./Docs/VolumeController.png?raw=true)](./Docs/VolumeController.png)

직렬화된 StorableValue는 에디터에서 키(K), 값(V), 초기값(D), 저장값(S) 순으로 보여집니다. 이중 키와 값 필드는 에디터를 통해 수정 가능하지만, 초기값과 저장값 필드는 수정 불가능합니다. 이는 의도된 것으로, 사용자는 키와 값 필드만 관리하고, 나머지 필드는 값을 확인하는 용도입니다.

#### 초기값 필드

초기값 필드는 에디터의 에딧 모드에서는 값 필드와 자동으로 동기화되지만, 플레이 모드에서는 플레이 직전의 값이 유지됩니다. 같은 방식으로, 빌드 전의 최종 초기값이 빌드 후의 초기값으로 유지됩니다. 초기값은 플레이 중 또는 빌드 후 변경할 수 없습니다. 이러한 사용 시나리오는 '초기값'이라는 단어가 의미하는 바를 있는 그대로 반영합니다. 참고로 값을 초기화하기 위해서는 스크립트를 통해 `Revert()` 메소드를 호출하세요.

#### 저장값 필드

저장값 필드는 수정 불가능하며 PlayerPrefs에 저장된 값을 직접 보여줍니다. 즉, 저장값 필드는 읽기 전용으로, 유니티의 직렬화를 이용하지 않습니다. 덕분에 플레이 중 값을 저장하여 저장값 필드가 갱신된 경우, 플레이가 종료된 후에도 그 값이 필드에 유지되어 에디터가 의도치 않게 더티(dirty) 표시가 되는 상황을 방지합니다. 저장값 필드는 PlayerPrefs에 저장되어 있는 경우 푸른색으로, 그렇지 않은 경우 붉은색으로 표시됩니다. 참고로 값을 저장하고 불러오기 위해서는 스크립트를 통해 `Save()`, `Load()` 메소드를 호출하세요.

### Value Property

위에서 따로 언급하진 않았지만, StorableValue의 값에 접근하기 위해서는 `Value` 프로퍼티를 사용합니다.

```c#
bgmVolume.Value = .7f;
```

값을 얻을 때는 묵시적 형변환에 의해 `Value` 프로퍼티를 생략할 수 있습니다.

```c#
Debug.Log(bgmVolume); // Same as "Debug.Log(bgmVolume.Value);"
```

#### Getter/Setter Functions

일반적인 프로퍼티를 선언할 때, get/set 접근자를 사용하여 값을 읽거나 쓰는 메커니즘을 정의했던 것처럼, StorableValue를 선언할 때도, 값을 읽거나 쓰는 메커니즘을 정의할 수 있습니다. 생성자의 2, 3번째 인자로 각각 get/set 델리게이트(delegate)를 정의하여 사용합니다. 이 인자들은 null일 수 없습니다. 아래 예제는 voiceVolume의 값을 쓸 때, 0과 1 사이로 고정합니다.

```c#
StorableFloat voiceVolume = new StorableFloat("voiceVolume", .7f, 
                                              v => v, // Getter function
                                              v => Mathf.Clamp01(v) // Setter function
                                             );
```

### Events

StorableValue는 값의 변화에 따른 이벤트가 정의되어 있습니다.

#### Changed

값이 변할 때마다 불립니다.

```c#
voiceVolume.Changed += v => Debug.Log(string.Format("Value changed to {0}", v));
```

#### Saved/Loaded/Reverted

각각 `Save()`/`Load()`/`Revert()` 메소드가 호출될 때 불립니다.

```c#
voiceVolume.Saved += v => Debug.Log(string.Format("Value saved to {0}", v));
voiceVolume.Loaded += v => Debug.Log(string.Format("Value loaded to {0}", v));
voiceVolume.Reverted += v => Debug.Log(string.Format("Value reverted to {0}", v));
```

### Custom StorableEnum

개인적으로 정의한 enum에 저장 기능을 추가하고 싶다면, 아래 예제처럼 `StorableEnum<T>`를 상속하는 클래스를 정의하여 이용하면 됩니다. 유니티 인스펙터를 이용하고 싶다면 `StorableEnumDrawer<T>` 클래스를 상속하는 클래스도 정의하세요.

```c#
#if UNITY_EDITOR
using Mmisman.StorableValue.Editor;
#endif

public enum ScreenOrientation
{
	Landscape,
	LandscapeUpsideDown,
	PortraitLeft,
	PortraitRight
}

public class StorableScreenOrientation : StorableEnum<ScreenOrientation> { }

#if UNITY_EDITOR
[UnityEditor.CustomPropertyDrawer(typeof(StorableScreenOrientation))]
public class StorableScreenOrientationDrawer : StorableEnumDrawer<ScreenOrientation> { }
#endif
```

## License

This library is under the MIT License.