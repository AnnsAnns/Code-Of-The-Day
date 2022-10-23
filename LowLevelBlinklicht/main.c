#include "stm32f4xx_hal.h"
#include "init.h"
#include "delay.h"
#include "LCD_GUI.h"
#include "LCD_Demos.h"
#include "lcd.h"
#include "fontsFLASH.h"
#include "LCD_Touch.h"
#include "error.h"
#include <stdbool.h>
#include "stm32f4xx.h"
#include "timer.h"
#include "GPIOMagic.h"

int last_light = 0;
bool isEnabled = false;
bool buttonCurrentlyPressed = false;

/**
* Returns current light number, needs last light number
*/
void NextLauflicht() {
	int light = last_light + 1;
	modifyLightFullrange(last_light, true);
	if (last_light >= 15) {
		light = 0;
	}
	modifyLightFullrange(light, false);
	last_light = light;
}

void NextBlinklicht() {
	resetAllLights(!isEnabled);
	isEnabled = !isEnabled;
}

void AusgLauflicht() {
	lcdPrintlnS("Zustand Lauflicht");
}

void AusgBlinklicht() {
	lcdPrintlnS("Zustand Blinklicht");
}

void schneller() {
	lcdPrintlnS("Wird schneller gemacht!");
	TIM2->ARR *= 0.8;
}

void langsamer() {
	lcdPrintlnS("Wird langsamer gemacht!");
	TIM2->ARR *= 1.2;
}

void nothing(void){}

void clear() {
	resetAllLights(false);
	last_light = 0;
	isEnabled = false;
}
	
enum Zustaende {
	LAUFLICHT,
	BLINKLICHT
} Zustand;

enum Ereignisse {
	TasteS0Runter,
	TasteS0Rauf,
	TasteS1,
	TasteS2,
	TimerAbgelaufen,
	Nichts,
} Ereignis;

struct EventHandler {
	enum Zustaende NeuerZustand;
	void (*Start)(void);
};

struct EventHandler EventHandlerTable[2][6] = {
	{{BLINKLICHT, AusgBlinklicht},{LAUFLICHT, nothing}, {LAUFLICHT, langsamer}, {LAUFLICHT, schneller}, {LAUFLICHT, NextLauflicht}, {LAUFLICHT, nothing}},
	{{BLINKLICHT, nothing},{LAUFLICHT, AusgLauflicht},{BLINKLICHT, langsamer},{BLINKLICHT, schneller},{BLINKLICHT, NextBlinklicht}, {BLINKLICHT, nothing}}
};

enum Ereignisse getEvent() {
	if (isButtonPressed(0) && !buttonCurrentlyPressed) {
		buttonCurrentlyPressed = true;
		return TasteS0Runter;
	} else if (buttonCurrentlyPressed && !isButtonPressed(0)) {
		buttonCurrentlyPressed = false;
		return TasteS0Rauf;
	}
	
	if (isButtonPressed(1)) {
		return TasteS1;
	}
	
	if (isButtonPressed(2)) {
		return TasteS2;
	}
	
	if ((TIM2->SR & 1) == 0) {
		return TimerAbgelaufen;
	}
	
	return Nichts;
}

/**
  * @brief  Main program
  * @param  None
  * @retval None
  */
int main(void){
	initITSboard();                 // Initialisierung des ITS Boards
	initTimer();
	TIM2->ARR = 90000000; // 90MHz * 10E06 => 1s
	GUI_init(DEFAULT_BRIGHTNESS);   // Initialisierung des LCD Boards mit Touch
	if (!checkVersionFlashFonts()) {
			// Ueberpruefe Version der Fonts im Flash passt nicht zur Software Version
				Error_Handler();
	}
	
	while (1) {
		enum Ereignisse ereignis = getEvent();
		struct EventHandler nextUp = EventHandlerTable[Zustand][ereignis]; // Get next state
		if (nextUp.NeuerZustand != Zustand) {
			clear(); // Clear all LEDs in order to prevent ghost LEDs
			Zustand = nextUp.NeuerZustand; // Replace current state with next state
		};
		if ((TIM2->SR & 1) == 1) {
			TIM2->SR >>= 1; // Right and then left bitshift in order to get rid of only the 0th bit
			TIM2->SR <<= 1;
			nextUp.Start(); // Start next state
		};
	}
}

// EOF
