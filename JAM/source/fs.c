#include "fs.h"
#include "design.h"
#include <JAGL.h>
#include <switch.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <dirent.h>
#include <unistd.h>

char *toLower(char *in){
    char *out = CopyTextUtil(in);
    
    for (char *iter = out; *iter; ++iter)
        *iter = tolower(*iter);
    

    return out;
}

void ShapeLinkAddSorted(ShapeLinker_t **start, void *item, u8 type){
    ShapeLinker_t *add = malloc(sizeof(ShapeLinker_t));
    add->item = item;
    add->type = type;
    add->next = NULL;

    FileInfo_t *info;
    FileInfo_t *NInfo = item;
    char *dst = toLower(NInfo->name);
    char *src;
    int res;


    if (*start != NULL){
        info = (*start)->item;
        src = toLower(info->name);
        res = strcmp(src, dst);
        free(src);
    }

    
    if (*start == NULL || res > 0){
        add->next = *start;
        *start = add;
    }
    else {
        ShapeLinker_t *iter = *start;
        while(iter->next != NULL){
            info = iter->next->item;
            src = toLower(info->name);
            res = strcmp(src, dst);
            free(src);

            if (res >= 0)
                break;
            
            iter = iter->next;
        }
        add->next = iter->next;
        iter->next = add;
    }

    free(dst);
}

FileInfo_t *FileInfoCreate(char *name, bool isDir, u64 size){
    FileInfo_t *out = malloc(sizeof(FileInfo_t));

    out->name = CopyTextUtil(name);
    out->isDir = isDir;
    out->size = size;

    return out;
}

void FreeFileInfoList(ShapeLinker_t **list){
    for (ShapeLinker_t *iter = *list; iter != NULL;){
        ShapeLinker_t *iterNext = iter->next;

        FileInfo_t *info = iter->item;
        free(info->name);
        free(info);

        free(iter);
        iter = iterNext;
    }

    *list = NULL;
}

char *fsutil_getnextloc(const char *current, const char *add){
    static char *ret;

    if (ret != NULL){
        free(ret);
        ret = NULL;
    }

    size_t size = strlen(current) + strlen(add) + 2;
    ret = malloc (size);

    if (current[strlen(current) - 1] == '/')
        sprintf(ret, "%s%s", current, add);
    else
        sprintf(ret, "%s/%s", current, add);

    return ret;
}

ShapeLinker_t *ListFolderSorted(char *path){
    ShapeLinker_t *out = NULL;

    struct dirent *de;
    struct stat stats;
    DIR *dr = opendir(path);

    if (dr == NULL)
        return NULL;

    while ((de = readdir(dr)) != NULL){
        stat(fsutil_getnextloc(path, de->d_name), &stats);
        ShapeLinkAddSorted(&out, FileInfoCreate(de->d_name, de->d_type & DT_DIR, stats.st_size), DataType);
    }

    closedir(dr);
    return out;
}

ShapeLinker_t *ListFolder(char *path){
    ShapeLinker_t *out = NULL;

    struct dirent *de;
    struct stat stats;
    DIR *dr = opendir(path);

    if (dr == NULL)
        return NULL;

    while ((de = readdir(dr)) != NULL){
        stat(fsutil_getnextloc(path, de->d_name), &stats);
        ShapeLinkAdd(&out, FileInfoCreate(de->d_name, de->d_type & DT_DIR, stats.st_size), DataType);
    }

    closedir(dr);
    return out;
}

const char *fileSizes[] = {"B", "KB", "MB", "GB"};

char *ConvertSizeToText(u64 size){
    int stage = 0;
    u64 localSize = size;

    while (localSize > 1024){
        localSize /= 1024;
        stage++;
    }

    if (stage > 3)
        stage = 3;

    return CopyTextArgsUtil("%d %s", localSize, fileSizes[stage]);
}

ShapeLinker_t *ConvertFolderListToListItems(ShapeLinker_t *list){
    ShapeLinker_t *out = NULL;

    for (ShapeLinker_t *iter = list; iter != NULL; iter = iter->next){
        FileInfo_t *info = iter->item;
        char *RText = (info->isDir) ? CopyTextUtil("<DIR>") : ConvertSizeToText(info->size);
        
        ShapeLinkAdd(&out, ListItemCreate(COLOR_WHITE, COLOR(255,136,0,255), (info->isDir) ? folderSIcon : fileSIcon, info->name, RText), ListItemType);
        free(RText);
    }

    return out;
}

void RegenFileListing(Context_t *ctx){
    char *curPath = CopyTextUtil(ShapeLinkFind(ctx->all, DataType)->item);
    
    ShapeLinker_t *dirList = ListFolderSorted(curPath);
    ShapeLinker_t *menu = CreateFileExplorerMenu(dirList, curPath);

    free(curPath);
    ShapeLinkDispose(&ctx->all);
    ctx->all = menu;
    ctx->curOffset = 0;

    for (ShapeLinker_t *iter = ctx->all; iter != NULL; iter = iter->next){
        if (iter->type >= ListViewType){
            ctx->selected = iter;
            break;
        }
        ctx->curOffset++;
    }

    ShapeLinker_t *lvShape = ShapeLinkFind(menu, ListViewType);

    if (lvShape != NULL){
        ListView_t *lv = lvShape->item;
        lv->highlight = 0;
        lv->options |= LIST_SELECTED;
    }

    FreeFileInfoList(&dirList);
}