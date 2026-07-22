# FARM LIFE — DEVELOPMENT LOG

> Chỉ ghi những gì đã thực sự hoàn thành, thay đổi hoặc sửa lỗi.  
> Không ghi lỗi giả định hoặc nội dung chưa được triển khai.

## Phase 00 — Project Setup

**Trạng thái:** ✅ Hoàn thành

Đã hoàn thành:

- Tạo Unity Project.
- Thiết lập Git và GitHub.
- Tạo `.gitignore`.
- Tạo `README`.
- Chuẩn hóa cấu trúc thư mục.
- Import Clover Valley và Seliel Farming Crops.
- Tạo Scene `Farm.unity`.
- Thêm Scene vào Build Profile.
- Thiết lập URP 2D.
- Thiết lập Sorting Layer và Layer.
- Thiết lập Input System.
- Thống nhất Coding Convention.
- Thống nhất Git Workflow.

Thay đổi quan trọng:

- Gán `UniversalRP` trong Project Settings.
- Điều chỉnh `.gitignore` để phù hợp với cấu trúc Repository có Unity Project nằm trong thư mục con.

---

## Phase 01 — Foundation Gameplay

**Trạng thái:** ✅ Hoàn thành

Đã hoàn thành:

- Sprite slicing.
- Tile Palette.
- Grid.
- Tilemap Ground.
- Tilemap Building.
- Tilemap Decoration.
- Tilemap Collision.
- Vẽ Prototype Farm.
- Tree bằng SpriteRenderer.
- Chuẩn hóa Asset về `_Project`.
- Player Prefab.
- Rigidbody2D và CapsuleCollider2D.
- Player Input.
- Player Movement.
- Player Animation.
- Animator.
- Camera Follow bằng Cinemachine.
- Tilemap Collision.
- Tree Prefab.
- Y Sorting.
- Kiểm thử Map, Player, Camera, Collision và Sorting.

---

## Phase 02 — Interaction System

**Trạng thái:** ✅ Hoàn thành

Đã hoàn thành:

- 2.1 Interaction Architecture.
- 2.2 Thêm Interact Input Action với phím `E`.
- 2.3 Tạo `IInteractable`.
- 2.4 Tạo Layer `Interactable` và LayerMask.
- 2.5 Tạo Interaction Point phía trước Player.
- 2.6 Tích hợp Facing Direction từ Phase 01.
- 2.7 Tạo `PlayerInteraction`.
- 2.8 Tạo Test Interactable và Gizmos Debug.
- 2.9 Kiểm thử Interaction.
- 2.10 Review, lưu Scene, Apply Prefab và Git Commit.

Luồng hệ thống:

```text
Phím E
  ↓
PlayerInputReader
  ↓
PlayerInteraction
  ↓
Phát hiện mục tiêu hợp lệ phía trước
  ↓
IInteractable.Interact()
```

Kết quả:

- Player có thể phát hiện mục tiêu theo hướng nhìn.
- Player chỉ phát hiện Collider thuộc LayerMask tương tác.
- Player có thể gọi hành vi `Interact()` bằng phím `E`.
- Hệ thống xử lý an toàn khi không có mục tiêu.
- Có vùng Debug bằng Gizmos.
- Có Interaction Prompt thử nghiệm.

---

## Current Status

```text
Ready
  ↓
Phase 03
  ↓
Item & Inventory Foundation
  ↓
Lesson 3.1 — Item & Inventory Architecture
```

## Phase 03 — Chưa bắt đầu triển khai

Chưa được ghi là hoàn thành cho tới khi từng Lesson được thực hiện và kiểm thử.

Mục tiêu Phase 03:

- Item Type.
- Item Data ScriptableObject.
- Test Items.
- Item Stack.
- Inventory Model.
- Add Item.
- Remove Item.
- Inventory Capacity.
- Hotbar Data.
- Item Pickup thử nghiệm.
- Inventory Debug.
- Testing.
- Review và Git Commit.
