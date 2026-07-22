# FARM LIFE — PHASE CHECKLIST

## 1. Trạng thái dự án

| Mục | Giá trị |
|---|---|
| Current Phase | Phase 03 |
| Current Lesson | 3.1 |
| Current Module | Item & Inventory Foundation |
| Completed Phases | 3/15 nếu tính từ Phase 00 đến Phase 14 |

### Ký hiệu

- `☐` Chưa bắt đầu.
- `🟨` Đang thực hiện.
- `☑` Hoàn thành.
- `✅` Phase hoàn thành.

---

## Phase 00 — Project Setup

**Trạng thái:** ✅ COMPLETED

- ☑ Unity Project.
- ☑ Git và GitHub.
- ☑ Folder Structure.
- ☑ Import Assets.
- ☑ Scene và Build Profile.
- ☑ URP 2D.
- ☑ Sorting Layer và Layer.
- ☑ Input System.
- ☑ Coding Rules.
- ☑ Git Workflow.
- ☑ Review.

---

## Phase 01 — Foundation Gameplay

**Trạng thái:** ✅ COMPLETED

- ☑ Bài 01 — Tạo Project Farm Life.
- ☑ Bài 02 — Git.
- ☑ Bài 03 — Cấu trúc thư mục.
- ☑ Bài 04 — Import Assets.
- ☑ Bài 05 — Slice Sprite.
- ☑ Bài 06 — Tile Palette.
- ☑ Bài 07 — Grid.
- ☑ Bài 08 — Tilemap.
- ☑ Bài 09 — Brush.
- ☑ Bài 10 — Vẽ Map.
- ☑ Bài 11 — Tree.
- ☑ Bài 12 — Chuẩn hóa Asset.
- ☑ Bài 13 — Player.
- ☑ Bài 14 — Animation.
- ☑ Bài 15 — Input System.
- ☑ Bài 16 — Player Movement.
- ☑ Bài 17 — Player Animation.
- ☑ Bài 18 — Camera.
- ☑ Bài 19 — Collision.
- ☑ Bài 20 — Tree Prefab.
- ☑ Bài 21 — Test.
- ☑ Bài 22 — Lưu Project.
- ☑ Bài 23 — Y Sorting.

---

## Phase 02 — Interaction System

**Trạng thái:** ✅ COMPLETED

- ☑ 2.1 — Interaction Architecture.
- ☑ 2.2 — Interact Input Action.
- ☑ 2.3 — IInteractable Interface.
- ☑ 2.4 — Interaction Layer.
- ☑ 2.5 — Interaction Area.
- ☑ 2.6 — Facing Direction Integration.
- ☑ 2.7 — PlayerInteraction.
- ☑ 2.8 — Test Interactable và Debug.
- ☑ 2.9 — Interaction Testing.
- ☑ 2.10 — Review và Git Commit.

---

# Phase 03 — Item & Inventory Foundation

**Trạng thái:** 🟨 IN PROGRESS

## 3.1 — Item & Inventory Architecture

- 🟨 Xác định ranh giới giữa Item Data, Item Stack, Inventory và Hotbar.
- ☐ Kiểm tra cấu trúc Script hiện tại.
- ☐ Xác định Folder mới.
- ☐ Thiết kế luồng thêm, xóa và chứa Item.
- ☐ Không viết UI trong Inventory Model.
- ☐ Không triển khai Tool hoặc Crop gameplay.

## 3.2 — ItemType

- ☐ Tạo `ItemType`.
- ☐ Xác định các nhóm Item tối thiểu.
- ☐ Không nhét logic gameplay vào Enum.
- ☐ Kiểm tra khả năng mở rộng.

## 3.3 — ItemData ScriptableObject

- ☐ Tạo `ItemData.cs`.
- ☐ Tạo Menu Asset.
- ☐ Khai báo ID.
- ☐ Khai báo Display Name.
- ☐ Khai báo Icon.
- ☐ Khai báo Item Type.
- ☐ Khai báo Max Stack.
- ☐ Validate dữ liệu.

## 3.4 — Create Test Items

- ☐ Tạo thư mục Item Data.
- ☐ Tạo ít nhất ba Item mẫu.
- ☐ Gán Icon.
- ☐ Gán Item Type.
- ☐ Gán Max Stack.
- ☐ Kiểm tra ID không trùng.

## 3.5 — ItemStack

