﻿#version 330

out vec4 outputColor;

void main()
{
    outputColor = vec4(1.0f); //hanya aplha, karena set color nya ada di lighting.frag
}