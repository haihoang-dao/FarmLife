# FARM LIFE — ASSET DATABASE

## 1. Quy tắc chung

- Không chỉnh sửa trực tiếp Asset gốc.
- Không đổi tên hoặc di chuyển file bên trong Asset gốc nếu không cần thiết.
- Asset cần cắt, chỉnh Pivot, đổi tên hoặc chỉnh sửa phải được copy sang `_Project`.
- Scene, Prefab và Script của dự án chỉ nên tham chiếu tới bản đã chuẩn hóa trong `_Project` khi có thể.
- Không xóa Asset gốc khi dự án vẫn đang sử dụng file từ Asset đó.

## 2. Clover Valley V2 Free

### Mục đích sử dụng

- Player.
- NPC.
- Buildings.
- Environment.
- Animals.
- Tilesets.
- Decoration.
- Item artwork khi phù hợp.

### Quy tắc

Asset cần chỉnh sửa được copy sang:

```text
Assets/_Project/Sprites/
```

hoặc thư mục con phù hợp.

## 3. Seliel Farming Crops

Asset sử dụng:

- Seliel Farming Crops #1.
- Seliel Farming Crops #2.

Mục đích:

- Seed artwork.
- Crop stages.
- Harvested crop artwork.
- Farming-related Item icons nếu phù hợp.

Quy tắc:

- Đây là bộ Crop chính thức của dự án.
- Không sử dụng Crop của Clover Valley cho hệ thống Crop chính.
- Không chỉnh sửa file gốc.
- Copy Sprite cần dùng sang `_Project` trước khi chỉnh sửa.

## 4. Player

Nguồn:

- Clover Valley.
- Character: Boy.

Đã sử dụng:

- Idle Down.
- Idle Up.
- Idle Side.
- Walk Down.
- Walk Up.
- Walk Side.

Đã hoàn thành:

- Sprite slicing.
- Animation Clip.
- Animator.
- Flip X.
- Facing Direction.
- Player Prefab.

## 5. Map và Tilemap

Đã sử dụng:

- Grass.
- Path.
- Fence.
- Lake.
- Building.
- Decoration.
- Collision tiles.

Tile Palette chính:

```text
FarmPalette
```

Cấu trúc dự kiến:

```text
Assets/_Project/Art/TilePalette/
├── FarmPalette/
└── Tiles/
```

## 6. Tree

Tree sử dụng:

- SpriteRenderer.
- Không dùng Tilemap cho Tree chính.
- Root chứa Collider.
- Graphics chứa SpriteRenderer.
- Có Prefab.
- Có Y Sorting.
- Pivot và Sort Point phải phù hợp với chân cây.

## 7. Scene

Scene chính:

```text
Assets/_Project/Scenes/Farm.unity
```

Scene hiện có:

- Prototype Farm.
- Tilemap.
- Collision.
- Player.
- Tree Prefab.
- Camera.
- Interaction Test Objects nếu còn được giữ lại.

## 8. Render Pipeline

- Universal Render Pipeline 2D.
- Renderer2D.
- UniversalRP đã được gán trong Project Settings.

## 9. Asset cho Phase 03

Phase 03 cần chuẩn bị Icon thử nghiệm cho:

- Seed.
- Crop.
- Material hoặc Item thông thường.
- Tool chỉ dùng làm dữ liệu thử nghiệm nếu cần, chưa triển khai Tool gameplay.

Icon có thể lấy từ Asset hiện có, nhưng phải:

1. Xác định rõ nguồn.
2. Copy sang `_Project/Sprites/Items/`.
3. Thiết lập Sprite Import đúng.
4. Không chỉnh trực tiếp Asset gốc.
