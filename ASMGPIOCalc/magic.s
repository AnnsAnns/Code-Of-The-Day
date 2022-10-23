    PRESERVE8
    AREA    BSearch, CODE
    EXPORT addASM
	EXPORT subASM
	EXPORT mulASM
	EXPORT divASM

addASM   PROC
	ADD r0, r0, r1
	bx lr
	ENDP

subASM PROC
	SUB r0, r0, r1
	bx lr
	ENDP
		
mulASM PROC
	MUL r0, r0, r1
	bx lr
	ENDP
		
divASM PROC
	SDIV r0, r0, r1
	bx lr
	ENDP
		
	ALIGN
	END