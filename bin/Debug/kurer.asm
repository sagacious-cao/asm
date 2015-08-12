.386
;Data1 segment

	ten		dgd	10d
	four	dd	4999999999999999
	hex~	dd	20h
	octo	dw	8
	binar	dd	1010010001b
	text	db	'wor7%$xfgjsfghjsfghjsfgh'
	label10	dd	l10
			dw	seg 8

Data1 ends

Data2 segment

	Twenty	dd	20,5
	Thirty	dd	30
	Thirty	dd	300
	ONE		dd	"text"
	TWO555555555	dd	2
	fifty	dd	(28+5)16h)-11b

Data2 ends

Code1 segment

	assume	cs: Code1, ds: Data1, es: Data2
	begin:
		mov		ten, ptr es:text[ecx+edx*2]
		cld
		mov		eax,twenty;some comment
		mov		ebx, teng
		30 cmp		es:thirty, eax
		jae		l20
		jmp		fword ptr label10
		jmp		fword ptr [ecx]
	l20:mov		eax, dword ptr octo
		mov		edx, binar,20
		neg		eax
		neg		dx
		push	ecx;
		push	dword ptr es:text[ecx+edx*5]
		jmp		far ptr l10
		
Code1 ends

Code2 segment

	assume cs: Code2

		mov		eax, four
		cmp		twenty,ecx
	l10 label far
		cmp		dword ptr es:hex[ecx+esp*1], eax
		jae		l30
	l30 label
		mov		edx, one
		mov		ecx, two
		mov		ecx, dword ptr es:text[ebx+esi*4]

Code2 ends

end begin