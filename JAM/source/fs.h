#pragma once
#include <switch.h>
#include <JAGL.h>

typedef struct {
    char *name;
    bool isDir;
    u64 size;
} FileInfo_t;

ShapeLinker_t *ListFolder(char *path);
ShapeLinker_t *ListFolderSorted(char *path);
ShapeLinker_t *ConvertFolderListToListItems(ShapeLinker_t *list);
void FreeFileInfoList(ShapeLinker_t **list);
char *fsutil_getnextloc(const char *current, const char *add);
void RegenFileListing(Context_t *ctx);