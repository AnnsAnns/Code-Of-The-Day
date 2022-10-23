#include <stdio.h>
#include "stm32f4xx_hal.h"
#include "init.h"
#include "delay.h"
#include "LCD_GUI.h"
#include "LCD_Demos.h"
#include "lcd.h"
#include <stdlib.h>
#include "fontsFLASH.h"
#include "LCD_Touch.h"
#include "error.h"
#include <stdbool.h>
#include "stm32f4xx.h"

extern int addASM(int a, int b);
extern int subASM(int a, int b);
extern int divASM(int a, int b);
extern int mulASM(int a, int b);

static bool ButtonTracker[8];

bool isButtonPressed(int button) {
	bool isCurrentlyPressed = ((0x01U << button) != (GPIOF->IDR & (0x01U << button)));
	
	if (ButtonTracker[button] != isCurrentlyPressed) {
		ButtonTracker[button] = isCurrentlyPressed;
		
		if (isCurrentlyPressed) {
			return true;
		}
	}
	return false;
};

void modLight(int light, bool disable) {
	int alt = 0;
	
	if (!disable) {
		alt = 16;
	};
	
	int value = (0x01U << light) << alt;
	GPIOD->BSRR = value;
}

struct CalcTracker {
	int op1;
	int op2;
	int ergebnis;
	char calcChar;
	int (*calcFunc)(int, int);
} tracker;

enum Operators {
	ADD,
	SUB,
	MUL,
	DIV,
	ADDASM,
	SUBASM,
	MULASM,
	DIVASM
} CurrentOperator;

void printOutput(struct CalcTracker tracker) {
	char printVal[80];
	
	sprintf(printVal, "%d %c %d = %d", tracker.op1, tracker.calcChar, tracker.op2, tracker.ergebnis);
	lcdPrintlnS(printVal);
}

int add(int one, int two) {
	return one + two;
}

int sub(int one, int two) {
	return one - two;
}

int divid(int one, int two) {
	return one / two;
}

int mult(int one, int two) {
	return one*two;
}

static struct CalcTracker trackerTrackerTrackingTrackerFunnyName[8] = {
	{0, 0, 0, '+', add},
	{0, 0, 0, '-', sub},
	{0, 0, 0, '*', mult},
	{0, 0, 0, '/', divid},
	{0, 0, 0, '&', addASM},
	{0, 0, 0, '~', subASM},
	{0, 0, 0, '.', mulASM},
	{0, 0, 0, ':', divASM}
};

/**
  * @brief  Main program
  * @param  None
  * @retval None
  */
int main(void){
	initITSboard();                 // Initialisierung des ITS Boards
	GUI_init(DEFAULT_BRIGHTNESS);   // Initialisierung des LCD Boards mit Touch
	TP_Init(false);                 // Initialisierung des LCD Boards mit Touch
	if (!checkVersionFlashFonts()) {
	    // Ueberpruefe Version der Fonts im Flash passt nicht zur Software Version
		Error_Handler();
	}
	
	tracker = trackerTrackerTrackingTrackerFunnyName[0];
	
	while (1) {
		int res;
		bool printToOutput = isButtonPressed(7);
		bool changeOperator = isButtonPressed(6);
		
		int in = GPIOF->IDR & 0xFF;
		int op2 = in & 0x38;
		op2 >>= 3;
		int op1 = in & 0x07;
		
		if (changeOperator) {
			if (CurrentOperator == DIVASM) {
				CurrentOperator = ADD;
			} else {
				CurrentOperator += 1;
			}
			
			tracker = trackerTrackerTrackingTrackerFunnyName[CurrentOperator];
		};
		
		tracker.op1 = op1;
		tracker.op2 = op2;
		tracker.ergebnis = tracker.calcFunc(tracker.op1, tracker.op2);
		
		if (changeOperator) {
			char retValue[10] = "";
			sprintf(retValue, "= %c", tracker.calcChar);
			lcdPrintlnS(retValue);
		}
		
		if (printToOutput) {
			printOutput(tracker);
		};
		
		for (int i = 0; i <= 8; i++) {
			modLight(i, res & 1);
			res >>= 1;
		}
	}
}

// EOF