- ☐ Tạo `ItemStack`.
- ☐ Lưu `ItemData`.
- ☐ Lưu Quantity.
- ☐ Kiểm tra Stack rỗng.
- ☐ Kiểm tra số lượng hợp lệ.
- ☐ Không vượt Max Stack.

## 3.6 — Inventory Model

- ☐ Tạo `Inventory`.
- ☐ Xác định Capacity.
- ☐ Tạo danh sách Slot hoặc Stack.
- ☐ Không phụ thuộc UI.
- ☐ Không phụ thuộc trực tiếp Player.
- ☐ Có API đọc dữ liệu an toàn.

## 3.7 — Add Item

- ☐ Ưu tiên gộp vào Stack hiện có.
- ☐ Tạo Stack mới khi cần.
- ☐ Xử lý số lượng dư.
- ☐ Trả về kết quả rõ ràng.
- ☐ Debug Add Item.

## 3.8 — Remove Item

- ☐ Tìm đúng Item.
- ☐ Giảm Quantity.
- ☐ Xóa Stack rỗng.
- ☐ Xử lý không đủ số lượng.
- ☐ Trả về kết quả rõ ràng.
- ☐ Debug Remove Item.

## 3.9 — Inventory Capacity

- ☐ Kiểm tra Inventory đầy.
- ☐ Kiểm tra Stack còn chỗ.
- ☐ Kiểm tra Item không Stack được.
- ☐ Kiểm tra số lượng lớn hơn một Stack.
- ☐ Không làm mất Item dư.

## 3.10 — Hotbar Data

- ☐ Thiết kế Hotbar Data.
- ☐ Xác định số Slot.
- ☐ Theo dõi Slot đang chọn.
- ☐ Không làm UI hoàn chỉnh.
- ☐ Không triển khai Tool Use.
- ☐ Kiểm tra đổi Slot dữ liệu.

## 3.11 — Item Pickup thử nghiệm

- ☐ Tạo Item Pickup thử nghiệm.
- ☐ Sử dụng `IInteractable` hoặc cơ chế phù hợp đã thống nhất.
- ☐ Thêm Item vào Inventory.
- ☐ Không nhặt khi Inventory không đủ chỗ.
- ☐ Xóa Pickup khi đã nhận hết Item.
- ☐ Không làm Drop System hoàn chỉnh.

## 3.12 — Inventory Debug

- ☐ In nội dung Inventory có kiểm soát.
- ☐ Hiển thị Item và Quantity.
- ☐ Kiểm tra Add.
- ☐ Kiểm tra Remove.
- ☐ Kiểm tra Capacity.
- ☐ Console không có lỗi.

## 3.13 — Testing

- ☐ Thêm Item vào Stack rỗng.
- ☐ Gộp Item cùng loại.
- ☐ Tạo Stack mới.
- ☐ Thêm quá Max Stack.
- ☐ Thêm khi Inventory đầy.
- ☐ Xóa một phần Item.
- ☐ Xóa toàn bộ Stack.
- ☐ Xóa Item không tồn tại.
- ☐ Nhặt Item thử nghiệm.
- ☐ Console không có lỗi.

## 3.14 — Review và Git Commit

- ☐ Kiểm tra kiến trúc.
- ☐ Kiểm tra Folder.
- ☐ Kiểm tra Script.
- ☐ Kiểm tra ScriptableObject.
- ☐ Kiểm tra Inspector.
- ☐ Lưu Scene và Assets.
- ☐ Kiểm tra Console.
- ☐ Kiểm tra `git status`.
- ☐ Commit Phase 03.
- ☐ Push GitHub.

---

## Phase 04 — Tool System

- ☐ Tool Architecture.
- ☐ ToolType.
- ☐ ToolData.
- ☐ Tool Selection.
- ☐ Current Tool.
- ☐ Tool Use Input.
- ☐ Tool Target Point.
- ☐ Hoe.
- ☐ Watering Can.
- ☐ Tool Cooldown.
- ☐ Tool Animation Integration.
- ☐ Testing.
- ☐ Review và Git Commit.

---

## Phase 05 — Crop System

- ☐ Farming Architecture.
- ☐ Soil Data.
- ☐ Soil Tile hoặc Plot.
- ☐ Hoe Soil.
- ☐ Seed Data Integration.
- ☐ Plant Seed.
- ☐ Water Soil.
- ☐ Crop Data.
- ☐ Crop Growth Stages.
- ☐ Harvest.
- ☐ Add Harvested Item to Inventory.
- ☐ Reset Soil.
- ☐ Testing.
- ☐ Review và Git Commit.

---

