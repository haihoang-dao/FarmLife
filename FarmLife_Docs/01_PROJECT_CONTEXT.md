# FARM LIFE - 01: PROJECT CONTEXT

## Project Information

Tên dự án: Farm Life

Thể loại:
- 2D Top Down
- Farming RPG
- Single Player
- Offline

Engine:
- Unity 6.5
- Version: 6000.5.0f1

Language:
- C#

Render Pipeline:
- Universal Render Pipeline (URP 2D)

Art Style:
- Pixel Art

Development Philosophy:
- Gameplay First
- Prototype First
- Graphics Later
- Polish Last

---

# Graduation Goal

Người chơi có thể:

- Di chuyển
- Cuốc đất
- Trồng hạt giống
- Tưới nước
- Cây phát triển
- Thu hoạch
- Nhận Item
- Inventory
- Bán Item
- Nhận Coin
- Mua Seed
- Mở rộng Farm
- NPC
- Dialogue
- Quest
- Save / Load

Không phát triển:

- Multiplayer
- Combat
- Dungeon
- Fishing
- Marriage
- Online

---

# Project Structure

Assets

└── _Project

    ├── Art
    ├── Animations
    ├── Audio
    ├── Fonts
    ├── Materials
    ├── Prefabs
    ├── Resources
    ├── Scenes
    ├── ScriptableObjects
    ├── Scripts
    ├── Shaders
    ├── Sprites
    └── UI

---

# Script Structure

Scripts

- Core
- Player
- Camera
- Input
- Crop
- Inventory
- Item
- Tool
- Building
- NPC
- Dialogue
- Quest
- Economy
- Save
- UI
- Utilities

---

# Coding Rules

- PascalCase cho Class
- PascalCase cho Method
- camelCase cho Field
- [SerializeField] private cho biến Inspector
- Một Script chỉ có một nhiệm vụ
- Comment tiếng Việt
- Giải thích Unity API
- Code sạch
- Có Debug
- Có Git Commit

---

# Working Rules

ChatGPT đóng vai Senior Unity Developer.

Người học là Junior Unity Developer.

Mỗi bài học phải gồm:

1. Lý thuyết
2. Thiết kế hệ thống
3. Kiến trúc
4. Folder
5. Script
6. Code
7. Giải thích từng dòng
8. Unity API
9. Debug
10. Bài tập
11. Git Commit

Không được:

- Nhảy Phase
- Bỏ bước
- Đổi Roadmap
- Tự thêm Feature
- Dùng kiến trúc khác khi chưa được yêu cầu

Luôn ưu tiên Gameplay trước.