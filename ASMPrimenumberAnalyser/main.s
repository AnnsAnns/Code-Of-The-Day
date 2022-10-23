    EXTERN initITSboard
	EXTERN GUI_init
	EXTERN TP_Init
    
	EXTERN lcdPrintS            ;Display Text Ausgabe
	EXTERN lcdPrintInt	        ;Display Ganzzahl Ausgabe
    

;********************************************
; Data section, aligned on 4-byte boundery
;********************************************
	
	AREA MyData, DATA, align = 2
	
	    GLOBAL text
DEFAULT_BRIGHTNESS DCW  800

;--- Testausgabe kann geloescht werden 	
text	DCB	"Hallo TI-Labor",0
;--- Ende kann geloescht werden

;--- Start eigene Variablen ----------------

Base
PrimFeld		FILL 20000
Anzahl			DCD 20000
SQRTAnzahl		DCD 142
AnalyseStart	DCD 0
AnalyseEnde		DCD 545
	
				EXPORT PrimFeld
				EXPORT Anzahl
				EXPORT SQRTAnzahl
				EXPORT AnalyseStart
				EXPORT AnalyseEnde

;--- Ende eigene Variablen

;********************************************
; Code section, aligned on 8-byte boundery
;********************************************
	AREA |.text|, CODE, READONLY, ALIGN = 3

;--------------------------------------------
; main subroutine
;--------------------------------------------
				EXPORT main [CODE]
			
main			PROC
				BL initITSboard
				ldr r1, =DEFAULT_BRIGHTNESS
				ldrh r0, [r1]
				bl GUI_init
				mov r0, #0x00
				bl TP_Init
				
				
				; ----Start eigener Code -------- 		
				
;;;;;;;;;;;;;;;;;;;;;;;;;
;Primzahlenfinder  1.2.1;
;;;;;;;;;;;;;;;;;;;;;;;;;
				
				LDR r6,=PrimFeld ; Lade Startadresse f�rs Primfeld
				LDR r1,=1; Load 1?
		
				MOVS r4,#2 ; Load i
				
				LDR r0,=Anzahl ; Load ANZ Address
				LDR r5,[r0] ; Load ANZ into r5
				LDR r0,=SQRTAnzahl ; Load SQRTAnzahl Address
				LDR r7,[r0]; Load endi into r7
		
whileInitFelder	CMP r4, r5 ; If r4(i) < r5(ANZ)
				BLT doInitFelder ; True Jump
				b endInitFelder ; False Jump
				
doInitFelder	
				STRB r1,[r6] ; Set 1 on memory address
				ADD r6, r6, #1 ; r6(ANZ Adresse) += 1
				ADD r4, r4, #1 ; r4(i) += 1
				
				b whileInitFelder
endInitFelder

				; 0 und 1 per default keine Primzahl
				LDR r0,=0 ; r0 = 0
				LDR r1,=PrimFeld ; Lade Startadresse vom Primfeld
				STRB r0,[r1] ; prims[0] = 0
				ADD r1, r1, #1 ; Adresse + 1
				STRB r0,[r1] ; prims[1] = 0

				LDR r4,=0 ; i = 0

whileFirstLoop	CMP r4, r7; if i < endi
				BLT doFirstLoop
				b endFirstLoop
doFirstLoop
				
ifFieldOne		LDR r0,=PrimFeld ; Adress von PrimFeld in r0
				ADD r0, r0, r4 ; Adresse + i
				LDRB r1,[r0] ; Lade Inhalt von PrimFeld + i in r1

				CMP r1,#1 ; if prims[i] == 1
				BEQ doFieldOne
				b endFieldOne
doFieldOne
				MUL r3, r4, r4 ; r3(j) = i * i
				MOV r9,#0 ; r9 = 0
				LDR r0,=PrimFeld ; Lade Startadresse f�rs Primfeld

whileSecondLoop	CMP r3, r5 ; j < ANZ
				BLT doSecondLoop
				b endSecondLoop
doSecondLoop
				STRB r9,[r0,r3] ; Store 0 in prims[j]
				
				ADD r3, r3, r4; j = j + i
				b whileSecondLoop
endSecondLoop
endFieldOne

				ADD r4, r4, #1 ; i++
				b whileFirstLoop
endFirstLoop

				LDR r0,=AnalyseStart ; Lade Adresse von Untergrenze
				LDR r1,[r0] ; Lade Untergrenze in r8
				LDR r0,=AnalyseEnde ; Lade Adresse von Obergrenze
				LDR r2,[r0] ; Lade Obergrenze in r7
				BL primzahlfunc

;;;;;;;;;;;;;;;;;;;;;;;;;
;     Ausgabe  1.2.3    ;
;;;;;;;;;;;;;;;;;;;;;;;;;

				;-----Ende eigener Code ----
				
				;---- Testausgabe kann geloescht werden
				BL  lcdPrintInt
				;---- Ende kann geloescht werden

forever			b	forever		; nowhere to retun if main ends		
				ENDP
			
			
				;------ Start eigene Funktionen --------------

				;;;;;;;;;;;;;;;;;;;;;;;;;
				;Primzahlenanalyse 1.2.2;
				;;;;;;;;;;;;;;;;;;;;;;;;;
				
				
primzahlfunc	PROC
				PUSH {r9, r5, LR, r8}
				LDR r9,=PrimFeld ; Lade Adresse vom Primfeld
				LDR r0,=0 ; r0 = 0
				LDR r5,=0 ; r5 = 0
				
whileAnalyse	cmp r1, r2 ; Untergrenze (i) < Obergrenze
				BLT doWhileAnalyse
				b endWhileAnalyse
doWhileAnalyse
				LDRB r5,[r9, r1] ; r5 = Inhalt von Adresse Primfeld + i (Untergrenze)
				
ifEins			CMP r5, #1 ; if Inhalt == 1
				BEQ doIfEins
				b endIfEins

doIfEins		ADD r0, r0, #1

endIfEins
				ADD r1, r1, #1 ; Untergrenze++
				b whileAnalyse
endWhileAnalyse
				BL lcdPrintInt
				POP {r9, r5, LR, r8}
				BX LR
				ENDFUNC
				
				;----- Ende eigene Funktionen -----------
				ALIGN
			   
				END