#include <stdbool.h>
#include "stm32f4xx.h"
#include "stm32f4xx_hal.h"
#include "lcd.h"
#include "GPIOMagic.h"

bool isButtonPressed(int button) {
	return ((0x01U << button) != (GPIOF->IDR & (0x01U << button)));
}; 

/**
Button can be 0-7, blue or yellow
*/
void modifyLight(int button, bool isBlue, bool disable) {
	int alt = 0;
	
	if (disable) {
		alt = 16;
	}
	
	int value = (0x01U << button) << alt;
	
	if (isBlue) {
		GPIOD->BSRR = value;
	} else {
		GPIOE->BSRR = value;
	}
}

void modifyLightFullrange(int button, bool disable) {
	bool isBlue = false;
	
	if (button > 7) {
		isBlue = true;
		button -= 8;
	}
	
	modifyLight(button, isBlue, disable);
}

void resetAllLights(bool enable) {
	for (int i = 0; i<=15; i++) {
		modifyLightFullrange(i, !enable);
	}
}
