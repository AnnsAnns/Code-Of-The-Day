#ifndef _GPIO_MAGIC
#define _GPIO_MAGIC
#include <stdbool.h>

bool isButtonPressed(int button);
void modifyLight(int button, bool isBlue, bool disable);
void modifyLightFullrange(int button, bool disable);
void resetAllLights(bool enable);

#endif
