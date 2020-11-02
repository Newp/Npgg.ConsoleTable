# Npgg.ConsoleTable
https://www.nuget.org/packages/Npgg.ConsoleTable/


### 특징
1. Reflection 을 이용하여 자동으로 테이블을 그립니다.
2. 한글/영문/특수문자에서도 깨지지 않고 똑바로 그립니다.
3. 패턴매칭이나 대리자를 사용하여 원하는 Row에 원하는 색을 지정할 수 있습니다.


### Exmaple

```csharp

var items = new[]
{
    new Item(){ Name= "Leoric's Crown", Rarity = Rarity.Normal, Slot ="Helm"},
    new Item(){ Name= "Thunderfury", Rarity = Rarity.Unique, Slot ="One Handed Weapon"},
    new Item(){ Name= "할배검 the grandfather", Rarity = Rarity.Legendary, Slot ="Two Handed Weapon"},
    new Item(){ Name= "WINDFORCE", Rarity = Rarity.Magic, Slot ="양손무기"},
};

ConsoleTable.Write(items, item => item.Rarity switch
{
    Rarity.Magic => ConsoleColor.DarkCyan,
    Rarity.Unique => ConsoleColor.DarkMagenta,
    Rarity.Legendary => ConsoleColor.DarkYellow,
    _ => ConsoleColor.White
});

```

### 결과
![image](https://user-images.githubusercontent.com/2803110/97784813-7b4bf100-1be4-11eb-9603-d80b22e394e7.png)

### 다양한 폰트에서도 문제없이 동작
![image](https://user-images.githubusercontent.com/2803110/97784938-763b7180-1be5-11eb-90dc-829f575dc6d1.png)


### 간단하게 출력하고 싶다면?

```csharp
    var items = new[]
    {
        new Item(){ Name= "Leoric's Crown", Rarity = Rarity.Normal, Slot ="Helm"},
        new Item(){ Name= "Thunderfury", Rarity = Rarity.Unique, Slot ="One Handed Weapon"},
        new Item(){ Name= "할배검 the grandfather", Rarity = Rarity.Legendary, Slot ="Two Handed Weapon"},
        new Item(){ Name= "WINDFORCE", Rarity = Rarity.Magic, Slot ="양손무기"},
    };


    ConsoleTable.Write(items);
```

### Column,Row,Table 색상 설정

```csharp
    ConsoleTable.TableColor = ConsoleColor.Red; //테이블 색깔을 지정합니다.
    ConsoleTable.ColumnColor = ConsoleColor.Cyan; //Column에 들어가는 Text 색상을 지정합니다.
    ConsoleTable.RowColor = ConsoleColor.White; //Row의 색상을 지정합니다. 단, 대리자를 이용한 색상 지정일 경우에는 동작하지 않습니다.
```

### 
![image](https://user-images.githubusercontent.com/2803110/97785181-c5ce6d00-1be6-11eb-8801-4530b05eea8a.png)


### anonymous/tuple을 활용하여 원하는 Property/Member만 출력하기
```csharp
ConsoleTable.Write(items.Select(item => (item.Name, item.Rarity)));
//or
ConsoleTable.Write(items.Select(item => new { item.Name, item.Rarity }));
```
![image](https://user-images.githubusercontent.com/2803110/97873382-e4ee0b80-1d5a-11eb-8dfd-b05caadfcc9e.png)


### 한개의 오브젝트의 Property/Member 정보들을 출력

```csharp
var obj = new Item() { Name = "Leoric's Crown", Rarity = Rarity.Normal, Slot = "Helm" };
ConsoleTable.WriteSingle(obj);

```
![image](https://user-images.githubusercontent.com/2803110/97872733-deab5f80-1d59-11eb-87b1-939bd465fc7d.png)


