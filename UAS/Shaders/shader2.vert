#version 330 core
//layout untuk di EnableVertexAttribArray
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aNormal;
//layout (location = 2) in vec2 aTexCoords; //untuk menggunakan texture

//untuk camera
uniform mat4 transform;
uniform mat4 view;
uniform mat4 projection;

//output yang akan di bawa ke lighting.frag
out vec3 Normal;
out vec3 FragPos;
//out vec2 TexCoords; //untuk menggunakan texture

void main(){
	gl_Position = vec4(aPosition,1.0) * transform * view * projection;
	FragPos = vec3(vec4(aPosition, 1.0) * transform);
    Normal = aNormal * mat3(transpose(inverse(transform)));
	//TexCoords = aTexCoords;
}

