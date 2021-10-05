#include "menuTypes.h"
#include "design.h"
#include <JAGL.h>

Context_t createTopMenu(char *name, SDL_Color mainColor, int topButtonX, TopMenuEntry_t *listItems, u64 entryCount, ShapeLinker_t *extraShapes){
    ShapeLinker_t *out = NULL;
    ShapeLinker_t *entries = NULL;

    /*
    ShapeLinkAdd(&entries, ListItemCreate(COLOR_WHITE, COLOR_WHITE, NULL, "Create folder", NULL), ListItemType);
    ShapeLinkAdd(&entries, ListItemCreate(COLOR_WHITE, COLOR_WHITE, NULL, "Create file", NULL), ListItemType);
    */

    for (int i = 0; i < entryCount; ++i){
        ShapeLinkAdd(&entries, ListItemCreate(COLOR_WHITE, COLOR_WHITE, NULL, listItems[i].name, NULL), ListItemType);
    }

    int len = entryCount * 60 + 50;

    SDL_Texture *screenshot = ScreenshotToTexture();
    ShapeLinkAdd(&out, ImageCreate(screenshot, POS(0, 0, SCREEN_W, SCREEN_H), 0), ImageType);
    ShapeLinkAdd(&out, RectangleCreate(POS(0, 0, SCREEN_W, SCREEN_H), COLOR(0,0,0,130), 1), RectangleType);
    ShapeLinkAdd(&out, RectangleCreate(POS(200, 50, SCREEN_W - 400, len), COLOR_TOPBAR, 1), RectangleType);
    //ShapeLinkAdd(&out, RectangleCreate(POS(200,0,200,50), COLOR_BUTTONPOWER, 1), RectangleType);
    //ShapeLinkAdd(&out, TextCenteredCreate(POS(200,0,200,50), "Power", COLOR_WHITE, FONT_TEXT[FSize28]), TextCenteredType);
    ShapeLinkAdd(&out, ButtonCreate(POS(topButtonX, 50, 200, 50), COLOR_TOPBAR, mainColor, COLOR_WHITE, COLOR_TOPBARSELECTION, 0, ButtonStyleTopStrip, "Back", FONT_TEXT[FSize28], exitFunc), ButtonType);
    ShapeLinkAdd(&out, ButtonCreate(POS(topButtonX, 0 , 200, 50), mainColor, mainColor, COLOR_WHITE, COLOR_TOPBARSELECTION, 0, ButtonStyleFlat, name, FONT_TEXT[FSize28], exitFunc), ButtonType);

    ListView_t *lv = ListViewCreate(POS(200, 100, SCREEN_W - 400, len - 50), 60, COLOR_CENTERLISTBG, COLOR_CENTERLISTSELECTION, COLOR_CENTERLISTPRESS, LIST_CENTERLEFT, entries, exitFunc, NULL, FONT_TEXT[FSize33]);
    ShapeLinkAdd(&out, lv, ListViewType);

    ShapeLinker_t *last = ShapeLinkOffset(out, ShapeLinkCount(out) - 1);

    ShapeLinkMergeLists(&out, extraShapes);

    Context_t ctx;

    do {
        ctx = MakeMenu(out, ButtonHandlerBExit, NULL);

        if (ctx.curOffset == 5 && ctx.origin == OriginFunction){
            if (listItems[lv->highlight].toRun(&ctx) < 0)
                break;
            else {
                SETBIT(lv->options, LIST_SELECTED | LIST_PRESSED, 0);
                lv->highlight = 0;
            }
        }

    } while (ctx.curOffset == 5 && ctx.origin == OriginFunction);

    last->next = NULL;
    ShapeLinkDispose(&out);

    return ctx;
}