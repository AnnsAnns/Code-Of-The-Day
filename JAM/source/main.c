// Include the most common headers from the C standard library
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

// Include the main libnx system header, for Switch development
#include <switch.h>
#include <JAGL.h>
#include "design.h"

int main(int argc, char* argv[])
{
    InitSDL();
    FontInit();
    romfsInit();
    InitDesign();

    ShapeLinker_t *mainMenu = CreateMainMenu();
    MakeMenu(mainMenu, NULL, NULL);
    ShapeLinkDispose(&mainMenu);
    
    ExitDesign();
    romfsExit();
    FontExit();
    ExitSDL();
    return 0;
}
