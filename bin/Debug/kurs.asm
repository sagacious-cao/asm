.386
Data1 segment

	ten		dd	10d
	four	dd	4
	hex		dd	20h
	octo	dw	28+5-16h
	binar	dd	1010010001b
	text	db	'wor7%$xfgjsfghjsfghjsfgh'
	label10	dd	l10
			dw	seg l10

Data1 ends

Data2 segment

	Twenty	dd	20
	Thirty	dd	30
	ONE		dd	1
	TWO		dd	2

Data2 ends

Code1 segment

	assume	cs: Code1, ds: Data1, es: Data2
	begin:
		mov		eax, dword ptr es:text[ecx+edx*2]
		cld
		mov		eax,twenty;some comment
		mov		ebx, ten
		cmp		es:thirty, eax
		jae		l20
		jmp		fword ptr label10
		jmp		fword ptr es:text[ecx+edx*2]
	l20:mov		eax, dword ptr octo
		mov		edx, binar
		neg		eax
		neg		edx
		push	four;
		push	dword ptr es:text[ecx+edx*1]
		jmp		far ptr l10
		
Code1 ends

Code2 segment

	assume cs: Code2

		mov		eax, four
		cmp		twenty,ecx
	l10 label far
		cmp		dword ptr es:hex[ecx+edx*1], eax
		jae		l30
	l30:
		mov		edx, one
		mov		ecx, two
		mov		ecx, dword ptr es:text[ebx+esi*4]

Code2 ends

end begin