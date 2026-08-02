## Process for creating new item

### Folder Items
Simple enough, create new Texture Button Scene in ./FolderObjects/ or copy old scene and set new texture
Set itemType and texture
Attach FolderItem.cs script

In main_dressup.tscn add this new scene as a child of appropriate StrictContainer
Set FolderContainer to MenuClothes node, parent of all StrictContainers
Set ItemLayerNode to Item Layer node2D under Characters node2D

### Live Items
Simple enough, create new sprite Scene in ./LiveObjects/ or copy old scene and set new texture

Attach LiveItem.cs script
Assign which Doll the item is relevant to, and its Item Type
If item can cover multiple slots, define them under Item Slots, otherwise it will default to just the Item Type
Add Area2D as a child
Add CollisionPolygon2D as a child of Area2D, and add points to define the collision polygon, slightly bigger than the item itself

### Magnet Position
In j_scene/m_scene, whatever makes sense for the item youre adding, add the LiveItem scene youve just made as a child of the Node2D relevant to the item i.e. DressSprites
