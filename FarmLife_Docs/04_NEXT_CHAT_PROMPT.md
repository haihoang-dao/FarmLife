# FARM LIFE — NEXT CHAT PROMPT

Bạn đang tiếp tục phát triển đồ án tốt nghiệp Unity **Farm Life**.

## 1. Tài liệu phải đọc trước

Đọc theo thứ tự:

1. `01_PROJECT_CONTEXT.md`
2. `02_PROJECT_STATE.md`
3. `03_ASSET_DATABASE.md`
4. `05_DEVELOPMENT_LOG.md`
5. `06_PHASE_CHECKLIST.md`

Sau đó tiếp tục đúng Phase và Lesson hiện tại.

## 2. Trạng thái hiện tại

```text
Phase 03
Item & Inventory Foundation

Current Lesson
3.1 — Item & Inventory Architecture
```

Phase 00, Phase 01 và Phase 02 đã hoàn thành.

Không quay lại dạy lại:

- Project Setup.
- Tilemap.
- Player Movement.
- Player Animation.
- Camera Follow.
- Collision.
- Y Sorting.
- Interaction System.

Chỉ chỉnh các hệ thống trên khi bài hiện tại thực sự yêu cầu tích hợp và phải giải thích rõ ảnh hưởng.

## 3. Quy tắc khi người dùng nhập `next`

Khi người dùng nhập:

```text
next
```

Phải hiểu rằng:

1. Lesson hiện tại đã hoàn thành.
2. Đánh dấu Lesson đó hoàn thành trong Checklist.
3. Chuyển sang Lesson tiếp theo.
4. Không hỏi lại nội dung đã xác nhận.
5. Không bỏ Lesson.
6. Không tự ý chuyển Phase.
7. Không dạy lại bài cũ.

## 4. Cấu trúc bắt buộc của mỗi Lesson

Mỗi bài phải có:

1. Mục tiêu.
2. Kiến thức cần hiểu.
3. Kiến trúc và trách nhiệm từng thành phần.
4. Kiểm tra trạng thái hiện tại trước khi sửa.
5. Đường dẫn Folder chính xác.
6. Các thao tác Unity theo từng click.
7. Script cần tạo hoặc chỉnh sửa.
8. Code hoàn chỉnh.
9. Comment và XML Documentation tiếng Việt.
10. Giải thích từng phần code.
11. Giải thích Unity API.
12. Cấu hình Inspector với giá trị cụ thể.
13. Cách kiểm tra trong Play Mode.
14. Lỗi thường gặp và cách sửa.
15. Điều kiện hoàn thành.
16. Git Commit phù hợp.

## 5. Quy tắc code

- Clean Code.
- Một Script một trách nhiệm.
- Không tạo Controller khổng lồ.
- Dùng `[SerializeField] private` cho tham chiếu Inspector.
- Dùng XML Documentation `///` cho Class, Method và Field quan trọng.
- Không tự ý thay đổi tên Script hiện có.
- Không viết lại Player Movement hoặc Player Animation.
- Không tạo hệ thống Facing Direction mới.
- Tái sử dụng Interaction System của Phase 02.
- Inventory Model phải độc lập với Inventory UI.
- Item Data không được chứa logic dành riêng cho Crop, Tool, NPC hoặc Quest.
- Có kiểm tra `null`, dữ liệu không hợp lệ và giới hạn sức chứa.
- Không ghi lỗi giả định vào Development Log.

## 6. Thứ tự ưu tiên

```text
Gameplay
  ↓
Data Model
  ↓
System
  ↓
Testing
  ↓
UI
  ↓
Polish
```

## 7. Roadmap chính thức từ Phase 03

```text
Phase 03 — Item & Inventory Foundation
Phase 04 — Tool System
Phase 05 — Crop System
Phase 06 — Time & Day System
Phase 07 — Economy & Shop
Phase 08 — NPC & Dialogue
Phase 09 — Quest System
Phase 10 — Farm Expansion & Building
Phase 11 — Animal System
Phase 12 — Save & Load
Phase 13 — UI & Menu
Phase 14 — Polish, Build & Final Testing
```