## Phase 06 — Time & Day System

- ☐ Game Time Architecture.
- ☐ Clock.
- ☐ Day Counter.
- ☐ Time Scale.
- ☐ Pause Time.
- ☐ Daily Reset Events.
- ☐ Crop Growth Integration.
- ☐ Testing.
- ☐ Review và Git Commit.

---

## Phase 07 — Economy & Shop

- ☐ Currency.
- ☐ Wallet.
- ☐ Item Price.
- ☐ Sell Box.
- ☐ Shop Data.
- ☐ Buy Item.
- ☐ Sell Item.
- ☐ Seed Shop.
- ☐ Testing.
- ☐ Review và Git Commit.

---

## Phase 08 — NPC & Dialogue

- ☐ NPC Architecture.
- ☐ NPC Prefab.
- ☐ NPC Interaction.
- ☐ Dialogue Data.
- ☐ Dialogue Runner.
- ☐ Dialogue UI.
- ☐ Portrait.
- ☐ NPC Routine cơ bản.
- ☐ Testing.
- ☐ Review và Git Commit.

---

## Phase 09 — Quest System

- ☐ Quest Architecture.
- ☐ QuestData.
- ☐ Quest Objective.
- ☐ Quest State.
- ☐ Quest Manager.
- ☐ Accept Quest.
- ☐ Progress Quest.
- ☐ Complete Quest.
- ☐ Reward.
- ☐ Quest UI.
- ☐ Testing.
- ☐ Review và Git Commit.

---

## Phase 10 — Farm Expansion & Building

- ☐ Expansion Architecture.
- ☐ Unlock Area Data.
- ☐ Area Requirement.
- ☐ Unlock Farm Area.
- ☐ Building Data.
- ☐ Placement Preview.
- ☐ Placement Validation.
- ☐ Building Collision.
- ☐ Confirm và Cancel Placement.
- ☐ Testing.
- ☐ Review và Git Commit.

---

## Phase 11 — Animal System

- ☐ Animal Architecture.
- ☐ Animal Data.
- ☐ Pig.
- ☐ Sheep.
- ☐ Basic AI Movement.
- ☐ Animal Interaction.
- ☐ Product Generation.
- ☐ Collect Product.
- ☐ Testing.
- ☐ Review và Git Commit.

---

## Phase 12 — Save & Load

- ☐ Save Architecture.
- ☐ Save Data Model.
- ☐ Player Data.
- ☐ Inventory Data.
- ☐ Crop Data.
- ☐ Economy Data.
- ☐ Quest Data.
- ☐ JSON Serialization.
- ☐ Save.
- ☐ Load.
- ☐ Auto Save.
- ☐ Data Versioning cơ bản.
- ☐ Testing.
- ☐ Review và Git Commit.

---

## Phase 13 — UI & Menu

- ☐ Main Menu.
- ☐ HUD.
- ☐ Hotbar UI.
- ☐ Inventory UI.
- ☐ Currency UI.
- ☐ Clock UI.
- ☐ Dialogue UI Review.
- ☐ Quest UI Review.
- ☐ Pause Menu.
- ☐ Settings.
- ☐ UI Navigation.
- ☐ Testing.
- ☐ Review và Git Commit.

---

## Phase 14 — Polish, Build & Final Testing

- ☐ Sound Effects.
- ☐ Background Music.
- ☐ Particle Effects.
- ☐ Animation Polish.
- ☐ Camera Polish.
- ☐ UI Polish.
- ☐ Performance Check.
- ☐ Bug Fix.
- ☐ Build Settings.
- ☐ Windows Build.
- ☐ Full Gameplay Test.
- ☐ Graduation Demo Checklist.
- ☐ Final Git Commit.
- ☐ Final Push.

---

## Bài tiếp theo

```text
Phase 03
Lesson 3.1
Item & Inventory Architecture
```

## Quy tắc tiến độ

Khi người dùng nhập `next`:

1. Đánh dấu Lesson hiện tại hoàn thành.
2. Chuyển sang Lesson tiếp theo.
3. Không bỏ Lesson.
4. Không nhảy Phase.
5. Không dạy lại nội dung đã hoàn thành.
6. Chỉ cập nhật Development Log bằng những việc đã thực sự hoàn thành.
7. Cập nhật `02_PROJECT_STATE.md`, `05_DEVELOPMENT_LOG.md` và `06_PHASE_CHECKLIST.md` khi kết thúc Phase hoặc khi cần bàn giao sang chat mới.
