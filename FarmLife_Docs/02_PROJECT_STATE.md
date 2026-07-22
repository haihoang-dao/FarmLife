# FARM LIFE — PROJECT STATE

## 1. Thông tin dự án

| Mục | Giá trị |
|---|---|
| Project | Farm Life |
| Unity | 6000.5.0f1 |
| Render Pipeline | Universal Render Pipeline 2D |
| Language | C# |
| Git Branch | `main` |
| Scene chính | `Farm.unity` |

## 2. Tiến độ hiện tại

| Phase | Trạng thái |
|---|---|
| Phase 00 — Project Setup | ✅ Hoàn thành |
| Phase 01 — Foundation Gameplay | ✅ Hoàn thành |
| Phase 02 — Interaction System | ✅ Hoàn thành |
| Phase 03 — Item & Inventory Foundation | 🟨 Bắt đầu |

**Current Phase:** Phase 03  
**Current Lesson:** 3.1  
**Current Module:** Item & Inventory Foundation

## 3. Phase 00 đã hoàn thành

- Tạo Unity Project.
- Thiết lập Git và GitHub.
- Tạo cấu trúc thư mục.
- Import Asset.
- Tạo Scene Farm.
- Thiết lập Build Profile.
- Thiết lập URP 2D.
- Chuẩn hóa Sorting Layer và Layer.
- Thiết lập Input System.
- Thống nhất Coding Convention và Git Workflow.

## 4. Phase 01 đã hoàn thành

### Map và môi trường

- Grid.
- Tilemap Ground.
- Tilemap Building.
- Tilemap Decoration.
- Tilemap Collision.
- Tile Palette.
- Prototype Farm.
- Tree SpriteRenderer.
- Tree Prefab.
- Tilemap Collision.
- Y Sorting.

### Player

- Player Prefab.
- SpriteRenderer.
- Rigidbody2D.
- CapsuleCollider2D.
- Player Input.
- Player Movement.
- Player Animation.
- Animator.
- Camera Follow.
- Collision.
- Y Sorting.

## 5. Phase 02 đã hoàn thành

- 2.1 Interaction Architecture.
- 2.2 Interact Input Action.
- 2.3 `IInteractable` Interface.
- 2.4 Interaction Layer.
- 2.5 Interaction Area.
- 2.6 Facing Direction Integration.
- 2.7 `PlayerInteraction`.
- 2.8 Test Interactable và Debug.
- 2.9 Interaction Testing.
- 2.10 Review và Git Commit.

Luồng tương tác hiện tại:

```text
Phím E
  ↓
PlayerInputReader
  ↓
PlayerInteraction
  ↓
Phát hiện mục tiêu phía trước
  ↓
IInteractable.Interact()
```

## 6. Hệ thống hiện có

- Player Movement.
- Player Animation.
- Facing Direction.
- Camera Follow.
- Collision.
- Y Sorting.
- Interaction Input.
- Interaction Point.
- Interaction LayerMask.
- `IInteractable`.
- `PlayerInteraction`.
- Interaction Prompt.
- Test Interactable.

## 7. Script hiện tại

```text
Scripts/
├── Player/
│   ├── PlayerInputReader.cs
│   ├── PlayerMovement.cs
│   ├── PlayerAnimation.cs
│   ├── PlayerFacingDirection.cs
│   └── PlayerInteraction.cs
├── Interaction/
│   ├── IInteractable.cs
│   ├── TestInteractable.cs
│   ├── InteractionPromptUI.cs
│   └── InteractionPromptTest.cs
└── Sorting/
    └── YSortRenderer.cs
```

Tên Script trong danh sách này phải được cập nhật nếu dự án thực tế thay đổi.

## 8. Prefab và Scene hiện có

### Scene

- `Farm.unity`.

### Prefab

- Player.
- Tree Prefab.
- Các Test Interactable đã tạo trong Phase 02 nếu còn được giữ lại.

## 9. Gameplay hiện tại

Player có thể:

1. Di chuyển.
2. Phát Animation đúng hướng.
3. Va chạm với Tilemap và Tree.
4. Được Camera theo dõi.
5. Sắp xếp Sprite theo trục Y.
6. Nhìn theo bốn hướng.
7. Phát hiện đối tượng tương tác phía trước.
8. Hiện Interaction Prompt.
9. Nhấn phím `E`.
10. Gọi `IInteractable.Interact()` trên mục tiêu hợp lệ.

Chưa có:

- Item Data.
- Item Stack.
- Inventory.
- Hotbar.
- Item Pickup chính thức.
- Tool.
- Crop.
- Time & Day.
- Economy.
- NPC.
- Dialogue.
- Quest.
- Farm Expansion.
- Animal.
- Save / Load.
- UI hoàn chỉnh.

## 10. Phase tiếp theo

# Phase 03 — Item & Inventory Foundation

Mục tiêu:

```text
ItemData
   ↓
ItemStack
   ↓
Inventory
   ↓
Hotbar Data
   ↓
Item Pickup thử nghiệm
```

Kết quả cuối Phase 03:

- Có cấu trúc dữ liệu Item tổng quát.
- Có Item mẫu bằng ScriptableObject.
- Có Item Stack.
- Có Inventory Model độc lập với UI.
- Có Add Item và Remove Item.
- Có giới hạn sức chứa.
- Có Hotbar Data cơ bản.
- Có Item Pickup thử nghiệm.
- Có công cụ Debug Inventory.
- Chưa triển khai Tool gameplay hoặc Crop gameplay.

## 11. Bài tiếp theo

**Lesson 3.1 — Item & Inventory Architecture**

Không được viết lại:

- Player Movement.
- Player Animation.
- Camera.
- Collision.
- Y Sorting.
- Interaction System.

Phase 03 phải xây dựng trên các hệ thống đã hoàn thành.
