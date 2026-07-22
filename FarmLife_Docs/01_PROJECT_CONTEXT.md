# FARM LIFE — PROJECT CONTEXT

## 1. Thông tin dự án

| Mục | Giá trị |
|---|---|
| Tên dự án | Farm Life |
| Thể loại | 2D Top Down Farming RPG |
| Chế độ | Single Player, Offline |
| Engine | Unity 6.5 |
| Phiên bản Unity | 6000.5.0f1 |
| Ngôn ngữ | C# |
| Render Pipeline | Universal Render Pipeline 2D |
| Phong cách đồ họa | Pixel Art |

## 2. Triết lý phát triển

1. Gameplay First.
2. Prototype First.
3. Hoàn thiện hệ thống trước.
4. UI sau khi gameplay ổn định.
5. Graphics và Polish thực hiện cuối.

## 3. Mục tiêu đồ án

Người chơi có thể:

- Di chuyển trong bản đồ.
- Tương tác với đối tượng.
- Nhặt và quản lý vật phẩm.
- Chọn và sử dụng công cụ.
- Cuốc đất.
- Trồng hạt giống.
- Tưới nước.
- Chờ cây phát triển.
- Thu hoạch.
- Nhận vật phẩm vào Inventory.
- Bán vật phẩm và nhận Coin.
- Mua hạt giống.
- Giao tiếp với NPC.
- Nhận và hoàn thành Quest.
- Mở rộng nông trại.
- Lưu và tải dữ liệu.

Không phát triển trong phạm vi đồ án:

- Multiplayer.
- Combat.
- Dungeon.
- Fishing.
- Marriage.
- Online.
- Mine.

## 4. Cấu trúc thư mục dự án

```text
Assets/
└── _Project/
    ├── Art/
    ├── Animations/
    ├── Audio/
    ├── Fonts/
    ├── Input/
    ├── Materials/
    ├── Prefabs/
    ├── Resources/
    ├── Scenes/
    ├── Scripts/
    ├── ScriptableObjects/
    ├── Shaders/
    ├── Sprites/
    └── UI/
```

## 5. Cấu trúc Script dự kiến

```text
Scripts/
├── Core/
├── Player/
├── Camera/
├── Input/
├── Interaction/
├── Item/
├── Inventory/
├── Tool/
├── Crop/
├── Time/
├── Economy/
├── NPC/
├── Dialogue/
├── Quest/
├── Building/
├── Animal/
├── Save/
├── UI/
└── Utilities/
```

## 6. Quy tắc code

- Class, Struct, Enum, Method và Property dùng `PascalCase`.
- Field và biến cục bộ dùng `camelCase`.
- Biến cần hiển thị trong Inspector dùng `[SerializeField] private`.
- Một Script chỉ chịu trách nhiệm cho một nhóm hành vi rõ ràng.
- Không tạo Script quá lớn khi có thể tách trách nhiệm hợp lý.
- Comment và XML Documentation viết bằng tiếng Việt.
- Giải thích đầy đủ Unity API được sử dụng.
- Có bước Debug và kiểm thử trước khi chuyển bài.
- Mỗi nhóm thay đổi phải có Git Commit rõ ràng.

## 7. Quy tắc hướng dẫn

ChatGPT đóng vai Senior Unity Developer.

Người học đóng vai Junior Unity Developer.

Mỗi bài học phải có:

1. Mục tiêu.
2. Lý thuyết.
3. Thiết kế hệ thống.
4. Kiến trúc.
5. Cấu trúc Folder.
6. Tạo hoặc chỉnh sửa Script.
7. Code hoàn chỉnh.
8. Giải thích code.
9. Giải thích Unity API.
10. Cấu hình Inspector từng bước.
11. Debug và lỗi thường gặp.
12. Điều kiện hoàn thành.
13. Bài kiểm tra.
14. Git Commit.

Không được:

- Nhảy Phase.
- Bỏ bài.
- Dạy lại nội dung đã hoàn thành.
- Tự ý đổi Roadmap.
- Tự ý thêm Feature ngoài phạm vi.
- Thay đổi kiến trúc hiện tại khi chưa phân tích ảnh hưởng.
- Viết lại Player Movement hoặc Player Animation nếu bài hiện tại không yêu cầu.
