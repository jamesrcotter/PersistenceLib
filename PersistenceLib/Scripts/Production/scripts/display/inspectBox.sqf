private _box = _this select 0;

private _items = itemCargo _box;
private _magazines = magazineCargo _box;

diag_log _items;
hint format ["%1", _items + _magazines];