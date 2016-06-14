_this addAction ["Save", "scripts\persistence\actionSaveKit.sqf", [_this, "testKit"], 0, false, false, "", "(_target == _this)"];
_this addAction ["Load", "scripts\persistence\actionLoadKit.sqf", [_this, "testKit"], 0, false, false, "", "(_target == _this)"];
_this addAction ["Inspect Primary", "scripts\display\hintObject.sqf", [_this, "primaryWeapon", "CfgWeapons"], 0, false, false, "", "(_target == _this)"];
_this addAction ["Inspect Secondary", "scripts\display\hintObject.sqf", [_this, "secondaryWeapon", "CfgWeapons"], 0, false, false, "", "(_target == _this)"];
_this addAction ["Inspect Handgun", "scripts\display\hintObject.sqf", [_this, "handgunWeapon", "CfgWeapons"], 0, false, false, "", "(_target == _this)"];