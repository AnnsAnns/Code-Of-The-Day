#pragma once
#include <JAGL.h>

typedef struct {
    char *name;
    func_ptr toRun;
} TopMenuEntry_t;

Context_t createTopMenu(char *name, SDL_Color mainColor, int topButtonX, TopMenuEntry_t *listItems, u64 entryCount, ShapeLinker_t *extraShapes);